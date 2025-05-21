CREATE TABLE [dbo].[Incapacidades] (
    [id]               INT             IDENTITY (1, 1) NOT NULL,
    [id_estudiante]    INT             NOT NULL,
    [fecha_inicio]     DATE            NOT NULL,
    [fecha_fin]        DATE            NOT NULL,
    [motivo]           TEXT            NULL,
    [documento_medico] VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE
);

