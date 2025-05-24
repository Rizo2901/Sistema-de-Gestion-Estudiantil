CREATE TABLE [dbo].[Promedios] (
    [IdPromedio]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante] UNIQUEIDENTIFIER NOT NULL,
    [Promedio]     DECIMAL (5, 2)   NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPromedio] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

