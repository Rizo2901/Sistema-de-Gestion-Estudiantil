CREATE TABLE [dbo].[Documentos] (
    [IdDocumento]   UNIQUEIDENTIFIER NOT NULL,
    [IdEstudiante]  UNIQUEIDENTIFIER NOT NULL,
    [TipoDocumento] VARCHAR (50)     NOT NULL,
    [Descripcion]   TEXT             NULL,
    [Archivo]       VARBINARY (MAX)  NULL,
    [TipoArchivo]   VARCHAR (100)    NULL,
    PRIMARY KEY CLUSTERED ([IdDocumento] ASC)
);

