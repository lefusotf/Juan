# Sistema de Gestión Veterinaria (C# WinForms + SQL Server)

Proyecto de módulo BTVDS-1.3 — Diseño de Aplicaciones Multimedia (Instituto Técnico Ricaldone).

Reemplaza el uso desordenado de archivos físicos por un sistema para organizar expedientes de mascotas,
vacunas, propietarios y citas médicas.

## Cómo ejecutarlo

1. Abra **SQL Server Management Studio** (o Visual Studio → Explorador de objetos de SQL Server) conectado a
   `(localdb)\MSSQLLocalDB` y ejecute **una sola vez** el script `BaseDatos/Veterinaria.sql`.
   (Para reiniciar los datos: `DROP DATABASE Veterinaria;` y volver a ejecutar el script).
2. Abra `Veterinaria.sln` con Visual Studio 2022 (carga de trabajo *Desarrollo de escritorio con .NET*).
   Al compilar, NuGet restaura automáticamente **BCrypt.Net-Next**.
3. Establezca **Vista** como proyecto de inicio y presione F5.
4. Si usa otra instancia de SQL Server, cambie `Data Source` en `Vista/App.config`.

## Usuarios de prueba

| Rol           | Usuario  | Contraseña  |
|---------------|----------|-------------|
| Administrador | `admin`  | `Admin123*` |
| Veterinario   | `dvera`  | `Vet12345*` |
| Recepcionista | `recep1` | `Recep123*` |

## Estructura

```
Veterinaria.sln
├── BaseDatos/Veterinaria.sql      Script de BD (roles, permisos, usuarios, entidades, vistas, datos)
├── Modelos/                       CAPA MODELO (sin formularios)
│   ├── Conexion_DB/               Conexión y helpers parametrizados
│   ├── Entidades/                 Clases de dominio
│   ├── Datos/                     Acceso a datos (CRUD) por entidad
│   ├── Seguridad/                 BCrypt, sesión y códigos de permiso
│   └── Utilidades/                Logger (archivo + tabla bitácora)
├── Vista/                         CAPA VISTA (Windows Forms)
│   ├── Comun/                     Formulario base CRUD, estilos, mensajes, validaciones
│   ├── Login/  Dashboard/  Usuarios/  Propietarios/  Mascotas/
│   └── Citas/  Consultas/  Vacunas/  Bitacora/
└── Documentacion/Documentacion.md Portada, índice, introducción, casos de uso, ER y diccionario de datos
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
