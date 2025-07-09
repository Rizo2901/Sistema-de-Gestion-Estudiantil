CREATE TABLE [dbo].[Estudiante_Alergia] (
    [IdEstudiante]        UNIQUEIDENTIFIER NOT NULL,
    [IdAlergia]           UNIQUEIDENTIFIER NOT NULL,
    [FechaDeteccion]      DATE             NULL,
    [Gravedad]            VARCHAR (50)     NULL,
    [Observaciones]       TEXT             NULL,
    [IdEstudianteAlergia] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_Estudiante_Alergia] PRIMARY KEY CLUSTERED ([IdEstudianteAlergia] ASC),
    FOREIGN KEY ([IdAlergia]) REFERENCES [dbo].[Alergias] ([IdAlergia]) ON DELETE CASCADE,
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

