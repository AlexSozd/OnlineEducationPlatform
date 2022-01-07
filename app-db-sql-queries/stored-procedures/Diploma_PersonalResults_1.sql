use Edaibd;

/*
CREATE TABLE dbo.TestBlocks
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
IF OBJECT_ID('dbo.ExamResults', 'U') IS NOT NULL
   DROP TABLE dbo.ExamResults;
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
IF OBJECT_ID('dbo.Answers', 'U') IS NOT NULL
   DROP TABLE dbo.Answers;
CREATE TABLE dbo.Answers
(
   id INT NOT NULL PRIMARY KEY,
   id_us INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(id),
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   id_ex INT NOT NULL FOREIGN KEY REFERENCES dbo.ExamResults(id),
   us_balls REAL NOT NULL
);
IF OBJECT_ID('dbo.PromptCount', 'U') IS NOT NULL
   DROP TABLE dbo.PromptCount;
CREATE TABLE dbo.PromptCount
(
   id INT NOT NULL PRIMARY KEY,
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   max_num_pr INT NOT NULL
);*/

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
CREATE PROC PersonalTestAnswers
(
  @id_ex AS INT
)
AS
  BEGIN
    SELECT * FROM dbo.Answers WHERE id_ex=@id_ex;
  END;