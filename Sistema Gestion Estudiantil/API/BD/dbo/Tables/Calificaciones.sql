CREATE TABLE [dbo].[Calificaciones] (
    [IdCalificacion] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]   UNIQUEIDENTIFIER NOT NULL,
    [IdMateria]      UNIQUEIDENTIFIER NOT NULL,
    [Calificacion]   DECIMAL (5, 2)   NOT NULL,
    [Fecha]          DATE             NOT NULL,
    [Comentario]     VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_Calificaciones] PRIMARY KEY CLUSTERED ([IdCalificacion] ASC),
    CONSTRAINT [CK_Calificaciones_Calificacion] CHECK ([Calificacion]>=(0.00) AND [Calificacion]<=(100.00)),
    CONSTRAINT [FK_Calificaciones_Estudiantes] FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE,
    CONSTRAINT [FK_Calificaciones_Materias] FOREIGN KEY ([IdMateria]) REFERENCES [dbo].[Materias] ([IdMateria]) ON DELETE CASCADE
);

