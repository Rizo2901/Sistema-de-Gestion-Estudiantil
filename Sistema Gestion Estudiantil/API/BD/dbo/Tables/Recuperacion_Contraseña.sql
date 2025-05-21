CREATE TABLE [dbo].[Recuperacion_Contraseña] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [id_usuario]     INT           NOT NULL,
    [token]          VARCHAR (255) NOT NULL,
    [fecha_creacion] DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuarios] ([id])
);

