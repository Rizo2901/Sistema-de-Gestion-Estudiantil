CREATE TABLE [dbo].[Actividades] (
    [IdActividad] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Titulo]      VARCHAR (255)    NOT NULL,
    [Descripcion] TEXT             NULL,
    [Fecha]       DATE             NOT NULL,
    [Tipo]        VARCHAR (15)     NOT NULL,
    PRIMARY KEY CLUSTERED ([IdActividad] ASC)
);

