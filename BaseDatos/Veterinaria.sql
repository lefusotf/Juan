-- =========================================================
-- SISTEMA DE GESTIÓN VETERINARIA
-- Script de base de datos (SQL Server / LocalDB)
-- Contiene: tablas de seguridad (usuarios, roles, permisos),
--           tablas del negocio, bitácora, vistas y datos iniciales.
-- =========================================================

-- =========================================================
-- 1. CREACIÓN DE LA BASE DE DATOS
-- =========================================================
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Veterinaria')
BEGIN
    CREATE DATABASE Veterinaria;
END
GO

USE Veterinaria;
GO

-- =========================================================
-- 2. SEGURIDAD: ROLES, PERMISOS Y USUARIOS
-- =========================================================

-- Roles del sistema (Administrador, Veterinario, Recepcionista)
CREATE TABLE rol (
    idRol INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(200) NULL
);

-- Catálogo de permisos. El campo "codigo" es el que usa la aplicación en C#
CREATE TABLE permiso (
    idPermiso INT IDENTITY(1,1) PRIMARY KEY,
    codigo VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(200) NOT NULL
);

-- Relación muchos a muchos: qué permisos tiene cada rol
CREATE TABLE rolPermiso (
    idRol INT NOT NULL,
    idPermiso INT NOT NULL,
    CONSTRAINT pkRolPermiso PRIMARY KEY (idRol, idPermiso),
    CONSTRAINT fkRolPermisoRol FOREIGN KEY (idRol) REFERENCES rol(idRol) ON DELETE CASCADE,
    CONSTRAINT fkRolPermisoPermiso FOREIGN KEY (idPermiso) REFERENCES permiso(idPermiso) ON DELETE CASCADE
);

-- Usuarios (personal de la clínica). La contraseña se guarda como hash BCrypt, nunca en texto plano
CREATE TABLE usuario (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    nombreCompleto VARCHAR(150) NOT NULL,
    nombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(100) NOT NULL,
    correo VARCHAR(150) NULL,
    idRol INT NOT NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'Activo',
    fechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT fkUsuarioRol FOREIGN KEY (idRol) REFERENCES rol(idRol),
    CONSTRAINT ckUsuarioEstado CHECK (estado IN ('Activo', 'Inactivo'))
);

-- =========================================================
-- 3. ENTIDADES DEL NEGOCIO
-- =========================================================

CREATE TABLE propietario (
    idPropietario INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    dui VARCHAR(10) NOT NULL UNIQUE,
    telefono VARCHAR(15) NOT NULL,
    correo VARCHAR(150) NULL,
    direccion VARCHAR(250) NULL,
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE mascota (
    idMascota INT IDENTITY(1,1) PRIMARY KEY,
    idPropietario INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    especie VARCHAR(50) NOT NULL,
    raza VARCHAR(80) NULL,
    sexo CHAR(1) NOT NULL,
    fechaNacimiento DATE NULL,
    peso DECIMAL(6,2) NULL,
    color VARCHAR(50) NULL,
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT fkMascotaPropietario FOREIGN KEY (idPropietario) REFERENCES propietario(idPropietario),
    CONSTRAINT ckMascotaSexo CHECK (sexo IN ('M', 'H')),
    CONSTRAINT ckMascotaPeso CHECK (peso IS NULL OR peso > 0)
);

CREATE TABLE vacuna (
    idVacuna INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE,
    descripcion VARCHAR(250) NULL,
    especieDestino VARCHAR(50) NOT NULL,
    intervaloDias INT NOT NULL DEFAULT 365,
    CONSTRAINT ckVacunaIntervalo CHECK (intervaloDias > 0)
);

-- Citas médicas agendadas por recepción
CREATE TABLE cita (
    idCita INT IDENTITY(1,1) PRIMARY KEY,
    idMascota INT NOT NULL,
    idVeterinario INT NOT NULL,
    fechaHora DATETIME NOT NULL,
    motivo VARCHAR(250) NOT NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'Programada',
    idUsuarioRegistro INT NOT NULL,
    CONSTRAINT fkCitaMascota FOREIGN KEY (idMascota) REFERENCES mascota(idMascota),
    CONSTRAINT fkCitaVeterinario FOREIGN KEY (idVeterinario) REFERENCES usuario(idUsuario),
    CONSTRAINT fkCitaUsuarioRegistro FOREIGN KEY (idUsuarioRegistro) REFERENCES usuario(idUsuario),
    CONSTRAINT ckCitaEstado CHECK (estado IN ('Programada', 'Atendida', 'Cancelada'))
);

-- Consultas médicas: conforman el historial clínico de cada mascota
CREATE TABLE consulta (
    idConsulta INT IDENTITY(1,1) PRIMARY KEY,
    idMascota INT NOT NULL,
    idVeterinario INT NOT NULL,
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    motivo VARCHAR(250) NOT NULL,
    diagnostico VARCHAR(500) NOT NULL,
    tratamiento VARCHAR(500) NULL,
    observaciones VARCHAR(500) NULL,
    CONSTRAINT fkConsultaMascota FOREIGN KEY (idMascota) REFERENCES mascota(idMascota),
    CONSTRAINT fkConsultaVeterinario FOREIGN KEY (idVeterinario) REFERENCES usuario(idUsuario)
);

-- Vacunas aplicadas a cada mascota
CREATE TABLE aplicacionVacuna (
    idAplicacion INT IDENTITY(1,1) PRIMARY KEY,
    idMascota INT NOT NULL,
    idVacuna INT NOT NULL,
    idVeterinario INT NOT NULL,
    fechaAplicacion DATE NOT NULL DEFAULT GETDATE(),
    proximaDosis DATE NULL,
    observaciones VARCHAR(250) NULL,
    CONSTRAINT fkAplicacionMascota FOREIGN KEY (idMascota) REFERENCES mascota(idMascota),
    CONSTRAINT fkAplicacionVacuna FOREIGN KEY (idVacuna) REFERENCES vacuna(idVacuna),
    CONSTRAINT fkAplicacionVeterinario FOREIGN KEY (idVeterinario) REFERENCES usuario(idUsuario)
);

-- =========================================================
-- 4. BITÁCORA (LOGGING DE ACTIVIDADES Y ERRORES)
-- =========================================================
CREATE TABLE bitacora (
    idBitacora INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    nivel VARCHAR(10) NOT NULL,           -- INFO, ERROR
    nombreUsuario VARCHAR(50) NULL,
    modulo VARCHAR(50) NOT NULL,
    mensaje VARCHAR(500) NOT NULL,
    detalle VARCHAR(MAX) NULL
);
GO

-- =========================================================
-- 5. DATOS INICIALES
-- =========================================================

INSERT INTO rol (nombre, descripcion) VALUES
('Administrador', 'Asigna personal y tiene control general del sistema'),
('Veterinario', 'Actualiza historiales médicos y aplica vacunas'),
('Recepcionista', 'Registra propietarios, mascotas y agenda citas');

INSERT INTO permiso (codigo, descripcion) VALUES
('USUARIOS_GESTIONAR', 'Crear, editar y eliminar usuarios y asignarles rol'),
('ROLES_GESTIONAR', 'Asignar permisos a los roles'),
('BITACORA_VER', 'Consultar la bitácora del sistema'),
('PROPIETARIOS_VER', 'Consultar propietarios'),
('PROPIETARIOS_GESTIONAR', 'Registrar, editar y eliminar propietarios'),
('MASCOTAS_VER', 'Consultar mascotas'),
('MASCOTAS_GESTIONAR', 'Registrar, editar y eliminar mascotas'),
('CITAS_VER', 'Consultar citas'),
('CITAS_GESTIONAR', 'Agendar, editar y cancelar citas'),
('CONSULTAS_VER', 'Consultar historiales médicos'),
('CONSULTAS_GESTIONAR', 'Registrar y actualizar historiales médicos'),
('VACUNAS_VER', 'Consultar vacunas y aplicaciones'),
('VACUNAS_GESTIONAR', 'Administrar catálogo de vacunas y aplicarlas a mascotas');

-- Administrador: todos los permisos
INSERT INTO rolPermiso (idRol, idPermiso)
SELECT r.idRol, p.idPermiso FROM rol r CROSS JOIN permiso p WHERE r.nombre = 'Administrador';

-- Veterinario: consulta datos básicos, gestiona historiales y vacunas
INSERT INTO rolPermiso (idRol, idPermiso)
SELECT r.idRol, p.idPermiso FROM rol r INNER JOIN permiso p ON p.codigo IN
    ('PROPIETARIOS_VER', 'MASCOTAS_VER', 'CITAS_VER',
     'CONSULTAS_VER', 'CONSULTAS_GESTIONAR', 'VACUNAS_VER', 'VACUNAS_GESTIONAR')
WHERE r.nombre = 'Veterinario';

-- Recepcionista: registra propietarios, mascotas y citas
INSERT INTO rolPermiso (idRol, idPermiso)
SELECT r.idRol, p.idPermiso FROM rol r INNER JOIN permiso p ON p.codigo IN
    ('PROPIETARIOS_VER', 'PROPIETARIOS_GESTIONAR', 'MASCOTAS_VER', 'MASCOTAS_GESTIONAR',
     'CITAS_VER', 'CITAS_GESTIONAR')
WHERE r.nombre = 'Recepcionista';

-- Usuarios iniciales (hash BCrypt, factor de costo 11)
--   admin  / Admin123*
--   dvera  / Vet12345*
--   recep1 / Recep123*
INSERT INTO usuario (nombreCompleto, nombreUsuario, contrasena, correo, idRol, estado) VALUES
('Administrador del Sistema', 'admin',  '$2b$11$erHlHfyxnw2i5LRT1144ZOxMZCca0AlImZxmYqLaoGK8YE5WXIrxS', 'admin@veterinaria.com', 1, 'Activo'),
('Dra. Daniela Vera',         'dvera',  '$2b$11$1m4t7ZWQCCC7XE0G5fP3vOxzX5eOYkbrnSDIY0Pi1vqVp0JKCRYXC', 'dvera@veterinaria.com',  2, 'Activo'),
('Ana Morales',               'recep1', '$2b$11$e0P3J4A0dpHauZLeoaO8KeX6mIErbvgIpRLaYkZvBYy1nbU3bPbgu', 'amorales@veterinaria.com', 3, 'Activo');

INSERT INTO propietario (nombre, dui, telefono, correo, direccion) VALUES
('Carlos Mendoza', '01234567-8', '7890-1234', 'carlos.mendoza@correo.com', 'Col. Escalón, San Salvador'),
('María Rivas',    '02345678-9', '7890-2345', 'maria.rivas@correo.com',    'Santa Tecla, La Libertad'),
('José Gómez',     '03456789-0', '7890-3456', NULL,                        'Soyapango, San Salvador');

INSERT INTO mascota (idPropietario, nombre, especie, raza, sexo, fechaNacimiento, peso, color) VALUES
(1, 'Firulais', 'Perro', 'Labrador', 'M', '2021-03-15', 28.50, 'Dorado'),
(1, 'Michi',    'Gato',  'Siamés',   'H', '2022-07-02', 4.20,  'Crema'),
(2, 'Luna',     'Perro', 'Poodle',   'H', '2020-11-20', 6.80,  'Blanco'),
(3, 'Rocky',    'Perro', 'Bulldog',  'M', '2019-05-10', 22.00, 'Café');

INSERT INTO vacuna (nombre, descripcion, especieDestino, intervaloDias) VALUES
('Antirrábica',       'Protección contra la rabia',                  'Perro', 365),
('Antirrábica Felina','Protección contra la rabia en gatos',         'Gato',  365),
('Parvovirus',        'Protección contra parvovirus canino',         'Perro', 365),
('Triple Felina',     'Panleucopenia, rinotraqueítis y calicivirus', 'Gato',  365),
('Bordetella',        'Tos de las perreras',                         'Perro', 180);

INSERT INTO cita (idMascota, idVeterinario, fechaHora, motivo, estado, idUsuarioRegistro) VALUES
(1, 2, DATEADD(DAY, 1, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '09:00', 'Control general', 'Programada', 3),
(3, 2, DATEADD(DAY, 2, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '10:30', 'Vacunación anual', 'Programada', 3);

INSERT INTO consulta (idMascota, idVeterinario, fecha, motivo, diagnostico, tratamiento, observaciones) VALUES
(1, 2, '2026-08-10 09:00', 'Cojera en pata trasera', 'Esguince leve', 'Antiinflamatorio por 5 días y reposo', 'Revisar en 2 semanas'),
(4, 2, '2026-09-05 11:00', 'Pérdida de apetito', 'Gastritis', 'Dieta blanda y protector gástrico', NULL);

INSERT INTO aplicacionVacuna (idMascota, idVacuna, idVeterinario, fechaAplicacion, proximaDosis, observaciones) VALUES
(1, 1, 2, '2026-03-15', '2027-03-15', 'Sin reacciones'),
(3, 3, 2, '2026-04-20', '2027-04-20', NULL);
GO

-- =========================================================
-- 6. VISTAS (consultas con JOIN usadas por la aplicación)
-- =========================================================

CREATE VIEW vw_UsuariosDetalle AS
SELECT u.idUsuario, u.nombreCompleto, u.nombreUsuario, u.correo, u.estado,
       u.idRol, r.nombre AS rol, u.fechaCreacion
FROM usuario u
INNER JOIN rol r ON r.idRol = u.idRol;
GO

CREATE VIEW vw_MascotasDetalle AS
SELECT m.idMascota, m.idPropietario, p.nombre AS propietario, m.nombre, m.especie, m.raza,
       m.sexo, m.fechaNacimiento, m.peso, m.color
FROM mascota m
INNER JOIN propietario p ON p.idPropietario = m.idPropietario;
GO

CREATE VIEW vw_CitasDetalle AS
SELECT c.idCita, c.idMascota, m.nombre AS mascota, p.nombre AS propietario,
       c.idVeterinario, v.nombreCompleto AS veterinario,
       c.fechaHora, c.motivo, c.estado
FROM cita c
INNER JOIN mascota m ON m.idMascota = c.idMascota
INNER JOIN propietario p ON p.idPropietario = m.idPropietario
INNER JOIN usuario v ON v.idUsuario = c.idVeterinario;
GO

CREATE VIEW vw_ConsultasDetalle AS
SELECT c.idConsulta, c.idMascota, m.nombre AS mascota, p.nombre AS propietario,
       c.idVeterinario, v.nombreCompleto AS veterinario,
       c.fecha, c.motivo, c.diagnostico, c.tratamiento, c.observaciones
FROM consulta c
INNER JOIN mascota m ON m.idMascota = c.idMascota
INNER JOIN propietario p ON p.idPropietario = m.idPropietario
INNER JOIN usuario v ON v.idUsuario = c.idVeterinario;
GO

CREATE VIEW vw_AplicacionesDetalle AS
SELECT a.idAplicacion, a.idMascota, m.nombre AS mascota,
       a.idVacuna, vc.nombre AS vacuna,
       a.idVeterinario, v.nombreCompleto AS veterinario,
       a.fechaAplicacion, a.proximaDosis, a.observaciones
FROM aplicacionVacuna a
INNER JOIN mascota m ON m.idMascota = a.idMascota
INNER JOIN vacuna vc ON vc.idVacuna = a.idVacuna
INNER JOIN usuario v ON v.idUsuario = a.idVeterinario;
GO

-- =========================================================
-- 7. PRUEBAS RÁPIDAS
-- =========================================================
-- SELECT * FROM vw_UsuariosDetalle;
-- SELECT r.nombre AS rol, p.codigo FROM rolPermiso rp
--   INNER JOIN rol r ON r.idRol = rp.idRol INNER JOIN permiso p ON p.idPermiso = rp.idPermiso
--   ORDER BY r.nombre, p.codigo;
