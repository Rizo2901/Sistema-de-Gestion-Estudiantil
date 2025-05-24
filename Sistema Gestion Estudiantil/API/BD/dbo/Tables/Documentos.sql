CREATE TABLE [dbo].[Documentos] (
    [IdDocumento]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdEstudiante]  UNIQUEIDENTIFIER NOT NULL,
    [TipoDocumento] VARCHAR (10)     NOT NULL,
    [Descripcion]   TEXT             NULL,
    [Archivo]       VARBINARY (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([IdDocumento] ASC),
    FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([IdEstudiante]) ON DELETE CASCADE
);

