use Edaibd;

GO
ALTER PROC GetTryNum
(
  @id_bl AS INT,
  @id_us AS INT
)
AS
  BEGIN
    SELECT COUNT(*)+1 FROM ExamResults WHERE id_bl=@id_bl AND id_us=@id_us AND YEAR(dt)>(YEAR(SYSDATETIME()) - 1);
  END;
