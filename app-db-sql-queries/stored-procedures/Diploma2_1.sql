use Edaibd;
go
CREATE PROC SetLevels
AS
  BEGIN
  SELECT * FROM Levels;
  END;
  GO
CREATE PROC SetSections
AS
  BEGIN
  SELECT * FROM Sections;
  END;
  GO
CREATE PROC SetLessons
AS
  BEGIN
  SELECT * FROM Lessons;
  END;
  GO

CREATE PROC ShowSections
(
  @id_sec AS INT,
  @lev AS INT
)
AS
  BEGIN
  SELECT id, name FROM Sections WHERE id_up=@id_sec AND id_lev=@lev;
  END;
  GO
CREATE PROC ShowFirstLevel
AS
  BEGIN
  SELECT id, name FROM Sections WHERE id_lev=1;
  END;
  GO
CREATE PROC ShowLessons
(
  @id_sec AS INT
)
AS
  BEGIN
  SELECT id, name FROM Lessons WHERE id_sec=@id_sec;
  END;
  GO
CREATE PROC GetLessonPage
(
  @id_lesson AS INT
)
AS
  BEGIN
  SELECT * FROM Pages WHERE id_les=@id_lesson;
  END;
  GO

CREATE PROC GetPageLesson
(
  @id_page AS INT
)
AS
  BEGIN
  --SELECT * FROM Pages WHERE id_les=@id_lesson;
  SELECT id, name FROM Lessons WHERE id=(SELECT id_les FROM Pages WHERE id=@id_page);
  END;
  GO

CREATE FUNCTION dbo.GetNumFirPage
(
  @id_lesson AS INT
)
RETURNS INT
AS
  BEGIN
  DECLARE @i AS INT
  SET @i = (SELECT MIN(id) FROM Pages WHERE id_les=@id_lesson);
  RETURN @i
  END;
  GO
/*CREATE FUNCTION dbo.GetNumFolPage
(
  @id_page AS INT
)
RETURNS INT
AS
  BEGIN
  DECLARE @i AS INT
  SET @i = (SELECT id FROM Pages WHERE id=@id_page);
  RETURN @i
  END;
  GO*/
CREATE PROC GetLesson
(
  @id_lesson AS INT
)
AS
  BEGIN
  SELECT * FROM Lessons WHERE id=@id_lesson;
  END;
  GO
CREATE PROC GetPage
(
  @id_page AS INT
)
AS
  BEGIN
  SELECT * FROM Pages WHERE id=@id_page;
  END;
  GO
CREATE PROC GetTerm
(
  @id_term AS INT
)
AS
  BEGIN
  SELECT * FROM Terms WHERE id=@id_term;
  END;
  GO
CREATE PROC SetPageRefs
(
  @id_page AS INT
)
AS
  BEGIN
  --SELECT id, name FROM (SELECT * FROM PageRefs JOIN Terms on Terms.id=id_term) AS T WHERE id_page=@id_page;
  SELECT id, name FROM Terms WHERE id IN (SELECT id_term FROM PageRefs WHERE id_page=@id_page); 
  END;
  GO

/*CREATE PROC SetPageRefs
(
  @id_page AS INT
)
AS
  BEGIN
  SELECT * FROM PageRefs WHERE id_page=@id_page;
  END;
  GO*/

CREATE PROC SetInRefs
(
  @id_cur AS INT
)
AS
  BEGIN
  SELECT id, name FROM Terms WHERE id IN (SELECT id_term FROM InRefs WHERE id_cur=@id_cur);
  END;
  GO

/*CREATE PROC SetInRefs
(
  @id_cur AS INT
)
AS
  BEGIN
  SELECT * FROM InRefs WHERE id_cur=@id_cur;
  END;
  GO*/
CREATE FUNCTION PagesCount
(

)
RETURNS INT
AS
  BEGIN
  DECLARE @i AS INT
  SET @i = (SELECT COUNT(*) FROM Pages);
  RETURN @i
  END;
  GO