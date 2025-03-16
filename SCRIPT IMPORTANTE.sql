
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
		TipoIdentificacion TINYINT NOT NULL,
		FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
		Ip NVARCHAR(15) NOT NULL DEFAULT('0.0.0.0'),
		Estado BIT NOT NULL DEFAULT(1),
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
go;

create or alter procedure SEL_UsuarioGetAll
as
begin
select 
	IdUsuario,
	Identificador,
	Usuario,
	Rol,
	FechaCreacion,
	Ip
	from Usuario where Estado = 1
end
go;

create or alter procedure SEL_PersonasGetAll
as
begin
select 
	IdPersona,
	Nombres,
	Apellidos,
	NumeroIdentificacion,
	Email,
	TipoIdentificacion,
	FechaCreacion,
	Ip,
	NumeroIdentificacionCompleto,
	NombreCompleto
	from Personas where Estado = 1
end
go;

create or alter procedure SEL_UsuarioGetXId
	@IdUsuario int
as
begin
select 
	IdUsuario,
	Identificador,
	Usuario,
	Rol,
	FechaCreacion,
	Ip
	from Usuario where IdUsuario = @IdUsuario and Estado = 1
end
go

create or alter procedure SEL_PersonaGetXId
	@IdPersona int
as
begin
select 
	IdPersona,
	Nombres,
	Apellidos,
	NumeroIdentificacion,
	Email,
	TipoIdentificacion,
	FechaCreacion,
	Ip,
	NumeroIdentificacionCompleto,
	NombreCompleto
	from Personas where IdPersona = @IdPersona
end
go


CREATE or ALTER PROCEDURE sp_Usuario_CRUD
(
    @Opcion NVARCHAR(10),
    @IdUsuario INT = NULL,
    @Identificador NVARCHAR(10),
    @Usuario NVARCHAR(50),
    @Rol TINYINT,
    @FechaCreacion DATETIME = NULL,
    @Ip NVARCHAR(15),
    @Error BIT OUTPUT,
    @Mensaje NVARCHAR(255) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Error = 0;
    SET @Mensaje = N'Success';

    IF @Opcion = 'Insert'
    BEGIN
        IF EXISTS (SELECT 1 FROM Usuario WHERE Identificador = @Identificador AND Estado = 1)
        BEGIN
            SET @Error = 1;
            SET @Mensaje = N'El registro ya existe con Identificador activo.';
            RETURN;
        END

        INSERT INTO Usuario (Identificador, Usuario, Rol, FechaCreacion, Ip, Estado)
        VALUES (@Identificador, @Usuario, @Rol, GETDATE(), @Ip, 1);

        SET @Mensaje = N'Registro insertado correctamente.';
    END
    ELSE IF @Opcion = 'Update'
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdUsuario)
        BEGIN
            SET @Error = 1;
            SET @Mensaje = N'El registro no existe.';
            RETURN;
        END

        UPDATE Usuario
        SET Identificador = @Identificador,
            Usuario = @Usuario,
            Rol = @Rol,
            Ip = @Ip
        WHERE IdUsuario = @IdUsuario;

        SET @Mensaje = N'Registro actualizado correctamente.';
    END
    ELSE IF @Opcion = 'Delete'
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdUsuario)
        BEGIN
            SET @Error = 1;
            SET @Mensaje = N'El registro no existe.';
            RETURN;
        END

        UPDATE Usuario
        SET Estado = 0
        WHERE IdUsuario = @IdUsuario;

        SET @Mensaje = N'Registro eliminado correctamente.';
    END
    ELSE
    BEGIN
        SET @Error = 1;
        SET @Mensaje = N'La opción proporcionada no es válida.';
    END
END
GO


------------------------

CREATE OR ALTER PROCEDURE sp_Persona_CRUD
(
    @Opcion NVARCHAR(10),
    @IdPersona INT = NULL,
    @Nombres NVARCHAR(50),
    @Apellidos NVARCHAR(50),
    @NumeroIdentificacion NVARCHAR(20),
    @Email NVARCHAR(50),
    @TipoIdentificacion TINYINT,
    @Ip NVARCHAR(15),
    @Error BIT OUTPUT,
    @Mensaje NVARCHAR(255) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Existe BIT;

    SELECT @Existe = CASE WHEN EXISTS (
        SELECT 1 FROM Personas 
        WHERE NumeroIdentificacion = @NumeroIdentificacion AND IdPersona != ISNULL(@IdPersona, 0)
    ) THEN 1 ELSE 0 END;

    IF (@Opcion = 'Insert' AND @Existe = 1)
    BEGIN
        SET @Error = 1;
        SET @Mensaje = 'El número de identificación ya existe.';
        RETURN;
    END

    IF (@Opcion = 'Update' AND @Existe = 1)
    BEGIN
        SET @Error = 1;
        SET @Mensaje = 'El número de identificación ya existe en otro registro.';
        RETURN;
    END

    IF (@Opcion = 'Insert')
    BEGIN
        INSERT INTO Personas (Nombres, Apellidos, NumeroIdentificacion, Email, TipoIdentificacion, Ip)
        VALUES (@Nombres, @Apellidos, @NumeroIdentificacion, @Email, @TipoIdentificacion, @Ip);

        SET @Error = 0;
        SET @Mensaje = 'Persona registrada correctamente.';
        RETURN;
    END

    IF (@Opcion = 'Update')
    BEGIN
        UPDATE Personas
        SET Nombres = @Nombres,
            Apellidos = @Apellidos,
            NumeroIdentificacion = @NumeroIdentificacion,
            Email = @Email,
            TipoIdentificacion = @TipoIdentificacion,
            Ip = @Ip
        WHERE IdPersona = @IdPersona;

        SET @Error = 0;
        SET @Mensaje = 'Persona actualizada correctamente.';
        RETURN;
    END

    IF (@Opcion = 'Delete')
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM Personas WHERE IdPersona = @IdPersona)
        BEGIN
            SET @Error = 1;
            SET @Mensaje = 'El registro no existe.';
            RETURN;
        END

        UPDATE Personas SET Estado = 0 WHERE IdPersona = @IdPersona;

        SET @Error = 0;
        SET @Mensaje = 'Persona eliminada correctamente.';
        RETURN;
    END

    SET @Error = 1;
    SET @Mensaje = 'Opción no válida.';
END;
