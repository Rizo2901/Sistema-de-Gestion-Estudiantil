CREATE TABLE [dbo].[Usuarios] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [nombre_usuario]   VARCHAR (50)  NOT NULL,
    [contrasena]       VARCHAR (255) NOT NULL,
    [id_rol]           INT           NOT NULL,
    [estado]           BIT           DEFAULT ((0)) NULL,
    [fecha_activacion] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_rol]) REFERENCES [dbo].[Roles] ([id]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([nombre_usuario] ASC)
);

