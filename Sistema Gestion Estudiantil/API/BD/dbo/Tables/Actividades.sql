CREATE TABLE [dbo].[Actividades] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [titulo]      VARCHAR (255) NOT NULL,
    [descripcion] TEXT          NULL,
    [fecha]       DATE          NOT NULL,
    [tipo]        VARCHAR (15)  NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

