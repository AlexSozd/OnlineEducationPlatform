use Edaibd;

/*CREATE TABLE dbo.TestBlocks
(
   id INT NOT NULL PRIMARY KEY,
   id_page INT NOT NULL FOREIGN KEY REFERENCES dbo.Pages(id),
   name NVARCHAR(255) NOT NULL
);
CREATE TABLE dbo.TestQuestions
(
   id INT NOT NULL PRIMARY KEY,
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   ques NVARCHAR(255) NOT NULL,
   im_file NVARCHAR(255) NULL,
   var1 NVARCHAR(255) NULL,
   var2 NVARCHAR(255) NULL,
   var3 NVARCHAR(255) NULL,
   var4 NVARCHAR(255) NULL,
   r_ans NVARCHAR(255) NOT NULL,
   balls INT NOT NULL
);*/
/*CREATE TABLE dbo.Users
(
   id INT NOT NULL PRIMARY KEY,
   name NVARCHAR(255) NOT NULL,
   cell_tel NVARCHAR(255) NULL,
   e_mail NVARCHAR(255) NULL,
   parole NVARCHAR(255) NOT NULL
);
CREATE TABLE dbo.ExamResults
(
   id INT NOT NULL PRIMARY KEY,
   id_us INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(id),
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   real_balls REAL NOT NULL,
   max_res INT NOT NULL,
   procnt INT NOT NULL,
   mark INT NOT NULL,
   dt DATETIME NOT NULL,
   trynum INT NOT NULL
);
CREATE TABLE dbo.Answers
(
   id INT NOT NULL PRIMARY KEY,
   id_us INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(id),
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   id_ex INT NOT NULL FOREIGN KEY REFERENCES dbo.ExamResults(id),
   us_balls REAL NOT NULL
);
CREATE TABLE dbo.PromptCount
(
   id INT NOT NULL PRIMARY KEY,
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   max_num_pr INT NOT NULL
);*/

/*GO
CREATE PROC GetTryNum
(
  @id_bl AS INT,
  @id_us AS INT
)
AS
  BEGIN
    SELECT COUNT(*)+1 FROM ExamResults WHERE id_bl=@id_bl AND id_us=@id_us AND dt>=CAST((YEAR(SYSDATETIME()) - 1) + '0901' AS DATETIME);
  END;
*/
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
  @stud_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id)
	WHERE ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100));
  END;
GO
CREATE PROC ShowPassedProcentTestResults1
(
  @ex_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE procnt>=@ex_proc;
  END;
GO
CREATE PROC ShowPassedProcentTestResults2
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
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id)
	WHERE ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100) AND (dt>=@dt1) AND (dt<=@dt2));
  END;
GO
CREATE PROC ShowPassedProcentTestResultsForTimePeriod1
(
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE procnt>=@ex_proc AND dt>=@dt1 AND dt<=@dt2;
  END;
GO
CREATE PROC ShowPassedProcentTestResultsForTimePeriod2
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
  @stud_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100));
  END;
GO
CREATE PROC ShowProcentedBestResults1
(
  @ex_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id) AND procnt>=@ex_proc;
  END;
GO
CREATE PROC ShowProcentedBestResults2
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
  @stud_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100));
  END;
GO
CREATE PROC ShowProcentedLastResults1
(
  @ex_proc AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id) AND procnt>=@ex_proc;
  END;
GO
CREATE PROC ShowProcentedLastResults2
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
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100) AND (dt>=@dt1) AND (dt<=@dt2));
  END;
GO
CREATE PROC ShowProcentedBestResultsForPeriod1
(
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT MAX(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id) AND procnt>=@ex_proc AND dt>=@dt1 AND dt<=@dt2;
  END;
GO
CREATE PROC ShowProcentedBestResultsForPeriod2
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
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id) AND
	ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100 AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count >= (@stud_proc * 100)) AND (dt>=@dt1) AND (dt<=@dt2);
  END;
GO
CREATE PROC ShowProcentedLastResultsForPeriod1
(
  @ex_proc AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	dt=(SELECT MAX(dt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id) AND procnt>=@ex_proc AND (dt>=@dt1) AND (dt<=@dt2);
  END;
GO
CREATE PROC ShowProcentedLastResultsForPeriod2
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