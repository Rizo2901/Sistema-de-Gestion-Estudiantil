CREATE TABLE [dbo].[Usuarios] (
    [IdUsuario]         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [NombreUsuario]     VARCHAR (50)     NOT NULL,
    [Contrasena]        VARCHAR (255)    NOT NULL,
    [CorreoElectronico] VARCHAR (100)    NULL,
    [IdRol]             UNIQUEIDENTIFIER NOT NULL,
    [Estado]            BIT              DEFAULT ((0)) NULL,
    [FechaActivacion]   DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles] ([IdRol]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([NombreUsuario] ASC)
);

