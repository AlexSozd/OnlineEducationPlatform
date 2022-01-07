use Edaibd;

GO
CREATE PROC ShowUserLastAnswerBall
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255)
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE Answers.id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND
	id_ex=(SELECT id FROM ExamResults WHERE dt=(SELECT MAX(dt) FROM ((dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) JOIN dbo.ExamResults ON Answers.id_ex=ExamResults.id AND TestQuestions.id_bl=ExamResults.id_bl) WHERE Answers.id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques));
  END;
GO
CREATE PROC ShowUserLastAnswerBallForTimePeriod
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE Answers.id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND
	id_ex=(SELECT id FROM ExamResults WHERE dt>=@dt1 AND dt<=@dt2 AND
	dt=(SELECT MAX(dt) FROM ((dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) JOIN dbo.ExamResults ON Answers.id_ex=ExamResults.id AND TestQuestions.id_bl=ExamResults.id_bl) WHERE Answers.id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND dt>=@dt1 AND dt<=@dt2));
  END;