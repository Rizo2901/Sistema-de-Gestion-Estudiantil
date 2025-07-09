CREATE TABLE [dbo].[Medicamentos] (
    [IdMedicamento] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]        VARCHAR (100)    NOT NULL,
    [Descripcion]   TEXT             NULL,
    PRIMARY KEY CLUSTERED ([IdMedicamento] ASC)
);

