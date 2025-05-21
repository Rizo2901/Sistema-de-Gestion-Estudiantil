CREATE TABLE [dbo].[Profesores] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [nombre]     VARCHAR (255) NOT NULL,
    [apellido_1] VARCHAR (255) NOT NULL,
    [apellido_2] VARCHAR (255) NOT NULL,
    [direccion]  VARCHAR (MAX) NOT NULL,
    [contacto]   VARCHAR (50)  NULL,
    [id_usuario] INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuarios] ([id])
);

