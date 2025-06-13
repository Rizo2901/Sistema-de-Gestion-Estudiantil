CREATE PROCEDURE SP_BuscarEstudiantes
  @nombre NVARCHAR(100) = NULL,
  @cedula NVARCHAR(20) = NULL,
  @grado NVARCHAR(20) = NULL,
  @seccionNombre NVARCHAR(100) = NULL
AS
BEGIN
  SELECT e.*
  FROM Estudiantes e
  INNER JOIN Secciones s ON e.IdSeccion = s.IdSeccion
  WHERE (@nombre IS NULL OR e.Nombre LIKE '%' + @nombre + '%')
    AND (@cedula IS NULL OR e.Cedula LIKE '%' + @cedula + '%')
    AND (@grado IS NULL OR e.GradoActual LIKE '%' + @grado + '%')
    AND (@seccionNombre IS NULL OR s.Nombre LIKE '%' + @seccionNombre + '%')
END