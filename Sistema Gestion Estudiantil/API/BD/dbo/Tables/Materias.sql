CREATE TABLE [dbo].[Materias] (
    [IdMateria]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]      VARCHAR (100)    NOT NULL,
    [Descripcion] TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdMateria] ASC)
);

