CREATE TABLE [dbo].[AdaptacionesCurriculares] (
    [IdAdaptacion]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]  UNIQUEIDENTIFIER NULL,
    [Tipo]          VARCHAR (100)    NULL,
    [AreaAfectada]  VARCHAR (100)    NULL,
    [FechaInicio]   DATE             NULL,
    [FechaFin]      DATE             NULL,
    [Observaciones] TEXT             NULL,
    [Estado]        VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([IdAdaptacion] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

