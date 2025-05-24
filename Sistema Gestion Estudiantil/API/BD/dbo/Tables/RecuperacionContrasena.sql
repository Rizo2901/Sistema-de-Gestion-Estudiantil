CREATE TABLE [dbo].[RecuperacionContrasena] (
    [IdRecuperacion] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdUsuario]      UNIQUEIDENTIFIER NOT NULL,
    [Token]          VARCHAR (255)    NOT NULL,
    [FechaCreacion]  DATETIME         DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([IdRecuperacion] ASC),
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
);

