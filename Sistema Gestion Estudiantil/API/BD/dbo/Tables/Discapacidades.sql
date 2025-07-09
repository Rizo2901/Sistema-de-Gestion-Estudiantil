CREATE TABLE [dbo].[Discapacidades] (
    [IdDiscapacidad] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdCategoria]    UNIQUEIDENTIFIER NOT NULL,
    [Nombre]         VARCHAR (100)    NOT NULL,
    [Descripcion]    TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdDiscapacidad] ASC),
    FOREIGN KEY ([IdCategoria]) REFERENCES [dbo].[CategoriasDiscapacidad] ([IdCategoria])
);

