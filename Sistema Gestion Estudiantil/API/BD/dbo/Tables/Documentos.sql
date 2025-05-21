CREATE TABLE [dbo].[Documentos] (
    [id]             INT             IDENTITY (1, 1) NOT NULL,
    [id_estudiante]  INT             NOT NULL,
    [tipo_documento] VARCHAR (10)    NOT NULL,
    [descripcion]    TEXT            NULL,
    [archivo]        VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE
);

