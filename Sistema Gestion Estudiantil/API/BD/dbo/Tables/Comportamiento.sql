CREATE TABLE [dbo].[Comportamiento] (
    [id]            INT  IDENTITY (1, 1) NOT NULL,
    [id_estudiante] INT  NOT NULL,
    [fecha]         DATE NOT NULL,
    [observacion]   TEXT NULL,
    [accion_tomada] TEXT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE
);

