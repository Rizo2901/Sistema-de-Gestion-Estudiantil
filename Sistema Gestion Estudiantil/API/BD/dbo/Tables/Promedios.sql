CREATE TABLE [dbo].[Promedios] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [id_estudiante] INT            NOT NULL,
    [promedio]      DECIMAL (5, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE
);

