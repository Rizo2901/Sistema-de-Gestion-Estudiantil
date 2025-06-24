CREATE TABLE [dbo].[MateriaProfesor] (
    [IdMateriaProfesor] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdMateria]         UNIQUEIDENTIFIER NOT NULL,
    [IdProfesor]        UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([IdMateriaProfesor] ASC),
    FOREIGN KEY ([IdMateria]) REFERENCES [dbo].[Materias] ([IdMateria]) ON DELETE CASCADE,
    FOREIGN KEY ([IdProfesor]) REFERENCES [dbo].[Profesores] ([IdProfesor]) ON DELETE CASCADE
);

