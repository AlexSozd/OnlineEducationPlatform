use Edaibd;

GO
CREATE PROC ShowResultsByTest1
(
  @tname AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowUserAnswersInTest1
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @procnt AS INT
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowUserResults3
(
  @usname AS NVARCHAR(100),
  @procnt AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowPassedProcentTestResults
(
  @stud_proc AS INT,
  @ex_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id)
	WHERE ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100)) AND procnt>=@ex_proc;
  END;
GO
CREATE PROC ShowResultsByTestForTimePeriod1
(
  @tname AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowUserAnswersInTestForTimePeriod1
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowUserResultsInTimePeriod1
(
  @usname AS NVARCHAR(100),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowPassedProcentTestResultsForTimePeriod
(
  @stud_proc AS INT,
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id)
	WHERE ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100)) AND procnt>=@ex_proc AND dt>=@dt1 AND dt<=@dt2;
  END;
GO
CREATE PROC ShowBestResultsByTest1
(
  @tname AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) GROUP BY Users.id) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowLastResultsByTest1
(
  @tname AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND
	dt=(SELECT MAX(dt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) GROUP BY Users.id) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowBestUserAnswerInTest1
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @procnt AS INT
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND
	procnt=(SELECT MAX(procnt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname)) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowLastUserAnswerInTest1
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @procnt AS INT
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND
	dt=(SELECT MAX(dt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname)) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowBestUserResults1
(
  @usname AS NVARCHAR(100),
  @procnt AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) GROUP BY TestBlocks.id) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowLastUserResults1
(
  @usname AS NVARCHAR(100),
  @procnt AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND
	dt=(SELECT MAX(dt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) GROUP BY TestBlocks.id) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowProcentedBestResults
(
  @stud_proc AS INT,
  @ex_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100)) AND procnt>=@ex_proc;
  END;
GO
CREATE PROC ShowProcentedLastResults
(
  @stud_proc AS INT,
  @ex_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100)) AND procnt>=@ex_proc;
  END;
GO
CREATE PROC ShowBestResultsByTestForPeriod1
(
  @tname AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowLastResultsByTestForPeriod1
(
  @tname AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	dt=(SELECT MAX(dt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowBestUserAnswerInTestForPeriod1
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT MAX(procnt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2)) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowLastUserAnswerInTestForPeriod1
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	dt=(SELECT MAX(dt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2)) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowBestUserResultsForPeriod1
(
  @usname AS NVARCHAR(100),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY TestBlocks.id) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowLastUserResultsForPeriod1
(
  @usname AS NVARCHAR(100),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	dt=(SELECT MAX(dt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY TestBlocks.id) AND procnt>=@procnt;
  END;
GO
CREATE PROC ShowProcentedBestResultsForPeriod
(
  @stud_proc AS INT,
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100)) AND procnt>=@ex_proc AND dt>=@dt1 AND dt<=@dt2;
  END;
GO
CREATE PROC ShowProcentedLastResultsForPeriod
(
  @stud_proc AS INT,
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100)) AND procnt>=@ex_proc AND (dt>=@dt1) AND (dt<=@dt2);
  END;