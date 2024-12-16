
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DBPrueba')
BEGIN
    create database DBPrueba;
END;

Use DBPrueba

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Personas')
BEGIN
	CREATE TABLE Personas (
		IdPersona INT IDENTITY(1,1) CONSTRAINT PK_Persona_IdPersona PRIMARY KEY,
		Nombres NVARCHAR(100) NOT NULL,
		Apellidos NVARCHAR(100) NOT NULL,
		NumeroIdentificacion NVARCHAR(50) NOT NULL,
		Email NVARCHAR(255) NOT NULL,
		TipoIdentificacion NVARCHAR(50) NOT NULL,
		FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
		Ip NVARCHAR(15) NOT NULL DEFAULT('0.0.0.0'),
		NumeroIdentificacionCompleto AS (TipoIdentificacion + '-' + NumeroIdentificacion) PERSISTED,
		NombreCompleto AS (Nombres + ' ' + Apellidos) PERSISTED
	);
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuario')
BEGIN
	CREATE TABLE Usuario (
		IdUsuario INT IDENTITY(1,1) CONSTRAINT PK_Usuario_Idusuario PRIMARY KEY, -- Aquí defines el nombre de la constraint
		Identificador NVARCHAR(10) NOT NULL UNIQUE,
		Usuario NVARCHAR(50) NOT NULL,
		Pass NVARCHAR(255) NOT NULL,
		Rol TINYINT NOT NULL,
		FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
		Ip NVARCHAR(15) NOT NULL DEFAULT('0.0.0.0'),
		Estado BIT NOT NULL DEFAULT(1)
	);
END
GO

CREATE OR ALTER PROCEDURE sp_GetAllPersonas
AS
BEGIN
    SELECT 
        IdPersona,
        Nombres,
        Apellidos,
        NumeroIdentificacion,
        Email,
        TipoIdentificacion,
        FechaCreacion,
        NumeroIdentificacionCompleto,
        NombreCompleto
    FROM Personas;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetPersonaByNumeroIdentificacion
    @NumeroIdentificacion NVARCHAR(50)
AS
BEGIN
    SELECT 
        IdPersona,
        Nombres,
        Apellidos,
        NumeroIdentificacion,
        Email,
        TipoIdentificacion,
        FechaCreacion,
        NumeroIdentificacionCompleto,
        NombreCompleto
    FROM Personas
    WHERE NumeroIdentificacion = @NumeroIdentificacion;
END;
GO


-------------------------------------
-------------------------------------
--SE QUE SE PUEDE PONER DATOS DE AUDITORIA.
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes AS i
    INNER JOIN sys.objects AS o
        ON i.object_id = o.object_id
    WHERE i.name = 'IX_Usuario_Identificador_Activo'
      AND o.name = 'Usuario'
)
BEGIN
    CREATE UNIQUE INDEX IX_Usuario_Identificador_Activo
    ON Usuario(Identificador)
    WHERE Estado = 1;
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Usuario
    WHERE Identificador = '001'
)
BEGIN
    INSERT INTO Usuario (Identificador, Usuario, Pass, Rol, Estado, Ip)
    VALUES ('001', 'admin', 'admin', 1, 1, '192.168.1.1');
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Usuario
    WHERE Identificador = '002'
)
BEGIN
	INSERT INTO Usuario (Identificador, Usuario, Pass, Rol, Estado, Ip)
	VALUES ('002', 'usu1', 'usu1',2, 1, '192.168.1.2');
END
GO

--exec AuthenticateUser 'admin', 'admin'

CREATE or ALTER PROCEDURE AuthenticateUser
    @UserName NVARCHAR(50),
    @Pass NVARCHAR(50)
AS
BEGIN
    -- Lógica de autenticación
    IF EXISTS (SELECT 1 FROM Usuario WHERE Usuario = @UserName AND Pass = @Pass AND Estado = 1)
        SELECT 
				Identificador as 'Identificacion',
				Usuario,
				Rol
				FROM Usuario where Usuario = @UserName and Pass = @Pass AND Estado = 1
    ELSE
        SELECT '' as Identificacion, '' as Usuario, 0 as Rol
END

