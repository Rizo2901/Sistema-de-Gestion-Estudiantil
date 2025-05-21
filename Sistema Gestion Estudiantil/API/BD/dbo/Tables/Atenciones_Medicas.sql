CREATE TABLE [dbo].[Atenciones_Medicas] (
    [id]            INT  IDENTITY (1, 1) NOT NULL,
    [id_estudiante] INT  NOT NULL,
    [fecha]         DATE NOT NULL,
    [diagnostico]   TEXT NOT NULL,
    [tratamiento]   TEXT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE
);

