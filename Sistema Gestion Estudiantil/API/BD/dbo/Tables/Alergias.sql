CREATE TABLE [dbo].[Alergias] (
    [IdAlergia]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdCategoria] UNIQUEIDENTIFIER NOT NULL,
    [Nombre]      VARCHAR (100)    NOT NULL,
    [Descripcion] TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdAlergia] ASC),
    FOREIGN KEY ([IdCategoria]) REFERENCES [dbo].[CategoriasAlergia] ([IdCategoria])
);

