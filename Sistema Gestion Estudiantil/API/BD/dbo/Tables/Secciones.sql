CREATE TABLE [dbo].[Secciones] (
    [IdSeccion] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]    VARCHAR (50)     NOT NULL,
    PRIMARY KEY CLUSTERED ([IdSeccion] ASC),
    UNIQUE NONCLUSTERED ([Nombre] ASC)
);

