CREATE TABLE [dbo].[Familiares] (
    [IdFamiliar]         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]       UNIQUEIDENTIFIER NOT NULL,
    [Nombre]             VARCHAR (255)    NOT NULL,
    [Apellido1]          VARCHAR (255)    NOT NULL,
    [Apellido2]          VARCHAR (255)    NOT NULL,
    [Direccion]          VARCHAR (MAX)    NOT NULL,
    [Parentesco]         VARCHAR (10)     NOT NULL,
    [TelefonoCelular]    VARCHAR (20)     NULL,
    [CorreoElectronico]  VARCHAR (100)    NULL,
    [Ocupacion]          VARCHAR (100)    NULL,
    [EstadoCivil]        VARCHAR (10)     NOT NULL,
    [CantidadHijos]      INT              DEFAULT ((0)) NULL,
    [IdUsuario]          UNIQUEIDENTIFIER NULL,
    [ViveConEstudiante]  BIT              DEFAULT ((0)) NULL,
    [EsResponsableLegal] BIT              DEFAULT ((0)) NULL,
    [Cedula]             VARCHAR (40)     NULL,
    PRIMARY KEY CLUSTERED ([IdFamiliar] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE,
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
);

