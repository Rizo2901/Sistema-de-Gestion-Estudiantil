CREATE TABLE [dbo].[Secciones] (
    [id]     INT          IDENTITY (1, 1) NOT NULL,
    [nombre] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([nombre] ASC)
);

