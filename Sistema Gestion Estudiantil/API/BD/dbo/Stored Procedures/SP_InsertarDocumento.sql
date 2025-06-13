CREATE PROCEDURE [dbo].[SP_InsertarDocumento]
    @IdEstudiante UNIQUEIDENTIFIER,
    @TipoDocumento VARCHAR(50),
    @Descripcion TEXT,
    @Archivo VARBINARY(MAX),
    @TipoArchivo VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Documentos (IdDocumento, IdEstudiante, TipoDocumento, Descripcion, Archivo, TipoArchivo)
    VALUES (@NuevoId, @IdEstudiante, @TipoDocumento, @Descripcion, @Archivo, @TipoArchivo);

    SELECT @NuevoId AS IdDocumento;
END;