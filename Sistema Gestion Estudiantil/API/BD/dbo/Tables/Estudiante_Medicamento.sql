CREATE TABLE [dbo].[Estudiante_Medicamento] (
    [IdEstudiante]            UNIQUEIDENTIFIER NOT NULL,
    [IdMedicamento]           UNIQUEIDENTIFIER NOT NULL,
    [Dosis]                   VARCHAR (50)     NULL,
    [Frecuencia]              VARCHAR (50)     NULL,
    [FechaInicio]             DATE             NULL,
    [FechaFin]                DATE             NULL,
    [Observaciones]           TEXT             NULL,
    [IdEstudianteMedicamento] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_Estudiante_Medicamento] PRIMARY KEY CLUSTERED ([IdEstudianteMedicamento] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE,
    FOREIGN KEY ([IdMedicamento]) REFERENCES [dbo].[Medicamentos] ([IdMedicamento]) ON DELETE CASCADE
);

