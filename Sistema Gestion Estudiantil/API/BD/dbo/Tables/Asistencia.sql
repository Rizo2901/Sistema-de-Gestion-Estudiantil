CREATE TABLE [dbo].[Asistencia] (
    [id]            INT          IDENTITY (1, 1) NOT NULL,
    [id_estudiante] INT          NOT NULL,
    [fecha]         DATE         NOT NULL,
    [estado]        VARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE
);

