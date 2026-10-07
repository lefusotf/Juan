# Sistema de Gestión Veterinaria (C# WinForms + SQL Server)

Proyecto de módulo BTVDS-1.3 — Diseño de Aplicaciones Multimedia (Instituto Técnico Ricaldone).

Reemplaza el uso desordenado de archivos físicos por un sistema para organizar expedientes de mascotas,
vacunas, propietarios y citas médicas.

## Cómo ejecutarlo

1. Abra **SQL Server Management Studio** conectado a su instancia y ejecute **una sola vez** el script
   `BaseDatos/Veterinaria.sql`. (Para reiniciar los datos: `DROP DATABASE Veterinaria;` y volver a ejecutarlo).
2. Abra `Veterinaria.sln` con Visual Studio 2022 (carga de trabajo *Desarrollo de escritorio con .NET*).
   Al compilar, NuGet restaura automáticamente **BCrypt.Net-Next**.
3. Establezca **dashboardVet** como proyecto de inicio y presione F5.
4. Si su servidor de SQL Server se llama distinto, cambie la constante `Servidor` en `Modelos/Conexion.cs`.

## Usuarios de prueba

| Rol           | Usuario  | Contraseña  |
|---------------|----------|-------------|
| Administrador | `admin`  | `Admin123*` |
| Veterinario   | `dvera`  | `Vet12345*` |
| Recepcionista | `recep1` | `Recep123*` |

## Estructura (misma organización que el proyecto de referencia)

```
Veterinaria.sln
├── BaseDatos/Veterinaria.sql     Script de BD (roles, permisos, usuarios, tablas, vistas, datos)
├── Modelos/                      CAPA MODELO (namespace único Modelos, sin formularios)
│   ├── Conexion.cs               ObtenerConexion()
│   ├── ErroresSistema.cs         CatalogoErrores, ErrorInfo y AppException
│   ├── DALBase.cs                Consultas parametrizadas y traducción de SqlException a AppException
│   ├── *DAL.cs                   Propietario, Mascota, Cita, Consulta, Vacuna, AplicacionVacuna, Usuario,
│   │                             Rol, Permisos, Login, Bitacora y Dashboard
│   ├── Sesion.cs                 Sesión, UsuarioSesion y códigos de permiso
│   ├── EncriptadorContrasena.cs  BCrypt
│   └── Logger.cs                 Archivo diario + tabla bitácora
└── dashboardVet/                 CAPA VISTA (Windows Forms; cada pantalla en su carpeta: .cs + .Designer.cs)
    ├── Program.cs                Arranque, manejo global de excepciones y bucle de sesión
    ├── ManejadorUIErrores.cs     Mensajes de error por código y tooltips automáticos
    ├── Base/FrmBase.cs           Formulario base de todas las pantallas
    ├── Helpers/                  Tema, Responsive, Mensajes, Validaciones, Entrada, Texto, GridUtil
    ├── Controles/                BotonModerno, BotonMenu, PanelTarjeta, PanelDegradado
    └── Login/ DashBoard/ Inicio/ Propietarios/ Mascotas/ Citas/ Consultas/
        Vacunas/ CatalogoVacunas/ AplicacionVacunas/ Usuarios/ Roles/ Bitacora/
```

## Permisos por rol

| Módulo                | Administrador | Veterinario | Recepcionista |
|-----------------------|:-------------:|:-----------:|:-------------:|
| Usuarios / asignar personal | ✔ | – | – |
| Roles y permisos      | ✔ | – | – |
| Bitácora              | ✔ | – | – |
| Propietarios          | ✔ CRUD | ver | ✔ CRUD |
| Mascotas              | ✔ CRUD | ver | ✔ CRUD |
| Citas                 | ✔ CRUD | ver | ✔ CRUD |
| Consultas (historial) | ✔ CRUD | ✔ CRUD | – |
| Vacunas (catálogo y aplicación) | ✔ CRUD | ✔ CRUD | – |

Los permisos se guardan en las tablas `rol`, `permiso` y `rolPermiso`, y el administrador puede modificarlos
desde *Roles y permisos*.
