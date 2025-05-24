CREATE TABLE [dbo].[ParticipantesReunionAdmin] (
    [IdParticipacion] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdReunionAdmin]  UNIQUEIDENTIFIER NOT NULL,
    [IdUsuario]       UNIQUEIDENTIFIER NOT NULL,
    [Asistio]         BIT              DEFAULT ((0)) NULL,
    [Comentarios]     TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdParticipacion] ASC),
    FOREIGN KEY ([IdReunionAdmin]) REFERENCES [dbo].[ReunionesAdministrativas] ([IdReunionAdmin]) ON DELETE CASCADE,
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]) ON DELETE CASCADE
);

