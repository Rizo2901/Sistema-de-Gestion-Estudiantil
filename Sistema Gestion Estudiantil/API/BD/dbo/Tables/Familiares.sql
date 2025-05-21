CREATE TABLE [dbo].[Familiares] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [id_estudiante]  INT           NOT NULL,
    [nombre]         VARCHAR (255) NOT NULL,
    [apellido_1]     VARCHAR (255) NOT NULL,
    [apellido_2]     VARCHAR (255) NOT NULL,
    [direccion]      VARCHAR (MAX) NOT NULL,
    [parentesco]     VARCHAR (10)  NOT NULL,
    [contacto]       VARCHAR (50)  NULL,
    [ocupacion]      VARCHAR (100) NULL,
    [estado_civil]   VARCHAR (10)  NOT NULL,
    [cantidad_hijos] INT           DEFAULT ((0)) NULL,
    [id_usuario]     INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Estudiantes] ([id]) ON DELETE CASCADE,
    FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuarios] ([id])
);

