CREATE TABLE [dbo].[Estudiante_Discapacidad] (
    [IdEstudiante]             UNIQUEIDENTIFIER NOT NULL,
    [IdDiscapacidad]           UNIQUEIDENTIFIER NOT NULL,
    [FechaDiagnostico]         DATE             NULL,
    [GradoAfectacion]          VARCHAR (50)     NULL,
    [Observaciones]            TEXT             NULL,
    [IdEstudianteDiscapacidad] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_Estudiante_Dsicapacidad] PRIMARY KEY CLUSTERED ([IdEstudianteDiscapacidad] ASC),
    FOREIGN KEY ([IdDiscapacidad]) REFERENCES [dbo].[Discapacidades] ([IdDiscapacidad]) ON DELETE CASCADE,
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

