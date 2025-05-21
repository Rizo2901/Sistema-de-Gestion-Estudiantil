CREATE TABLE [dbo].[Profesores_Secciones] (
    [id]          INT IDENTITY (1, 1) NOT NULL,
    [id_profesor] INT NOT NULL,
    [id_seccion]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id_profesor]) REFERENCES [dbo].[Profesores] ([id]) ON DELETE CASCADE,
    FOREIGN KEY ([id_seccion]) REFERENCES [dbo].[Secciones] ([id]) ON DELETE CASCADE
);

