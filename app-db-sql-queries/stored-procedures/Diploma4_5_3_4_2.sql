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
CREATE PROC ShowUserAnswersInTestPrompt
(
  @id_ex AS INT,
  @usname AS NVARCHAR
)
AS
  BEGIN
    --SELECT id, mark, real_balls, max_res, dt FROM (dbo.Answers JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE name=@usname;
    SELECT Answers.id, us_ans, tr_ans, us_balls, balls FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.id_bl=TestQuestions.id_bl) WHERE id_ex=@id_ex AND id_us=(SELECT id FROM dbo.Users WHERE name=@usname);
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
    SELECT Answers.id, id_us, us_ans, tr_ans, us_balls, balls FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.id_bl=TestQuestions.id_bl) WHERE id_ex=(SELECT id FROM dbo.ExamResults WHERE id_bl=@id_bl AND trynum=@p_num);
  END;
