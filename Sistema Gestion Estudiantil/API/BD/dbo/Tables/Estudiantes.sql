CREATE TABLE [dbo].[Estudiantes] (
    [IdEstudiante]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Cedula]             VARCHAR (20)     NOT NULL,
    [Nombre]             VARCHAR (255)    NOT NULL,
    [Apellido1]          VARCHAR (255)    NOT NULL,
    [Apellido2]          VARCHAR (255)    NOT NULL,
    [FechaNacimiento]    DATE             NOT NULL,
    [Genero]             VARCHAR (10)     NOT NULL,
    [GradoActual]        VARCHAR (20)     NOT NULL,
    [Repitiendo]         BIT              DEFAULT ((0)) NULL,
    [HistorialAcademico] TEXT             NULL,
    [Fotografia]         VARCHAR (MAX)    NULL,
    [Direccion]          TEXT             NULL,
    [TelefonoCelular]    VARCHAR (20)     NULL,
    [CorreoElectronico]  VARCHAR (100)    NULL,
    [Estado]             BIT              DEFAULT ((1)) NULL,
    [IdSeccion]          UNIQUEIDENTIFIER NULL,
    [IdUsuario]          UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([IdEstudiante] ASC),
    FOREIGN KEY ([IdSeccion]) REFERENCES [dbo].[Secciones] ([IdSeccion]) ON DELETE SET NULL,
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]) ON DELETE SET NULL,
    UNIQUE NONCLUSTERED ([Cedula] ASC)
);

