CREATE TABLE [dbo].[ProfesoresSecciones] (
    [IdProfesorSeccion] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdProfesor]        UNIQUEIDENTIFIER NOT NULL,
    [IdSeccion]         UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProfesorSeccion] ASC),
    FOREIGN KEY ([IdProfesor]) REFERENCES [dbo].[Profesores] ([IdProfesor]) ON DELETE CASCADE,
    FOREIGN KEY ([IdSeccion]) REFERENCES [dbo].[Secciones] ([IdSeccion]) ON DELETE CASCADE
);

