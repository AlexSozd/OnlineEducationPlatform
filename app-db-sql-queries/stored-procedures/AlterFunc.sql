USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[ShowUserAnswersInTestPrompt]    Script Date: 16.07.2018 21:19:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ShowUserAnswersInTestPrompt]
(
  @id_ex AS INT,
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    --SELECT id, mark, real_balls, max_res, dt FROM (dbo.Answers JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE name=@usname;
    SELECT Answers.id, us_ans, tr_ans, us_balls, balls FROM (dbo.Answers JOIN dbo.TestQuestions ON Answers.id_bl=TestQuestions.id_bl) WHERE id_ex=@id_ex AND id_us=(SELECT id FROM dbo.Users WHERE name=@usname);
  END;
