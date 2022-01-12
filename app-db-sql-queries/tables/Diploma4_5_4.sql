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
   us_balls REAL NOT NULL,
   us_ans NVARCHAR(255) NOT NULL,
   tr_ans NVARCHAR(255) NOT NULL
);
IF OBJECT_ID('dbo.PromptCount', 'U') IS NOT NULL
   DROP TABLE dbo.PromptCount;
CREATE TABLE dbo.PromptCount
(
   id INT NOT NULL PRIMARY KEY,
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   max_num_pr INT NOT NULL
);*/
IF OBJECT_ID('dbo.UserAccess', 'U') IS NOT NULL
   DROP TABLE dbo.UserAccess;
CREATE TABLE dbo.UserAccess
(
   id INT NOT NULL PRIMARY KEY,
   id_us INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(id),
   test_av NVARCHAR(5) NOT NULL,
   cont_av NVARCHAR(5) NOT NULL,
   stat_av NVARCHAR(5) NOT NULL
);

GO
CREATE PROC AddUserAccess
(
  @id_num AS INT,
  @id_us AS INT,
  @test_av AS NVARCHAR(5),
  @cont_av AS NVARCHAR(5),
  @stat_av AS NVARCHAR(5)
)
AS
  BEGIN
    INSERT INTO dbo.UserAccess(id, id_us, test_av, cont_av, stat_av) VALUES (@id_num, @id_us, @test_av, @cont_av, @stat_av);
  END;

GO
CREATE PROC ShowUsersAndAccess
AS
  BEGIN
    SELECT Users.id, Users.name, UserAccess.test_av, UserAccess.cont_av, UserAccess.stat_av FROM (dbo.Users JOIN dbo.UserAccess ON Users.id=UserAccess.id_us);
  END;

GO
CREATE PROC GetUserAccessID
AS
  BEGIN
    SELECT MAX(id)+1 FROM UserAccess;
  END;
