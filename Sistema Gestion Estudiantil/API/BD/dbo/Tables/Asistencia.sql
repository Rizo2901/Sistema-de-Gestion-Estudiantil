CREATE TABLE [dbo].[Asistencia] (
    [IdAsistencia] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante] UNIQUEIDENTIFIER NOT NULL,
    [Fecha]        DATE             NOT NULL,
    [Estado]       VARCHAR (15)     NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAsistencia] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

