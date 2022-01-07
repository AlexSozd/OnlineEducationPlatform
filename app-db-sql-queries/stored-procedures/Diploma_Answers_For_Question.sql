use Edaibd;

GO
CREATE PROC ShowPeopleAnswers
(
  @ques AS NVARCHAR(255)
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE ques=@ques;
  END;

GO
CREATE PROC ShowUserAnswer
(
  @usname AS NVARCHAR(100),
  @ques AS NVARCHAR(255)
)
AS
  BEGIN
    SELECT Answers.us_balls AS us_balls, TestQuestions.balls AS balls, Answers.us_ans AS us_ans, Answers.tr_ans AS tr_ans FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.tr_ans=TestQuestions.r_ans) WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname) AND ques=@ques;
  END;