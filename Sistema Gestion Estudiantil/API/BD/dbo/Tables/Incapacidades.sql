CREATE TABLE [dbo].[Incapacidades] (
    [IdIncapacidad]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]    UNIQUEIDENTIFIER NOT NULL,
    [FechaInicio]     DATE             NOT NULL,
    [FechaFin]        DATE             NOT NULL,
    [Motivo]          TEXT             NULL,
    [DocumentoMedico] VARBINARY (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([IdIncapacidad] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

