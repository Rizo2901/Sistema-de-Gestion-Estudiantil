CREATE TABLE [dbo].[ReunionesAdministrativas] (
    [IdReunionAdmin] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Fecha]          DATETIME         NOT NULL,
    [Tema]           VARCHAR (255)    NOT NULL,
    [Descripcion]    TEXT             NULL,
    [Lugar]          VARCHAR (100)    NULL,
    [Responsable]    UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([IdReunionAdmin] ASC),
    FOREIGN KEY ([Responsable]) REFERENCES [dbo].[Usuarios] ([IdUsuario]) ON DELETE SET NULL
);

