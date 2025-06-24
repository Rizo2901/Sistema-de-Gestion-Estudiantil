CREATE PROCEDURE [dbo].[sp_CrearMateriaProfesor]
    @IdMateria UNIQUEIDENTIFIER,
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO MateriaProfesor (IdMateriaProfesor, IdMateria, IdProfesor)
    VALUES (@NuevoId,@IdMateria, @IdProfesor);

	SELECT @NuevoId AS IdMateriaProfesor;
END