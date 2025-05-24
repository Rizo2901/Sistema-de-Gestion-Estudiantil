CREATE TABLE [dbo].[Reuniones] (
    [IdReunion]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante] UNIQUEIDENTIFIER NOT NULL,
    [Fecha]        DATE             NOT NULL,
    [Motivo]       TEXT             NOT NULL,
    PRIMARY KEY CLUSTERED ([IdReunion] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

