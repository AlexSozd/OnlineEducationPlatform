use Edaibd;
GO
CREATE PROC ShowBestResultsByTest
(
  @tname AS NVARCHAR(255)
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) GROUP BY Users.id);
  END;
GO
CREATE PROC ShowLastResultsByTest
(
  @tname AS NVARCHAR(255)
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND
	dt=(SELECT MAX(dt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) GROUP BY Users.id);
  END;
GO
CREATE PROC ShowBestUserAnswerInTest
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND
	procnt=(SELECT MAX(procnt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname));
  END;
GO
CREATE PROC ShowLastUserAnswerInTest
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND
	dt=(SELECT MAX(dt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname));
  END;
GO
CREATE PROC ShowBestUserResults
(
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) GROUP BY TestBlocks.id);
  END;
GO
CREATE PROC ShowLastUserResults
(
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND
	dt=(SELECT MAX(dt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) GROUP BY TestBlocks.id);
  END;
GO
CREATE PROC ShowAllBestResults
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id);
  END;
GO
CREATE PROC ShowAllLastResults
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id);
  END;
GO
CREATE PROC ShowBestResultsByTestForPeriod
(
  @tname AS NVARCHAR(255),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id);
  END;
GO
CREATE PROC ShowLastResultsByTestForPeriod
(
  @tname AS NVARCHAR(255),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	dt=(SELECT MAX(dt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id);
  END;
GO
CREATE PROC ShowBestUserAnswerInTestForPeriod
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT MAX(procnt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2));
  END;
GO
CREATE PROC ShowLastUserAnswerInTestForPeriod
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	dt=(SELECT MAX(dt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2));
  END;
GO
CREATE PROC ShowBestUserResultsForPeriod
(
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY TestBlocks.id);
  END;
GO
CREATE PROC ShowLastUserResultsForPeriod
(
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	dt=(SELECT MAX(dt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY TestBlocks.id);
  END;
GO
CREATE PROC ShowAllBestResultsForPeriod
(
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id);
  END;
GO
CREATE PROC ShowAllLastResultsForPeriod
(
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) AND
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id);
  END;
