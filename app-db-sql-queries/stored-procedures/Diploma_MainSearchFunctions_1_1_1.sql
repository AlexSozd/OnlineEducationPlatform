use Edaibd;

GO
ALTER PROC ShowPassedProcentTestResults
(
  @stud_proc AS INT,
  @ex_proc AS INT
)
AS
  BEGIN
    DECLARE @kol AS INT
    SET @kol = (SELECT COUNT(*) FROM Users WHERE id=(SELECT id FROM UserAccess WHERE test_av='1'));
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id)
	WHERE ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100/@kol AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count>=@stud_proc) AND procnt>=@ex_proc;
  END;
GO
ALTER PROC ShowPassedProcentTestResultsForTimePeriod
(
  @stud_proc AS INT,
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    DECLARE @kol AS INT
    SET @kol = (SELECT COUNT(*) FROM Users WHERE id=(SELECT id FROM UserAccess WHERE test_av='1'));
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id)
	WHERE ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100/@kol AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count>=@stud_proc) AND procnt>=@ex_proc AND dt>=@dt1 AND dt<=@dt2;
  END;
GO
ALTER PROC ShowProcentedBestResults
(
  @stud_proc AS INT,
  @ex_proc AS INT
)
AS
  BEGIN
    DECLARE @kol AS INT
    SET @kol = (SELECT COUNT(*) FROM Users WHERE id=(SELECT id FROM UserAccess WHERE test_av='1'));
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100/@kol AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count>=@stud_proc) AND procnt>=@ex_proc;
  END;
GO
ALTER PROC ShowProcentedLastResults
(
  @stud_proc AS INT,
  @ex_proc AS INT
)
AS
  BEGIN
    DECLARE @kol AS INT
    SET @kol = (SELECT COUNT(*) FROM Users WHERE id=(SELECT id FROM UserAccess WHERE test_av='1'));
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100/@kol AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count>=@stud_proc) AND procnt>=@ex_proc;
  END;
GO
ALTER PROC ShowProcentedBestResultsForPeriod
(
  @stud_proc AS INT,
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    DECLARE @kol AS INT
    SET @kol = (SELECT COUNT(*) FROM Users WHERE id=(SELECT id FROM UserAccess WHERE test_av='1'));
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100/@kol AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count>=@stud_proc) AND procnt>=@ex_proc AND dt>=@dt1 AND dt<=@dt2;
  END;
GO
ALTER PROC ShowProcentedLastResultsForPeriod
(
  @stud_proc AS INT,
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    DECLARE @kol AS INT
    SET @kol = (SELECT COUNT(*) FROM Users WHERE id=(SELECT id FROM UserAccess WHERE test_av='1'));
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100/@kol AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count>=@stud_proc) AND procnt>=@ex_proc AND (dt>=@dt1) AND (dt<=@dt2);
  END;
