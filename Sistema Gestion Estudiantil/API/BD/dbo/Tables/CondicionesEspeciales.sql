CREATE TABLE [dbo].[CondicionesEspeciales] (
    [IdCondicion]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]     UNIQUEIDENTIFIER NOT NULL,
    [TipoCondicion]    VARCHAR (50)     NOT NULL,
    [NombreCondicion]  VARCHAR (100)    NOT NULL,
    [FechaDiagnostico] DATE             NULL,
    [Observaciones]    TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdCondicion] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

