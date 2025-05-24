CREATE TABLE [dbo].[Roles] (
    [IdRol]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]      VARCHAR (50)     NOT NULL,
    [Descripcion] VARCHAR (255)    NULL,
    PRIMARY KEY CLUSTERED ([IdRol] ASC),
    UNIQUE NONCLUSTERED ([Nombre] ASC)
);

