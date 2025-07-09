CREATE TABLE [dbo].[SituacionFamiliar] (
    [IdSituacion]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]  UNIQUEIDENTIFIER NOT NULL,
    [IdCategoria]   UNIQUEIDENTIFIER NOT NULL,
    [Descripcion]   TEXT             NULL,
    [FechaRegistro] DATE             DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([IdSituacion] ASC),
    FOREIGN KEY ([IdCategoria]) REFERENCES [dbo].[CategoriasSituacionFamiliar] ([IdCategoria]) ON DELETE CASCADE,
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

