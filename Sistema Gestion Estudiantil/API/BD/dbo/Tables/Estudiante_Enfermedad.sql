CREATE TABLE [dbo].[Estudiante_Enfermedad] (
    [IdEstudiante]           UNIQUEIDENTIFIER NOT NULL,
    [IdEnfermedad]           UNIQUEIDENTIFIER NOT NULL,
    [FechaDiagnostico]       DATE             NULL,
    [Gravedad]               VARCHAR (50)     NULL,
    [Observaciones]          TEXT             NULL,
    [IdEstudianteEnfermedad] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_Estudiante_Enfermedad] PRIMARY KEY CLUSTERED ([IdEstudianteEnfermedad] ASC),
    FOREIGN KEY ([IdEnfermedad]) REFERENCES [dbo].[Enfermedades] ([IdEnfermedad]) ON DELETE CASCADE,
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

