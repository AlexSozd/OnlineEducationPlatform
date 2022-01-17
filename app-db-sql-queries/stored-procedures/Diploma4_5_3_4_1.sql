use Edaibd;

/*CREATE TABLE dbo.TestQuestions
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
);*/

GO
CREATE PROC AddExResult
(
  @id_num AS INT,
  @id_us AS INT,
  @id_bl AS INT,
  @m AS INT,
  @b AS REAL,
  @max_res AS INT,
  @pc AS REAL,
  @dt AS DATETIME,
  @p_num AS INT
)
AS
  BEGIN
    INSERT INTO dbo.ExamResults(id, id_us, id_bl, real_balls, max_res, procnt, mark, dt, trynum) VALUES (@id_num, @id_us, @id_bl, @b, @max_res, @pc, @m, @dt, @p_num);
  END;

GO
CREATE PROC AddUser
(
   @id_us AS INT,
   @name AS NVARCHAR,
   @c_tel AS NVARCHAR,
   @e_m AS NVARCHAR,
   @par AS NVARCHAR
)
AS
  BEGIN
    INSERT INTO dbo.Users(id, name, cell_tel, e_mail, parole) VALUES (@id_us, @name, @c_tel, @e_m, @par);
  END;

GO
CREATE PROC ShowUsers
AS
  BEGIN
    SELECT * FROM dbo.Users;
  END;

GO
CREATE PROC ShowAllResults
AS
  BEGIN
    SELECT * FROM dbo.ExamResults;
  END;

GO
CREATE PROC ShowUserResults
(
  @id_us AS INT
)
AS
  BEGIN
    SELECT id, dt, real_balls, max_res, procnt, mark FROM dbo.ExamResults WHERE id_us=@id_us;
  END;

GO
CREATE PROC ShowUserResults1
(
  @usname AS NVARCHAR
)
AS
  BEGIN
    --SELECT id, mark, real_balls, max_res, dt FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE name=@usname;
    SELECT id, dt, real_balls, max_res, procnt, mark FROM dbo.ExamResults WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname);
  END;

GO
CREATE FUNCTION GetUserID
(

)
RETURNS INT
AS
  BEGIN
    DECLARE @i AS INT
    SET @i = (SELECT MAX(id)+1 FROM Users);
    RETURN @i;
  END;

GO
CREATE FUNCTION GetExamID
(

)
RETURNS INT
AS
  BEGIN
    DECLARE @i AS INT
    SET @i = (SELECT MAX(id)+1 FROM ExamResults);
    RETURN @i;
  END;

GO
CREATE FUNCTION GetAnswerID
(

)
RETURNS INT
AS
  BEGIN
    DECLARE @i AS INT
    SET @i = (SELECT MAX(id)+1 FROM Answers);
    RETURN @i;
  END;

GO
CREATE PROC ShowStat
AS
  BEGIN
    SELECT * FROM dbo.Answers;
  END;
GO
CREATE PROC ShowUserAnswersInTestPrompt
(
  @id_ex AS INT,
  @usname AS NVARCHAR
)
AS
  BEGIN
    --SELECT id, mark, real_balls, max_res, dt FROM (dbo.Answers JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE name=@usname;
    SELECT id, us_ans, tr_ans, us_balls, balls FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.id_bl=TestQuestions.id_bl) WHERE id_ex=@id_ex AND id_us=(SELECT id FROM dbo.Users WHERE name=@usname);
  END;

GO
CREATE PROC ShowAnswersForPrompt
(
  @id_bl AS INT,
  @p_num AS INT
)
AS
  BEGIN
    --SELECT id, mark, real_balls, max_res, dt FROM (dbo.Answers JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE name=@usname;
    SELECT id, id_us, us_ans, tr_ans, us_balls, balls FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.id_bl=TestQuestions.id_bl) WHERE id_ex=(SELECT id FROM dbo.ExamResults WHERE id_bl=@id_bl AND trynum=@p_num);
  END;
