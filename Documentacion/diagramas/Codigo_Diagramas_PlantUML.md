# Código PlantUML de los diagramas

Pegue cada bloque en https://www.plantuml.com/plantuml o en la extensión PlantUML de Visual Studio Code, o abra los archivos `.puml` de esta carpeta.

## 01_casos_de_uso.puml

```plantuml
@startuml casos_de_uso
left to right direction
skinparam packageStyle rectangle
skinparam actorStyle awesome

actor "Administrador" as Admin
actor "Veterinario" as Vet
actor "Recepcionista" as Recep

rectangle "Sistema de Gestión Veterinaria" {
  usecase "Iniciar sesión" as UC01
  usecase "Cerrar sesión" as UC02
  usecase "Gestionar usuarios" as UC03
  usecase "Asignar permisos a roles" as UC04
  usecase "Consultar bitácora" as UC05
  usecase "Gestionar propietarios" as UC06
  usecase "Gestionar mascotas" as UC07
  usecase "Agendar citas" as UC08
  usecase "Registrar historial médico" as UC09
  usecase "Gestionar catálogo de vacunas" as UC10
  usecase "Aplicar vacunas" as UC11
  usecase "Consultar propietarios, mascotas y citas" as UC12
  usecase "Verificar credenciales (BCrypt)" as UC13
}

Admin --> UC01
Admin --> UC02
Admin --> UC03
Admin --> UC04
Admin --> UC05
Admin --> UC06
Admin --> UC07
Admin --> UC08
Admin --> UC09
Admin --> UC10
Admin --> UC11

Vet --> UC01
Vet --> UC02
Vet --> UC09
Vet --> UC10
Vet --> UC11
Vet --> UC12

Recep --> UC01
Recep --> UC02
Recep --> UC06
Recep --> UC07
Recep --> UC08

UC01 ..> UC13 : <<include>>
@enduml
```

## 02_entidad_relacion.puml

```plantuml
@startuml entidad_relacion
hide circle
skinparam linetype ortho
skinparam classAttributeIconSize 0

entity "rol" as rol {
  *idRol : INT <<PK>>
  --
  *nombre : VARCHAR(50) <<UQ>>
  descripcion : VARCHAR(200)
}

entity "permiso" as permiso {
  *idPermiso : INT <<PK>>
  --
  *codigo : VARCHAR(50) <<UQ>>
  *descripcion : VARCHAR(200)
}

entity "rolPermiso" as rolPermiso {
  *idRol : INT <<PK,FK>>
  *idPermiso : INT <<PK,FK>>
}

entity "usuario" as usuario {
  *idUsuario : INT <<PK>>
  --
  *nombreCompleto : VARCHAR(150)
  *nombreUsuario : VARCHAR(50) <<UQ>>
  *contrasena : VARCHAR(100)
  correo : VARCHAR(150)
  *idRol : INT <<FK>>
  *estado : VARCHAR(20)
  *fechaCreacion : DATETIME
}

entity "propietario" as propietario {
  *idPropietario : INT <<PK>>
  --
  *nombre : VARCHAR(150)
  *dui : VARCHAR(10) <<UQ>>
  *telefono : VARCHAR(15)
  correo : VARCHAR(150)
  direccion : VARCHAR(250)
  *fechaRegistro : DATETIME
}

entity "mascota" as mascota {
  *idMascota : INT <<PK>>
  --
  *idPropietario : INT <<FK>>
  *nombre : VARCHAR(100)
  *especie : VARCHAR(50)
  raza : VARCHAR(80)
  *sexo : CHAR(1)
  fechaNacimiento : DATE
  peso : DECIMAL(6,2)
  color : VARCHAR(50)
  *fechaRegistro : DATETIME
}

entity "vacuna" as vacuna {
  *idVacuna : INT <<PK>>
  --
  *nombre : VARCHAR(100) <<UQ>>
  descripcion : VARCHAR(250)
  *especieDestino : VARCHAR(50)
  *intervaloDias : INT
}

entity "cita" as cita {
  *idCita : INT <<PK>>
  --
  *idMascota : INT <<FK>>
  *idVeterinario : INT <<FK>>
  *fechaHora : DATETIME
  *motivo : VARCHAR(250)
  *estado : VARCHAR(20)
  *idUsuarioRegistro : INT <<FK>>
}

entity "consulta" as consulta {
  *idConsulta : INT <<PK>>
  --
  *idMascota : INT <<FK>>
  *idVeterinario : INT <<FK>>
  *fecha : DATETIME
  *motivo : VARCHAR(250)
  *diagnostico : VARCHAR(500)
  tratamiento : VARCHAR(500)
  observaciones : VARCHAR(500)
}

entity "aplicacionVacuna" as aplicacionVacuna {
  *idAplicacion : INT <<PK>>
  --
  *idMascota : INT <<FK>>
  *idVacuna : INT <<FK>>
  *idVeterinario : INT <<FK>>
  *fechaAplicacion : DATE
  proximaDosis : DATE
  observaciones : VARCHAR(250)
}

entity "bitacora" as bitacora {
  *idBitacora : INT <<PK>>
  --
  *fecha : DATETIME
  *nivel : VARCHAR(10)
  nombreUsuario : VARCHAR(50)
  *modulo : VARCHAR(50)
  *mensaje : VARCHAR(500)
  detalle : VARCHAR(MAX)
}

rol ||--o{ rolPermiso
permiso ||--o{ rolPermiso
rol ||--o{ usuario : "tiene"
propietario ||--o{ mascota : "posee"
mascota ||--o{ cita
mascota ||--o{ consulta
mascota ||--o{ aplicacionVacuna
vacuna ||--o{ aplicacionVacuna
usuario ||--o{ cita : "atiende"
usuario ||--o{ cita : "registra"
usuario ||--o{ consulta : "realiza"
usuario ||--o{ aplicacionVacuna : "aplica"
@enduml
```

## 03_arquitectura.puml

```plantuml
@startuml arquitectura
skinparam componentStyle rectangle
left to right direction

package "dashboardVet (Vista)" {
  component "Program" as Program
  component "Base\nFrmBase" as FrmBase
  component "Helpers\nTema, Responsive, Mensajes,\nValidaciones, Entrada, Texto, GridUtil" as Helpers
  component "Controles\nBotonModerno, BotonMenu,\nPanelTarjeta, PanelDegradado" as Controles
  component "ManejadorUIErrores" as MUI
  package "Pantallas" {
    component "Login" as Login
    component "DashBoard / Inicio" as Dash
    component "Propietarios" as Prop
    component "Mascotas" as Masc
    component "Citas" as Citas
    component "Consultas" as Cons
    component "Vacunas / CatalogoVacunas /\nAplicacionVacunas" as Vac
    component "Usuarios" as Usu
    component "Roles" as Roles
    component "Bitacora" as Bit
  }
}

package "Modelos (Datos y seguridad)" {
  component "DALBase" as DALBase
  component "PropietarioDAL\nMascotaDAL\nCitaDAL\nConsultaDAL\nVacunaDAL\nAplicacionVacunaDAL" as DALs
  component "UsuarioDAL\nRolDAL\nPermisosDAL\nLoginDAL\nBitacoraDAL\nDashboardDAL" as DALs2
  component "Conexion" as Conexion
  component "ErroresSistema\nCatalogoErrores / AppException" as Errores
  component "Sesion / Permisos" as Sesion
  component "EncriptadorContrasena\n(BCrypt)" as Bcrypt
  component "Logger" as Logger
}

database "SQL Server\nBD Veterinaria" as BD

Program --> Login
Program --> Dash
Dash --> Prop
Dash --> Masc
Dash --> Citas
Dash --> Cons
Dash --> Vac
Dash --> Usu
Dash --> Roles
Dash --> Bit

Pantallas ..> FrmBase : hereda
Pantallas ..> Helpers
Pantallas ..> Controles
Pantallas ..> MUI : errores
Pantallas --> DALs
Pantallas --> DALs2
Pantallas --> Sesion

DALs --|> DALBase
DALs2 --|> DALBase
DALBase --> Conexion
DALBase ..> Errores : lanza AppException
DALs2 --> Bcrypt
MUI --> Errores
MUI --> Logger
Logger --> Conexion
Conexion --> BD
@enduml
```

## 04_clases_modelos.puml

```plantuml
@startuml clases_modelos
skinparam classAttributeIconSize 0

abstract class DALBase {
  #_conexion : Conexion
  #P(nombre, valor) : SqlParameter
  #Like(nombre, filtro) : SqlParameter
  #Traducir(ex) : Exception
  #Consultar(sql, parametros) : DataTable
  #Ejecutar(sql, parametros) : int
  #Escalar(sql, parametros) : object
  #Existe(sql, parametros) : bool
}

class Conexion {
  +ObtenerConexion() : SqlConnection
}

class ErrorInfo {
  +Codigo : string
  +MensajeUsuario : string
  +DescripcionTecnica : string
}

class CatalogoErrores <<static>> {
  +Obtener(codigo) : ErrorInfo
  +MapearSqlException(numero) : string
}

class AppException {
  +CodigoError : string
  +DetalleError : ErrorInfo
}

class PropietarioDAL {
  +Listar(filtro) : DataTable
  +ExisteDui(dui, idExcluir) : bool
  +Insertar(...)
  +Actualizar(...)
  +Eliminar(idPropietario)
}

class MascotaDAL {
  +Listar(filtro) : DataTable
  +ListarParaCombo() : DataTable
  +ExisteNombreEnPropietario(...) : bool
  +ObtenerBasico(idMascota) : DataRow
  +Insertar(...)
  +Actualizar(...)
  +Eliminar(idMascota)
}

class CitaDAL {
  +Listar(filtro) : DataTable
  +VeterinarioOcupado(...) : bool
  +MascotaTieneCitaEseDia(...) : bool
  +Insertar(...)
  +Actualizar(...)
  +Eliminar(idCita)
}

class ConsultaDAL {
  +Listar(filtro) : DataTable
  +Insertar(...)
  +Actualizar(...)
  +Eliminar(idConsulta)
}

class VacunaDAL {
  +Listar(filtro) : DataTable
  +ListarParaCombo() : DataTable
  +ExisteNombre(nombre, idExcluir) : bool
  +ObtenerEspecieDestino(idVacuna) : string
  +Insertar(...)
  +Actualizar(...)
  +Eliminar(idVacuna)
}

class AplicacionVacunaDAL {
  +Listar(filtro) : DataTable
  +ExisteAplicacion(...) : bool
  +Insertar(...)
  +Actualizar(...)
  +Eliminar(idAplicacion)
}

class UsuarioDAL {
  +Listar(filtro) : DataTable
  +ListarVeterinarios() : DataTable
  +ExisteNombreUsuario(...) : bool
  +ExisteCorreo(...) : bool
  +Insertar(...)
  +Actualizar(...)
  +Eliminar(idUsuario)
}

class RolDAL {
  +Listar() : DataTable
}

class PermisosDAL {
  +Listar() : DataTable
  +PermisosDeRol(idRol) : HashSet<int>
  +Guardar(idRol, nombreRol, ids, codigos)
}

class LoginDAL {
  +Autenticar(usuario, contrasena) : UsuarioSesion
}

class BitacoraDAL {
  +Listar(nivel, filtro) : DataTable
}

class DashboardDAL {
  +Resumen() : DataRow
  +ProximasCitas() : DataTable
}

class UsuarioSesion {
  +IdUsuario : int
  +NombreCompleto : string
  +NombreUsuario : string
  +Correo : string
  +IdRol : int
  +Rol : string
  +Estado : string
  +Permisos : HashSet<string>
}

class Sesion <<static>> {
  +UsuarioActual : UsuarioSesion
  +Iniciar(usuario)
  +Cerrar()
  +Tiene(codigoPermiso) : bool
}

class EncriptadorContrasena <<static>> {
  +Hashear(contrasena) : string
  +Verificar(contrasena, hash) : bool
}

class Logger <<static>> {
  +Info(modulo, mensaje)
  +Error(modulo, ex)
}

DALBase <|-- PropietarioDAL
DALBase <|-- MascotaDAL
DALBase <|-- CitaDAL
DALBase <|-- ConsultaDAL
DALBase <|-- VacunaDAL
DALBase <|-- AplicacionVacunaDAL
DALBase <|-- UsuarioDAL
DALBase <|-- RolDAL
DALBase <|-- PermisosDAL
DALBase <|-- LoginDAL
DALBase <|-- BitacoraDAL
DALBase <|-- DashboardDAL
DALBase --> Conexion
DALBase ..> AppException : lanza
CatalogoErrores --> ErrorInfo
AppException --> ErrorInfo
LoginDAL ..> UsuarioSesion : crea
LoginDAL ..> EncriptadorContrasena
UsuarioDAL ..> EncriptadorContrasena
Sesion o-- UsuarioSesion
Logger ..> Conexion
@enduml
```
