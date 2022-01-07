use Edaibd;
GO
CREATE PROC ShowPeopleAnswers1
(
  @ques AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt);
  END;
GO
CREATE PROC ShowUserAnswer1
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt);
  END;
GO
CREATE PROC ShowPeopleAnswersForTimePeriod1
(
  @ques AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques AND dt>=@dt1 AND dt<=@dt2 AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt AND dt>=@dt1 AND dt<=@dt2);
  END;
GO
CREATE PROC ShowUserAnswerForTimePeriod1
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND dt>=@dt1 AND dt<=@dt2 AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt AND dt>=@dt1 AND dt<=@dt2);
  END;
GO
CREATE PROC ShowPeopleBestAnswerBalls1
(
  @ques AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques AND
	us_balls=(SELECT MAX(us_balls) FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques) AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt);
  END;
GO
CREATE PROC ShowUserBestAnswerBall1
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND
	us_balls=(SELECT MAX(us_balls) FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques) AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt);
  END;
GO
CREATE PROC ShowPeopleBestAnswerBallsForTimePeriod1
(
  @ques AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques AND dt>=@dt1 AND dt<=@dt2 AND
	us_balls=(SELECT MAX(us_balls) FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques AND dt>=@dt1 AND dt<=@dt2) AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt AND dt>=@dt1 AND dt<=@dt2);
  END;
GO
CREATE PROC ShowUserBestAnswerBallForTimePeriod1
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND dt>=@dt1 AND dt<=@dt2 AND
	us_balls=(SELECT MAX(us_balls) FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND dt>=@dt1 AND dt<=@dt2) AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt AND dt>=@dt1 AND dt<=@dt2);
  END;
GO
CREATE PROC ShowPeopleLastAnswerBalls1
(
  @ques AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt AND dt=(SELECT MAX(dt) FROM ((dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) JOIN dbo.ExamResults ON Answers.id_ex=ExamResults.id AND TestQuestions.id_bl=ExamResults.id_bl) WHERE ques=@ques GROUP BY ExamResults.id_us));
  END;
GO
CREATE PROC ShowPeopleLastAnswerBallsForTimePeriod1
(
  @ques AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt AND dt>=@dt1 AND dt<=@dt2 AND 
	dt=(SELECT MAX(dt) FROM ((dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) JOIN dbo.ExamResults ON Answers.id_ex=ExamResults.id AND TestQuestions.id_bl=ExamResults.id_bl) WHERE ques=@ques AND dt>=@dt1 AND dt<=@dt2));
  END;

GO
CREATE PROC ShowUserLastAnswerBall1
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE Answers.id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt AND dt=(SELECT MAX(dt) FROM ((dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) JOIN dbo.ExamResults ON Answers.id_ex=ExamResults.id AND TestQuestions.id_bl=ExamResults.id_bl) WHERE Answers.id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques));
  END;
GO
CREATE PROC ShowUserLastAnswerBallForTimePeriod1
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255),
  @procnt AS INT,
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE Answers.id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND
	id_ex=(SELECT id FROM ExamResults WHERE procnt>=@procnt AND dt>=@dt1 AND dt<=@dt2 AND
	dt=(SELECT MAX(dt) FROM ((dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) JOIN dbo.ExamResults ON Answers.id_ex=ExamResults.id AND TestQuestions.id_bl=ExamResults.id_bl) WHERE Answers.id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND dt>=@dt1 AND dt<=@dt2));
  END;