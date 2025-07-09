CREATE TABLE [dbo].[AtencionesMedicas] (
    [IdAtencionMedica] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]     UNIQUEIDENTIFIER NOT NULL,
    [Fecha]            DATE             NOT NULL,
    [Diagnostico]      TEXT             NULL,
    [Tratamiento]      TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdAtencionMedica] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

