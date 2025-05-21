CREATE TABLE [dbo].[Horarios] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [id_seccion]  INT          NOT NULL,
    [id_materia]  INT          NOT NULL,
    [dia]         VARCHAR (10) NOT NULL,
    [hora_inicio] TIME (7)     NOT NULL,
    [hora_fin]    TIME (7)     NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_materia]) REFERENCES [dbo].[Materias] ([id]) ON DELETE CASCADE,
    FOREIGN KEY ([id_seccion]) REFERENCES [dbo].[Secciones] ([id]) ON DELETE CASCADE
);

