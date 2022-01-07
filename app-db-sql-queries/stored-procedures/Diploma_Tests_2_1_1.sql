use Edaibd;

GO
ALTER PROC ShowPeopleAnswersForTimePeriod
(
  @ques AS NVARCHAR(255),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques AND 
	id_ex=(SELECT id FROM dbo.ExamResults WHERE dt>=@dt1 AND dt<=@dt2);
  END;

GO
ALTER PROC ShowUserAnswerForTimePeriod
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques AND 
	id_ex=(SELECT id FROM dbo.ExamResults WHERE dt>=@dt1 AND dt<=@dt2);
  END;