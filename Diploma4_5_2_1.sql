use Edaibd;
GO
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
   r_ans NVARCHAR(255) NOT NULL
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
   mark INT NOT NULL,
   real_balls REAL NOT NULL,
   max_res REAL NOT NULL,
   dt DATETIME NOT NULL
);*/
CREATE PROC FindTestBlock
(
  @id_page AS INT
)
AS
  BEGIN
  SELECT id, name FROM TestBlocks WHERE id_page=@id_page;
  END;

GO
CREATE PROC GetTestBlock
(
  @id_bl AS INT
)
AS
  BEGIN
  SELECT id, ques, im_file, var1, var2, var3, var4, r_ans, balls FROM TestQuestions WHERE id_bl=@id_bl;
  END;

/*GO
CREATE PROC AddExResult
(
  @id_num AS INT,
  @id_us AS INT,
  @m AS INT,
  @b AS REAL,
  @max_res AS REAL,
  @dt AS DATETIME
)
AS
  BEGIN
    INSERT INTO dbo.ExamResults(id, id_us, mark, real_balls, max_res, dt) VALUES (@id_num, @id_us,@m, @b, @max_res, @dt);
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
    SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_us=@id_us;
  END;

GO
CREATE PROC ShowUserResults1
(
  @usname AS NVARCHAR
)
AS
  BEGIN
    --SELECT id, mark, real_balls, max_res, dt FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE name=@usname;
	SELECT id, mark, real_balls, max_res, dt FROM dbo.ExamResults WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname);
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
  END;*/