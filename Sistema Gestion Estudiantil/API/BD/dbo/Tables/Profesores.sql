CREATE TABLE [dbo].[Profesores] (
    [IdProfesor]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]            VARCHAR (255)    NOT NULL,
    [Apellido1]         VARCHAR (255)    NOT NULL,
    [Apellido2]         VARCHAR (255)    NOT NULL,
    [Direccion]         VARCHAR (MAX)    NOT NULL,
    [TelefonoCelular]   VARCHAR (20)     NULL,
    [CorreoElectronico] VARCHAR (100)    NULL,
    [IdUsuario]         UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([IdProfesor] ASC),
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
);

