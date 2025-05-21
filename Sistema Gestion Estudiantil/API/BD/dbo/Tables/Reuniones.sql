CREATE TABLE [dbo].[Reuniones] (
    [id]                      INT          IDENTITY (1, 1) NOT NULL,
    [id_estudiante]           INT          NOT NULL,
    [id_profesor]             INT          NOT NULL,
    [fecha]                   DATE         NOT NULL,
    [hora]                    TIME (7)     NOT NULL,
    [motivo]                  TEXT         NOT NULL,
    [estado]                  VARCHAR (20) DEFAULT ('Pendiente') NULL,
    [confirmacion_asistencia] VARCHAR (20) DEFAULT ('Pendiente') NULL,
    [fecha_confirmacion]      DATETIME     NULL,
    [comentarios]             TEXT         NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE,
    FOREIGN KEY ([id_profesor]) REFERENCES [dbo].[Profesores] ([id]) ON DELETE CASCADE
);

