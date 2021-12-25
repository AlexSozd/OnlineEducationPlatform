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
                      (1, 32, 'Основы общей и сферической астрономии')/*,
					  (2, 46, 'Астрономические и навигационные приборы и их применение'),
					  (3, 68, 'Методы определения местонахождения наблюдателя по небесным светилам')*/;
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
       (1, 1, 'Долгота 98 градусов 52,5 минуты в часовой мере: ', NULL, '6 ч 48 мин 35 с', '6 ч 35 мин 30 с', '6 ч 33 мин 35 с', '6 ч 35 мин 27,5 с', '2', 1),
	   (2, 1, 'Дано: широта 55 градусов 45,6 минут N, склонение светила 10 градусов 13,4 минуты S, часовой угол - 62 градуса 24,5 минуты W. Найдите высоту светила.', NULL, '6 градусов 24,2 минуты', '6 градусов 18,1 минуты', '23 градуса 1 минута', '16 градусов 7,3 минуты', '3', 2),
	   (3, 1, 'По данным из задания 2 найдите азимут светила и запишите ответ, округлив до десятой части минуты.', NULL, NULL, NULL, NULL, NULL, '241.3', 2),
	   (4, 1, 'Используя меридиональную высоту 37 градусов 28,4 минуты S и склонение светила 12 градусов 13,8 минуты S, определите широту места.', NULL, '25 градусов 14,6 минуты S', '40 градусов 17,8 минуты N', '64 градуса 45,4 минуты S', '25 градусов 14,6 минуты N', '2', 1),
	   (5, 1, 'Для места с широтой 14 градусов S определить даты прохождения Солнца через зенит. Для определения примите 0,4 градуса в день.', NULL, '30/X и 12/II', '16/VIII и 27/IV', '30/IX и 14/III', NULL, '1', 2),
	   (6, 1, 'На Гринвиче время кульминации Луны 5 мая 6 ч 54 мин. Найти время кульминации для места с долготой 140 W.', NULL, '7 ч 10 мин', '7 ч 30 мин', '7 ч 27 мин', '7 ч 13 мин', '4', 1),
	   (7, 1, 'На меридиане 131 градусов 27,5 минуты Ost часовой угол светила 6 ч 18 мин 20 с. Определить часовой угол на меридиане 61 градус 43 минуты Ost.', NULL, '2 ч 00 мин 37 с', '1 ч 39 мин 22 с', '1 ч 27 мин 53 с', '1 ч 43 мин 48 с', '2', 2),
	   (8, 1, 'Местное время 5 ч 38 мин в месте с долготой 82 градуса 10 минут W; определить поясное время: ', NULL, '5 ч 38 мин', '5 ч 6 мин 40 с', '6 ч 7 мин', NULL, '3', 2),
	   (9, 1, 'Время хронометра 10 ч 51 мин 35 с, поправка хронометра -1 мин 12 с, время судовых часов 9 ч 50 мин 3/X (11 Ost). Определить точное гринвичское время и дату.', NULL, '22 ч 50 мин 23 с 1/X', '10 ч 50 мин 23 с 3/X', '22 ч 50 мин 23 с 2/X', NULL, '3', 1);
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