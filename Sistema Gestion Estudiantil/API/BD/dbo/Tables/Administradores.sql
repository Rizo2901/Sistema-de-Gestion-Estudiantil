CREATE TABLE [dbo].[Administradores] (
    [IdAdministrador]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]            VARCHAR (25)     NOT NULL,
    [Apellido1]         VARCHAR (25)     NOT NULL,
    [Apellido2]         VARCHAR (25)     NOT NULL,
    [Direccion]         VARCHAR (MAX)    NOT NULL,
    [TelefonoCelular]   VARCHAR (25)     NULL,
    [CorreoElectronico] VARCHAR (100)    NULL,
    [IdUsuario]         UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([IdAdministrador] ASC),
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
);

