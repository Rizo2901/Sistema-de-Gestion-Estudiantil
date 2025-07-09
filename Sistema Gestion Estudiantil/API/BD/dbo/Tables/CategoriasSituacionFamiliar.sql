CREATE TABLE [dbo].[CategoriasSituacionFamiliar] (
    [IdCategoria] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]      VARCHAR (100)    NOT NULL,
    [Descripcion] TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdCategoria] ASC),
    UNIQUE NONCLUSTERED ([Nombre] ASC)
);

