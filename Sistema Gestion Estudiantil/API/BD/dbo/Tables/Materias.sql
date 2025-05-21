CREATE TABLE [dbo].[Materias] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [nombre]      VARCHAR (100) NOT NULL,
    [descripcion] TEXT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

