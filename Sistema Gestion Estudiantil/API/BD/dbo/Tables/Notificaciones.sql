CREATE TABLE [dbo].[Notificaciones] (
    [id]            INT      IDENTITY (1, 1) NOT NULL,
    [id_estudiante] INT      NOT NULL,
    [mensaje]       TEXT     NOT NULL,
    [fecha]         DATETIME DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE
);

