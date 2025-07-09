CREATE TABLE [dbo].[CondicionesEspeciales] (
    [IdCondicion]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]     UNIQUEIDENTIFIER NOT NULL,
    [IdCategoria]      UNIQUEIDENTIFIER NOT NULL,
    [FechaDiagnostico] DATE             NULL,
    [Observaciones]    TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdCondicion] ASC),
    FOREIGN KEY ([IdCategoria]) REFERENCES [dbo].[CategoriasCondicionEspecial] ([IdCategoria]) ON DELETE CASCADE,
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

