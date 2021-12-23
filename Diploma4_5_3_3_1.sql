use Edaibd;

/*IF OBJECT_ID('dbo.TestBlocks', 'U') IS NOT NULL
   DROP TABLE dbo.TestBlocks;
CREATE TABLE dbo.TestBlocks
(
   id INT NOT NULL PRIMARY KEY,
   id_page INT NOT NULL FOREIGN KEY REFERENCES dbo.Pages(id),
   name NVARCHAR(255) NOT NULL
);*/
/*INSERT INTO dbo.TestBlocks(id, id_page, name) VALUES
                      (1, 32, '������ ����� � ����������� ����������')/*,
					  (2, 46, '��������������� � ������������� ������� � �� ����������'),
					  (3, 68, '������ ����������� ��������������� ����������� �� �������� ��������')*/;
/*IF OBJECT_ID('dbo.TestQuestions', 'U') IS NOT NULL
   DROP TABLE dbo.TestQuestions;
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
INSERT INTO dbo.TestQuestions(id, id_bl, ques, im_file, var1, var2, var3, var4, r_ans, balls) VALUES
       (1, 1, '������� 98 �������� 52,5 ������ � ������� ����: ', NULL, '6 � 48 ��� 35 �', '6 � 35 ��� 30 �', '6 � 33 ��� 35 �', '6 � 35 ��� 27,5 �', '2', 1),
	   (2, 1, '����: ������ 55 �������� 45,6 ����� N, ��������� ������� 10 �������� 13,4 ������ S, ������� ���� - 62 ������� 24,5 ������ W. ������� ������ �������.', NULL, '6 �������� 24,2 ������', '6 �������� 18,1 ������', '23 ������� 1 ������', '16 �������� 7,3 ������', '3', 2),
	   (3, 1, '�� ������ �� ������� 2 ������� ������ ������� � �������� �����, �������� �� ������� ����� ������.', NULL, NULL, NULL, NULL, NULL, '241.3', 2),
	   (4, 1, '��������� �������������� ������ 37 �������� 28,4 ������ S � ��������� ������� 12 �������� 13,8 ������ S, ���������� ������ �����.', NULL, '25 �������� 14,6 ������ S', '40 �������� 17,8 ������ N', '64 ������� 45,4 ������ S', '25 �������� 14,6 ������ N', '2', 1),
	   (5, 1, '��� ����� � ������� 14 �������� S ���������� ���� ����������� ������ ����� �����. ��� ����������� ������� 0,4 ������� � ����.', NULL, '30/X � 12/II', '16/VIII � 27/IV', '30/IX � 14/III', NULL, '1', 2),
	   (6, 1, '�� �������� ����� ����������� ���� 5 ��� 6 � 54 ���. ����� ����� ����������� ��� ����� � �������� 140 W.', NULL, '7 � 10 ���', '7 � 30 ���', '7 � 27 ���', '7 � 13 ���', '4', 1),
	   (7, 1, '�� ��������� 131 �������� 27,5 ������ Ost ������� ���� ������� 6 � 18 ��� 20 �. ���������� ������� ���� �� ��������� 61 ������ 43 ������ Ost.', NULL, '2 � 00 ��� 37 �', '1 � 39 ��� 22 �', '1 � 27 ��� 53 �', '1 � 43 ��� 48 �', '2', 2),
	   (8, 1, '������� ����� 5 � 38 ��� � ����� � �������� 82 ������� 10 ����� W; ���������� ������� �����: ', NULL, '5 � 38 ���', '5 � 6 ��� 40 �', '6 � 7 ���', NULL, '3', 2),
	   (9, 1, '����� ���������� 10 � 51 ��� 35 �, �������� ���������� -1 ��� 12 �, ����� ������� ����� 9 � 50 ��� 3/X (11 Ost). ���������� ������ ����������� ����� � ����.', NULL, '22 � 50 ��� 23 � 1/X', '10 � 50 ��� 23 � 3/X', '22 � 50 ��� 23 � 2/X', NULL, '3', 1);
*/
/*IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
   DROP TABLE dbo.Users;
CREATE TABLE dbo.Users
(
   id INT NOT NULL PRIMARY KEY,
   name NVARCHAR(255) NOT NULL,
   cell_tel NVARCHAR(255) NULL,
   e_mail NVARCHAR(255) NULL,
   parole NVARCHAR(255) NOT NULL
);
INSERT INTO dbo.Users(id, name, cell_tel, e_mail, parole) VALUES
       (1, 'Tester1', '+7(917)777-33-55', 'tester@mail.ru', 'RQSP4511');

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
	SELECT id, us_balls FROM dbo.Answers WHERE id_ex=@id_ex AND id_us=(SELECT id FROM dbo.Users WHERE name=@usname);
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
	SELECT id, id_us, us_balls FROM dbo.Answers WHERE id_ex=(SELECT id FROM dbo.ExamResults WHERE id_bl=@id_bl AND trynum=@p_num);
  END;