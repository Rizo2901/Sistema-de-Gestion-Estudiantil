CREATE TABLE [dbo].[Institucion] (
    [IdInstitucion] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]        VARCHAR (255)    NOT NULL,
    [Direccion]     TEXT             NOT NULL,
    [Telefono]      VARCHAR (20)     NOT NULL,
    [Correo]        VARCHAR (100)    NOT NULL,
    PRIMARY KEY CLUSTERED ([IdInstitucion] ASC)
);

