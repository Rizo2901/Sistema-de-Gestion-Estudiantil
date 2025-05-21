CREATE TABLE [dbo].[Asistencia_Profesores] (
    [id]           INT      IDENTITY (1, 1) NOT NULL,
    [id_profesor]  INT      NOT NULL,
    [fecha]        DATE     DEFAULT (getdate()) NOT NULL,
    [hora_entrada] TIME (7) NULL,
    [hora_salida]  TIME (7) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_profesor]) REFERENCES [dbo].[Profesores] ([id]) ON DELETE CASCADE
);

