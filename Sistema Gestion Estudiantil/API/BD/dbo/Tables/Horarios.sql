CREATE TABLE [dbo].[Horarios] (
    [IdHorario]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdSeccion]  UNIQUEIDENTIFIER NOT NULL,
    [IdMateria]  UNIQUEIDENTIFIER NOT NULL,
    [Dia]        VARCHAR (10)     NOT NULL,
    [HoraInicio] TIME (7)         NOT NULL,
    [HoraFin]    TIME (7)         NOT NULL,
    PRIMARY KEY CLUSTERED ([IdHorario] ASC),
    FOREIGN KEY ([IdMateria]) REFERENCES [dbo].[Materias] ([IdMateria]) ON DELETE CASCADE,
    FOREIGN KEY ([IdSeccion]) REFERENCES [dbo].[Secciones] ([IdSeccion]) ON DELETE CASCADE
);

