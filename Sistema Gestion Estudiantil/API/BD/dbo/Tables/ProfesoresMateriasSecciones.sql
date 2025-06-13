CREATE TABLE [dbo].[ProfesoresMateriasSecciones] (
    [IdProfesor] UNIQUEIDENTIFIER NOT NULL,
    [IdMateria]  UNIQUEIDENTIFIER NOT NULL,
    [IdSeccion]  UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProfesor] ASC, [IdMateria] ASC, [IdSeccion] ASC),
    FOREIGN KEY ([IdMateria]) REFERENCES [dbo].[Materias] ([IdMateria]) ON DELETE CASCADE,
    FOREIGN KEY ([IdProfesor]) REFERENCES [dbo].[Profesores] ([IdProfesor]) ON DELETE CASCADE,
    FOREIGN KEY ([IdSeccion]) REFERENCES [dbo].[Secciones] ([IdSeccion]) ON DELETE CASCADE
);

