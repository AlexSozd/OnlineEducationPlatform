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
);
CREATE TABLE dbo.Users
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

GO
CREATE PROC GetTryNum
(
  @id_bl AS INT,
  @id_us AS INT
)
AS
  BEGIN
    SELECT COUNT(*)+1 FROM ExamResults WHERE id_bl=@id_bl AND id_us=@id_us AND dt>=CAST((YEAR(SYSDATETIME()) - 1) + '0901' AS DATETIME);
  END;

GO
CREATE PROC ShowResultsByTest
(
  @tname AS NVARCHAR(255)
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname);
  END;

GO
CREATE PROC ShowUserAnswersInTest
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname);
  END;

GO
CREATE PROC ShowUserResults2
(
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname);
  END;

GO
CREATE PROC ShowAllResults1
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id);
  END;


/*GO
CREATE PROC ShowAvgResultsByTest
(
  @tname AS NVARCHAR(255)
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND
	procnt=(SELECT AVG(procnt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) GROUP BY Users.id);
  END;

GO
CREATE PROC ShowAvgUserAnswerInTest
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND
	procnt=(SELECT AVG(procnt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname));
  END;

GO
CREATE PROC ShowAvgUserResults
(
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND
	procnt=(SELECT AVG(procnt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) GROUP BY TestBlocks.id);
  END;

GO
CREATE PROC ShowAllAvgResults
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE
	procnt=(SELECT AVG(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) GROUP BY Users.id, TestBlocks.id);
  END;*/


GO
CREATE PROC ShowResultsByTestForTimePeriod
(
  @tname AS NVARCHAR(255),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2);
  END;

GO
CREATE PROC ShowUserAnswersInTestForTimePeriod
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2);
  END;

GO
CREATE PROC ShowUserResultsInTimePeriod
(
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2);
  END;

GO
CREATE PROC ShowAllResultsForTimePeriod
(
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2);
  END;


/*GO
CREATE PROC ShowAvgResultsByTestForPeriod
(
  @tname AS NVARCHAR(255),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT AVG(procnt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id);
  END;

GO
CREATE PROC ShowAvgUserAnswerInTestForPeriod
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT AVG(procnt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2));
  END;

GO
CREATE PROC ShowAvgUserResultsForPeriod
(
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT AVG(procnt) FROM (dbo.ExamResults JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2) GROUP BY TestBlocks.id);
  END;

GO
CREATE PROC ShowAllAvgResultsForPeriod
(
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, dt FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) AND
	procnt=(SELECT AVG(procnt) FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id) WHERE (dt>=@dt1) AND (dt<=@dt2) GROUP BY Users.id, TestBlocks.id);
  END;*/
