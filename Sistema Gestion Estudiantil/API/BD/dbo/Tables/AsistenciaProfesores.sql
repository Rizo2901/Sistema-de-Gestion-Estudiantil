CREATE TABLE [dbo].[AsistenciaProfesores] (
    [IdAsistenciaProfesor] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdProfesor]           UNIQUEIDENTIFIER NOT NULL,
    [Fecha]                DATE             DEFAULT (getdate()) NOT NULL,
    [HoraEntrada]          TIME (7)         NULL,
    [HoraSalida]           TIME (7)         NULL,
    PRIMARY KEY CLUSTERED ([IdAsistenciaProfesor] ASC),
    FOREIGN KEY ([IdProfesor]) REFERENCES [dbo].[Profesores] ([IdProfesor]) ON DELETE CASCADE
);

