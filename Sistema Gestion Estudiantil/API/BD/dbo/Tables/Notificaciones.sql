CREATE TABLE [dbo].[Notificaciones] (
    [IdNotificacion] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]   UNIQUEIDENTIFIER NOT NULL,
    [Mensaje]        TEXT             NOT NULL,
    [Fecha]          DATETIME         DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([IdNotificacion] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

