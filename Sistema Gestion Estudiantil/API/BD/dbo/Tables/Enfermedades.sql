CREATE TABLE [dbo].[Enfermedades] (
    [IdEnfermedad] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]       VARCHAR (100)    NOT NULL,
    [Descripcion]  TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdEnfermedad] ASC)
);

