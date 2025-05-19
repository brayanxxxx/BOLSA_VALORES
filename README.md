
# 📈 BOLSA DE VALORES - Equipo 8

## 📌 Propósito
Este proyecto tiene como objetivo desarrollar una aplicación de simulación de inversiones en bolsa de valores aplicando buenas prácticas de programación. Se utilizan patrones de diseño como Repository y Singleton, junto con principios SOLID, para lograr un sistema modular, mantenible y escalable.

La aplicación gestiona entidades como Usuarios, Acciones y Transacciones, brindando una experiencia interactiva para inversores y administradores en un entorno simulado.

---

## 🛠 Tecnologías Utilizadas
- **Lenguaje:** C#
- **Interfaz Gráfica:** Windows Forms
- **Base de Datos:** SQL Server
- **Acceso a Datos:** ADO.NET (sin ORM)
- **Patrones de diseño:** Repository, Singleton
- **Principios SOLID aplicados:** SRP (Principio de Responsabilidad Única), OCP (Principio Abierto/Cerrado)

---

## ⚙️ Funcionalidades del Sistema

### 👥 Gestión de Usuarios e Inversores
- CRUD completo de usuarios (Agregar, Actualizar, Eliminar, Consultar)
- Roles: Administrador e Inversor
- Control de acceso y autenticación
- Almacenamiento de datos financieros y saldo de los usuarios

### 📊 Gestión de Acciones y Valores
- CRUD de acciones del mercado (símbolo, sector, precio, variación)
- Simulación de actualización de precios en tiempo real

### 💰 Simulador de Transacciones (Compra/Venta)
- Compra/Venta de acciones por parte de inversores
- Validación de saldo antes de cada transacción
- Actualización del portafolio del usuario en tiempo real

### 📈 Reportes y Rendimiento
- Generación de reportes de portafolio por usuario
- Reportes de acciones más rentables y más transadas
- Cálculo de rentabilidad acumulada

### 🔔 Notificaciones de Cambios Importantes
- Notificaciones automáticas a los usuarios cuando las acciones de su portafolio presentan cambios relevantes de precio

---

## 🔑 Datos de Acceso (Pruebas)

### Administrador
- **Usuario:** ADMIN
- **Contraseña:** admin123

### Inversor
- **Usuario:** DEIVY
- **Contraseña:** 123

---

## 📸 Capturas de Pantalla

### Modelo Relacional
![image](https://github.com/user-attachments/assets/76589f58-c4d3-43c3-8936-22023cc5c509)

### Login Form
![image](https://github.com/user-attachments/assets/09255495-4932-4f05-bbbe-f20786e7b2e6)

### Dashboard Inversor
![image](https://github.com/user-attachments/assets/42efe668-79d2-48b1-a234-43af00819bba)

### Admin Dashboard
![image](https://github.com/user-attachments/assets/dc329480-2695-42be-b5fe-8d9a92dd72c9)

### CRUD de Usuarios
![image](https://github.com/user-attachments/assets/5da7883a-56f4-4958-89c2-34b6cd06ad6b)

---

## 🧪 Instrucciones para Compilar y Ejecutar

1. Tener instalado **Visual Studio 2019 o superior** y **SQL Server**.
2. Clonar este repositorio:

```bash
git clone https://github.com/tuusuario/BOLSA_VALORES.git
```

3. Abrir el archivo `BOLSA_VALORES.sln` en Visual Studio.
4. Restaurar los paquetes NuGet si es necesario.
5. Cargar la base de datos ejecutando el script en `Scripts/BOLSA_VALORES.sql`.
6. Establecer el proyecto como **inicio** y presionar `F5` para ejecutar la aplicación.

---

👨‍💻 Desarrollado por el **Equipo 8**
