CREATE TABLE [dbo].[Evaluaciones] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [id_estudiante] INT            NOT NULL,
    [id_materia]    INT            NOT NULL,
    [calificacion]  DECIMAL (5, 2) NOT NULL,
    [fecha]         DATE           NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE,
    FOREIGN KEY ([id_materia]) REFERENCES [dbo].[Materias] ([id]) ON DELETE CASCADE
);

