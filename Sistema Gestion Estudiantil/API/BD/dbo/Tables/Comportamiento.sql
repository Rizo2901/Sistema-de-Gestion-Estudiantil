CREATE TABLE [dbo].[Comportamiento] (
    [IdComportamiento] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]     UNIQUEIDENTIFIER NOT NULL,
    [Fecha]            DATE             NOT NULL,
    [Observacion]      TEXT             NULL,
    [AccionTomada]     TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdComportamiento] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

