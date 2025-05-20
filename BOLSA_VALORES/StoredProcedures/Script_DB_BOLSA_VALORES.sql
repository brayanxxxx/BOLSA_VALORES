IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BOLSA_VALORES')
BEGIN
    CREATE DATABASE BOLSA_VALORES;
END
GO

USE BOLSA_VALORES;
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuarios' AND xtype='U')
BEGIN
    CREATE TABLE Usuarios (
        UsuarioID INT PRIMARY KEY IDENTITY,
        Nombre NVARCHAR(100) NOT NULL,
        TipoUsuario NVARCHAR(20) NOT NULL, -- 'Administrador' o 'Inversor'
        Saldo DECIMAL(18,2) NOT NULL
    );
END
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Acciones' AND xtype='U')
BEGIN
    CREATE TABLE Acciones (
        AccionID INT PRIMARY KEY IDENTITY,
        Simbolo NVARCHAR(10) NOT NULL,
        Nombre NVARCHAR(100) NOT NULL,
        Sector NVARCHAR(50),
        PrecioActual DECIMAL(18,2) NOT NULL,
        VariacionDiaria DECIMAL(5,2) NOT NULL
    );
END
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Portafolio' AND xtype='U')
BEGIN
    CREATE TABLE Portafolio (
        PortafolioID INT PRIMARY KEY IDENTITY,
        UsuarioID INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioID),
        AccionID INT NOT NULL FOREIGN KEY REFERENCES Acciones(AccionID),
        Cantidad INT NOT NULL,
        ValorInvertido DECIMAL(18,2) NOT NULL
    );
END
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Transacciones' AND xtype='U')
BEGIN
    CREATE TABLE Transacciones (
        TransaccionID INT PRIMARY KEY IDENTITY,
        UsuarioID INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioID),
        AccionID INT NOT NULL FOREIGN KEY REFERENCES Acciones(AccionID),
        FechaTransaccion DATETIME NOT NULL DEFAULT GETDATE(),
        TipoTransaccion NVARCHAR(10) NOT NULL, -- 'Compra' o 'Venta'
        Cantidad INT NOT NULL,
        PrecioUnitario DECIMAL(18,2) NOT NULL
    );
END
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Notificaciones' AND xtype='U')
BEGIN
    CREATE TABLE Notificaciones (
        NotificacionID INT PRIMARY KEY IDENTITY,
        UsuarioID INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioID),
        Mensaje NVARCHAR(255),
        FechaEnvio DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO


IF OBJECT_ID('SP_RegistrarTransaccion') IS NOT NULL
    DROP PROCEDURE SP_RegistrarTransaccion;
GO

CREATE PROCEDURE SP_RegistrarTransaccion
    @UsuarioID INT,
    @AccionID INT,
    @TipoTransaccion NVARCHAR(10),
    @Cantidad INT,
    @PrecioUnitario DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Total DECIMAL(18,2) = @Cantidad * @PrecioUnitario;

    IF @TipoTransaccion = 'Compra'
    BEGIN
        DECLARE @SaldoActual DECIMAL(18,2);
        SELECT @SaldoActual = Saldo FROM Usuarios WHERE UsuarioID = @UsuarioID;

        IF @SaldoActual < @Total
        BEGIN
            RAISERROR ('Fondos insuficientes.', 16, 1);
            RETURN;
        END

        
        UPDATE Usuarios SET Saldo = Saldo - @Total WHERE UsuarioID = @UsuarioID;

        
        IF EXISTS (
            SELECT * FROM Portafolio WHERE UsuarioID = @UsuarioID AND AccionID = @AccionID
        )
        BEGIN
            UPDATE Portafolio
            SET Cantidad = Cantidad + @Cantidad,
                ValorInvertido = ValorInvertido + @Total
            WHERE UsuarioID = @UsuarioID AND AccionID = @AccionID;
        END
        ELSE
        BEGIN
            INSERT INTO Portafolio (UsuarioID, AccionID, Cantidad, ValorInvertido)
            VALUES (@UsuarioID, @AccionID, @Cantidad, @Total);
        END
    END
    ELSE IF @TipoTransaccion = 'Venta'
    BEGIN
        DECLARE @CantidadActual INT;
        SELECT @CantidadActual = Cantidad FROM Portafolio WHERE UsuarioID = @UsuarioID AND AccionID = @AccionID;

        IF @CantidadActual IS NULL OR @CantidadActual < @Cantidad
        BEGIN
            RAISERROR ('No tiene suficientes acciones para vender.', 16, 1);
            RETURN;
        END

        
        UPDATE Usuarios SET Saldo = Saldo + @Total WHERE UsuarioID = @UsuarioID;

        
        UPDATE Portafolio
        SET Cantidad = Cantidad - @Cantidad,
            ValorInvertido = ValorInvertido - @Total
        WHERE UsuarioID = @UsuarioID AND AccionID = @AccionID;

       
        DELETE FROM Portafolio WHERE UsuarioID = @UsuarioID AND AccionID = @AccionID AND Cantidad <= 0;
    END

    
    INSERT INTO Transacciones (UsuarioID, AccionID, TipoTransaccion, Cantidad, PrecioUnitario)
    VALUES (@UsuarioID, @AccionID, @TipoTransaccion, @Cantidad, @PrecioUnitario);
END
GO


IF OBJECT_ID('SP_ActualizarPrecioAccion') IS NOT NULL
    DROP PROCEDURE SP_ActualizarPrecioAccion;
GO

CREATE PROCEDURE SP_ActualizarPrecioAccion
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Acciones
    SET
        VariacionDiaria = ROUND((RAND() * 10 - 5), 2), -- entre -5% y +5%
        PrecioActual = ROUND(PrecioActual * (1 + (VariacionDiaria / 100.0)), 2);
END
GO


IF OBJECT_ID('SP_GenerarReportePortafolio') IS NOT NULL
    DROP PROCEDURE SP_GenerarReportePortafolio;
GO

CREATE PROCEDURE SP_GenerarReportePortafolio
    @UsuarioID INT
AS
BEGIN
    SELECT
        A.Simbolo,
        A.Nombre AS NombreAccion,
        P.Cantidad,
        A.PrecioActual,
        P.ValorInvertido,
        (P.Cantidad * A.PrecioActual) AS ValorActual,
        ((P.Cantidad * A.PrecioActual) - P.ValorInvertido) AS Ganancia
    FROM Portafolio P
    INNER JOIN Acciones A ON P.AccionID = A.AccionID
    WHERE P.UsuarioID = @UsuarioID;
END
GO


IF OBJECT_ID('SP_NotificarCambiosImportantes') IS NOT NULL
    DROP PROCEDURE SP_NotificarCambiosImportantes;
GO

CREATE PROCEDURE SP_NotificarCambiosImportantes
AS
BEGIN
    INSERT INTO Notificaciones (UsuarioID, Mensaje)
    SELECT DISTINCT P.UsuarioID,
        CONCAT('La acción ', A.Simbolo, ' tuvo una variación de ', A.VariacionDiaria, '%.')
    FROM Portafolio P
    INNER JOIN Acciones A ON P.AccionID = A.AccionID
    WHERE ABS(A.VariacionDiaria) >= 5;
END
GO


IF OBJECT_ID('SP_ObtenerAccionesPopulares') IS NOT NULL
    DROP PROCEDURE SP_ObtenerAccionesPopulares;
GO

CREATE PROCEDURE SP_ObtenerAccionesPopulares
AS
BEGIN
    SELECT A.Simbolo, A.Nombre,
        COUNT(T.TransaccionID) AS TotalTransacciones
    FROM Transacciones T
    INNER JOIN Acciones A ON T.AccionID = A.AccionID
    GROUP BY A.Simbolo, A.Nombre
    ORDER BY TotalTransacciones DESC;
END
GO


-- SP para autenticar usuario
CREATE PROCEDURE SP_AutenticarUsuario
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT * FROM Usuarios 
    WHERE Username = @Username AND Password = @Password
END
GO

-- SP para obtener todos usuarios
CREATE PROCEDURE SP_ObtenerTodosUsuarios
AS
BEGIN
    SELECT * FROM Usuarios
END
GO

-- SP para agregar usuario
CREATE PROCEDURE SP_AgregarUsuario
    @Nombre NVARCHAR(100),
    @TipoUsuario NVARCHAR(50),
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @Saldo DECIMAL(18,2)
AS
BEGIN
    INSERT INTO Usuarios (Nombre, TipoUsuario, Username, Password, Saldo)
    VALUES (@Nombre, @TipoUsuario, @Username, @Password, @Saldo)
END
GO

-- SP para actualizar usuario
CREATE PROCEDURE SP_ActualizarUsuario
    @UsuarioID INT,
    @Nombre NVARCHAR(100),
    @TipoUsuario NVARCHAR(50),
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @Saldo DECIMAL(18,2)
AS
BEGIN
    UPDATE Usuarios SET Nombre = @Nombre, TipoUsuario = @TipoUsuario,
                       Username = @Username, Password = @Password,
                       Saldo = @Saldo
    WHERE UsuarioID = @UsuarioID
END
GO

-- SP para obtener usuario por ID
CREATE PROCEDURE SP_ObtenerUsuarioPorID
    @UsuarioID INT
AS
BEGIN
    SELECT * FROM Usuarios WHERE UsuarioID = @UsuarioID
END
GO

-- SP para actualizar saldo
CREATE PROCEDURE SP_ActualizarSaldo
    @UsuarioID INT,
    @Saldo DECIMAL(18,2)
AS
BEGIN
    UPDATE Usuarios SET Saldo = @Saldo WHERE UsuarioID = @UsuarioID
END
GO

-- SP para eliminar usuario
CREATE PROCEDURE SP_EliminarUsuario
    @UsuarioID INT
AS
BEGIN
    DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID
END
GO


INSERT INTO Usuarios (Nombre, TipoUsuario, Username, Password, Saldo)
VALUES ('DEIVY', 'Inversor', 'DEIVY', '123', 1000.00);
GO

INSERT INTO Usuarios (Nombre, TipoUsuario, Username, Password, Saldo)
VALUES ('Admin Principal', 'Administrador', 'ADMIN', 'admin123', 0.00);
GO

INSERT INTO Acciones (Simbolo, Nombre, Sector, PrecioActual, VariacionDiaria)
VALUES 
('AAPL', 'Apple Inc.', 'Tecnología', 175.50, 1.25),
('MSFT', 'Microsoft Corp.', 'Tecnología', 300.10, -0.85),
('TSLA', 'Tesla Inc.', 'Automotriz', 720.75, 3.40);
GO

