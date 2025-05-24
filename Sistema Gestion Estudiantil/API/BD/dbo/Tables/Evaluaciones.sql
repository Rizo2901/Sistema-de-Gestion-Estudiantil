CREATE TABLE [dbo].[Evaluaciones] (
    [IdEvaluacion] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante] UNIQUEIDENTIFIER NOT NULL,
    [IdMateria]    UNIQUEIDENTIFIER NOT NULL,
    [Calificacion] DECIMAL (5, 2)   NOT NULL,
    [Fecha]        DATE             NOT NULL,
    PRIMARY KEY CLUSTERED ([IdEvaluacion] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE,
    FOREIGN KEY ([IdMateria]) REFERENCES [dbo].[Materias] ([IdMateria]) ON DELETE CASCADE
);

