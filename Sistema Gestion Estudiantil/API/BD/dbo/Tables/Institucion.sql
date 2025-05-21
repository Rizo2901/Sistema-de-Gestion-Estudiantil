CREATE TABLE [dbo].[Institucion] (
    [id]        INT           IDENTITY (1, 1) NOT NULL,
    [nombre]    VARCHAR (255) NOT NULL,
    [direccion] TEXT          NOT NULL,
    [telefono]  VARCHAR (20)  NOT NULL,
    [correo]    VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

