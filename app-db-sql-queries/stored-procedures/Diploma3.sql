use Edaibd;
GO
CREATE PROC GetLesFirPage
(
  @id_lesson AS INT
)
AS
  BEGIN
    SELECT MIN(id) FROM Pages WHERE id_les=@id_lesson;
  END;
GO
CREATE PROC PagesCount1
AS
  BEGIN
    SELECT COUNT(*) FROM Pages;
  END;
GO
