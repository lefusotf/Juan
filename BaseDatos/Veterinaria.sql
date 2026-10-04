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
--   Administradores (admin, jperez)             contraseña: Admin123*
--   Veterinarios (dvera, rmartinez, lhernandez,
--                 cgarcia, mflores, abonilla)   contraseña: Vet12345*
--   Recepcionistas (recep1, recep2, recep3)     contraseña: Recep123*
INSERT INTO usuario (nombreCompleto, nombreUsuario, contrasena, correo, idRol, estado) VALUES
('Administrador del Sistema', 'admin', '$2b$11$erHlHfyxnw2i5LRT1144ZOxMZCca0AlImZxmYqLaoGK8YE5WXIrxS', 'admin@veterinaria.com', 1, 'Activo'),
('Dra. Daniela Vera', 'dvera', '$2b$11$1m4t7ZWQCCC7XE0G5fP3vOxzX5eOYkbrnSDIY0Pi1vqVp0JKCRYXC', 'dvera@veterinaria.com', 2, 'Activo'),
('Ana Morales', 'recep1', '$2b$11$e0P3J4A0dpHauZLeoaO8KeX6mIErbvgIpRLaYkZvBYy1nbU3bPbgu', 'amorales@veterinaria.com', 3, 'Activo'),
('Dr. Ricardo Martínez', 'rmartinez', '$2b$11$1m4t7ZWQCCC7XE0G5fP3vOxzX5eOYkbrnSDIY0Pi1vqVp0JKCRYXC', 'rmartinez@veterinaria.com', 2, 'Activo'),
('Dra. Lucía Hernández', 'lhernandez', '$2b$11$1m4t7ZWQCCC7XE0G5fP3vOxzX5eOYkbrnSDIY0Pi1vqVp0JKCRYXC', 'lhernandez@veterinaria.com', 2, 'Activo'),
('Dr. Carlos García', 'cgarcia', '$2b$11$1m4t7ZWQCCC7XE0G5fP3vOxzX5eOYkbrnSDIY0Pi1vqVp0JKCRYXC', 'cgarcia@veterinaria.com', 2, 'Activo'),
('Dra. Mariana Flores', 'mflores', '$2b$11$1m4t7ZWQCCC7XE0G5fP3vOxzX5eOYkbrnSDIY0Pi1vqVp0JKCRYXC', 'mflores@veterinaria.com', 2, 'Activo'),
('Dr. Andrés Bonilla', 'abonilla', '$2b$11$1m4t7ZWQCCC7XE0G5fP3vOxzX5eOYkbrnSDIY0Pi1vqVp0JKCRYXC', 'abonilla@veterinaria.com', 2, 'Activo'),
('Karla Menjívar', 'recep2', '$2b$11$e0P3J4A0dpHauZLeoaO8KeX6mIErbvgIpRLaYkZvBYy1nbU3bPbgu', 'kmenjivar@veterinaria.com', 3, 'Activo'),
('Luis Alberto Cruz', 'recep3', '$2b$11$e0P3J4A0dpHauZLeoaO8KeX6mIErbvgIpRLaYkZvBYy1nbU3bPbgu', 'lcruz@veterinaria.com', 3, 'Activo'),
('Jorge Pérez', 'jperez', '$2b$11$erHlHfyxnw2i5LRT1144ZOxMZCca0AlImZxmYqLaoGK8YE5WXIrxS', 'jperez@veterinaria.com', 1, 'Activo');

INSERT INTO propietario (nombre, dui, telefono, correo, direccion) VALUES
('Carlos Mendoza', '01234567-8', '7890-1234', 'carlos.mendoza@correo.com', 'Col. Escalón, San Salvador'),
('María Rivas', '02345678-9', '7890-2345', 'maria.rivas@correo.com', 'Santa Tecla, La Libertad'),
('José Gómez', '03456789-0', '7890-3456', NULL, 'Soyapango, San Salvador'),
('Ana Lucía Martínez', '04567890-1', '7890-4567', 'ana.martinez@correo.com', 'Col. Médica, San Salvador'),
('Roberto López', '05678901-2', '7890-5678', 'roberto.lopez@correo.com', 'Antiguo Cuscatlán, La Libertad'),
('Sofía Hernández', '06789012-3', '6012-3456', 'sofia.hernandez@correo.com', 'Mejicanos, San Salvador'),
('Diego Torres', '07890123-4', '6123-4567', NULL, 'Apopa, San Salvador'),
('Gabriela Flores', '08901234-5', '7234-5678', 'gabriela.flores@correo.com', 'Ilopango, San Salvador'),
('Fernando Ramírez', '09012345-6', '7345-6789', 'fernando.ramirez@correo.com', 'San Miguel, San Miguel'),
('Patricia Vásquez', '10123456-7', '2245-6789', 'patricia.vasquez@correo.com', 'Santa Ana, Santa Ana'),
('Jorge Castillo', '11234567-8', '6456-7890', NULL, 'Cuscatancingo, San Salvador'),
('Claudia Aguilar', '12345678-9', '7567-8901', 'claudia.aguilar@correo.com', 'Nueva San Salvador, La Libertad');

INSERT INTO mascota (idPropietario, nombre, especie, raza, sexo, fechaNacimiento, peso, color) VALUES
(1, 'Firulais', 'Perro', 'Labrador', 'M', '2021-03-15', 28.50, 'Dorado'),
(1, 'Michi', 'Gato', 'Siamés', 'H', '2022-07-02', 4.20, 'Crema'),
(2, 'Luna', 'Perro', 'Poodle', 'H', '2020-11-20', 6.80, 'Blanco'),
(3, 'Rocky', 'Perro', 'Bulldog', 'M', '2019-05-10', 22.00, 'Café'),
(4, 'Pelusa', 'Gato', 'Persa', 'H', '2021-09-12', 3.90, 'Gris'),
(5, 'Max', 'Perro', 'Pastor Alemán', 'M', '2020-02-28', 32.40, 'Negro'),
(6, 'Coco', 'Perro', 'Chihuahua', 'M', '2022-01-18', 2.80, 'Café'),
(7, 'Nala', 'Gato', 'Mestizo', 'H', '2023-04-05', 3.50, 'Naranja'),
(8, 'Bella', 'Perro', 'Golden Retriever', 'H', '2021-12-01', 26.00, 'Dorado'),
(9, 'Thor', 'Perro', 'Rottweiler', 'M', '2019-08-22', 41.00, 'Negro'),
(10, 'Canela', 'Conejo', 'Cabeza de León', 'H', '2023-06-14', 1.60, 'Café'),
(11, 'Kiwi', 'Ave', 'Periquito', 'M', '2024-02-10', 0.05, 'Verde'),
(12, 'Simba', 'Gato', 'Maine Coon', 'M', '2022-10-30', 6.30, 'Atigrado'),
(2, 'Toby', 'Perro', 'Beagle', 'M', '2018-07-07', 12.10, 'Tricolor'),
(6, 'Mimi', 'Gato', 'Angora', 'H', '2020-05-25', 4.00, 'Blanco');

INSERT INTO vacuna (nombre, descripcion, especieDestino, intervaloDias) VALUES
('Antirrábica', 'Protección contra la rabia', 'Perro', 365),
('Antirrábica Felina', 'Protección contra la rabia en gatos', 'Gato', 365),
('Parvovirus', 'Protección contra parvovirus canino', 'Perro', 365),
('Triple Felina', 'Panleucopenia, rinotraqueítis y calicivirus', 'Gato', 365),
('Bordetella', 'Tos de las perreras', 'Perro', 180),
('Moquillo Canino', 'Protección contra el moquillo', 'Perro', 365),
('Leptospirosis', 'Protección contra leptospira', 'Perro', 365),
('Hepatitis Canina', 'Protección contra adenovirus canino', 'Perro', 365),
('Leucemia Felina', 'Protección contra el virus de leucemia felina', 'Gato', 365),
('Rinotraqueítis', 'Herpesvirus felino', 'Gato', 365),
('Mixomatosis', 'Protección contra mixomatosis en conejos', 'Conejo', 180),
('Newcastle', 'Protección contra la enfermedad de Newcastle', 'Ave', 120);

INSERT INTO cita (idMascota, idVeterinario, fechaHora, motivo, estado, idUsuarioRegistro) VALUES
(1, 2, DATEADD(DAY, 1, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '09:00', 'Control general', 'Programada', 3),
(3, 2, DATEADD(DAY, 2, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '10:30', 'Vacunación anual', 'Programada', 3),
(5, 4, DATEADD(DAY, 1, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '08:30', 'Revisión de la piel', 'Programada', 9),
(6, 5, DATEADD(DAY, 3, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '14:00', 'Cojera en pata delantera', 'Programada', 9),
(7, 6, DATEADD(DAY, 2, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '11:00', 'Limpieza dental', 'Programada', 10),
(8, 7, DATEADD(DAY, 4, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '09:30', 'Esterilización', 'Programada', 3),
(9, 8, DATEADD(DAY, 5, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '15:30', 'Control de peso', 'Programada', 9),
(10, 4, DATEADD(DAY, 6, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '10:00', 'Vacuna antirrábica', 'Programada', 10),
(11, 5, DATEADD(DAY, 7, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '16:00', 'Revisión de dientes', 'Programada', 3),
(13, 6, DATEADD(DAY, 3, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '13:00', 'Desparasitación', 'Programada', 9),
(4, 2, DATEADD(DAY, -2, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '09:00', 'Control de gastritis', 'Atendida', 3),
(2, 7, DATEADD(DAY, -1, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) + '11:30', 'Vómitos frecuentes', 'Cancelada', 10);

INSERT INTO consulta (idMascota, idVeterinario, fecha, motivo, diagnostico, tratamiento, observaciones) VALUES
(1, 2, '2026-08-10 09:00', 'Cojera en pata trasera', 'Esguince leve', 'Antiinflamatorio por 5 días y reposo', 'Revisar en 2 semanas'),
(4, 2, '2026-09-05 11:00', 'Pérdida de apetito', 'Gastritis', 'Dieta blanda y protector gástrico', NULL),
(2, 4, '2026-08-20 10:00', 'Estornudos y secreción nasal', 'Infección respiratoria leve', 'Antibiótico por 7 días', 'Mantener en ambiente cálido'),
(3, 5, '2026-08-25 15:30', 'Picazón constante', 'Dermatitis alérgica', 'Shampoo medicado y antihistamínico', 'Evitar cambio de alimento'),
(5, 6, '2026-09-01 09:15', 'Caída de pelo', 'Hongos en la piel', 'Antifúngico tópico por 3 semanas', NULL),
(6, 7, '2026-09-03 12:00', 'Control anual', 'Paciente sano', 'Ninguno', 'Peso adecuado'),
(7, 8, '2026-09-08 10:45', 'Mal aliento', 'Sarro dental grado 2', 'Limpieza dental programada', NULL),
(9, 2, '2026-09-12 16:00', 'Diarrea', 'Parasitosis intestinal', 'Desparasitante y suero oral', 'Control en 10 días'),
(10, 4, '2026-09-15 11:30', 'Herida en la oreja', 'Otitis externa', 'Gotas óticas por 10 días', NULL),
(11, 5, '2026-09-18 14:20', 'Desgano', 'Problema digestivo leve', 'Aumentar fibra y agua', 'Revisar en una semana'),
(12, 6, '2026-09-22 09:40', 'Plumas erizadas', 'Estrés por cambio de ambiente', 'Reposo y ambiente tranquilo', NULL),
(13, 7, '2026-09-28 13:10', 'Cojera leve', 'Contusión sin fractura', 'Reposo y analgésico por 3 días', 'Radiografía normal');

INSERT INTO aplicacionVacuna (idMascota, idVacuna, idVeterinario, fechaAplicacion, proximaDosis, observaciones) VALUES
(1, 1, 2, '2026-03-15', DATEADD(DAY, 365, '2026-03-15'), 'Sin reacciones'),
(3, 3, 2, '2026-04-20', DATEADD(DAY, 365, '2026-04-20'), NULL),
(2, 2, 4, '2026-05-10', DATEADD(DAY, 365, '2026-05-10'), NULL),
(4, 5, 5, '2026-05-22', DATEADD(DAY, 180, '2026-05-22'), 'Leve molestia en el sitio'),
(5, 4, 6, '2026-06-02', DATEADD(DAY, 365, '2026-06-02'), NULL),
(6, 1, 7, '2026-06-15', DATEADD(DAY, 365, '2026-06-15'), NULL),
(7, 6, 8, '2026-07-01', DATEADD(DAY, 365, '2026-07-01'), 'Primera dosis'),
(8, 10, 4, '2026-07-12', DATEADD(DAY, 365, '2026-07-12'), NULL),
(9, 7, 5, '2026-07-25', DATEADD(DAY, 365, '2026-07-25'), NULL),
(10, 8, 6, '2026-08-05', DATEADD(DAY, 365, '2026-08-05'), NULL),
(11, 11, 7, '2026-08-18', DATEADD(DAY, 180, '2026-08-18'), NULL),
(13, 9, 8, '2026-09-02', DATEADD(DAY, 365, '2026-09-02'), 'Sin reacciones'),
(12, 12, 6, '2026-09-10', DATEADD(DAY, 120, '2026-09-10'), NULL);

INSERT INTO bitacora (fecha, nivel, nombreUsuario, modulo, mensaje) VALUES
('2026-09-28 08:01', 'INFO', 'admin', 'Login', 'Inicio de sesión correcto (rol Administrador)'),
('2026-09-28 08:15', 'INFO', 'admin', 'Usuarios', 'Registro creado (id 0)'),
('2026-09-28 09:02', 'INFO', 'recep1', 'Login', 'Inicio de sesión correcto (rol Recepcionista)'),
('2026-09-28 09:10', 'INFO', 'recep1', 'Propietarios', 'Registro creado (id 0)'),
('2026-09-28 09:25', 'INFO', 'recep1', 'Mascotas', 'Registro creado (id 0)'),
('2026-09-28 10:00', 'INFO', 'recep2', 'Citas', 'Registro creado (id 0)'),
('2026-09-28 11:30', 'INFO', 'dvera', 'Login', 'Inicio de sesión correcto (rol Veterinario)'),
('2026-09-28 11:45', 'INFO', 'dvera', 'Consultas', 'Registro creado (id 0)'),
('2026-09-28 12:05', 'INFO', 'rmartinez', 'Vacunas', 'Registro creado (id 0)'),
('2026-09-28 13:20', 'ERROR', 'recep3', 'Propietarios', 'Ya existe un registro con esos datos únicos'),
('2026-09-29 08:00', 'INFO', 'jperez', 'Roles', 'Permisos actualizados para el rol Recepcionista');
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
