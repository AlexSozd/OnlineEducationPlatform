USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[ShowUserResults1]    Script Date: 17.07.2018 17:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ShowUserResults1]
(
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    --SELECT id, mark, real_balls, max_res, dt FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE name=@usname;
    SELECT id, dt, real_balls, max_res, procnt, mark FROM dbo.ExamResults WHERE id_us=(SELECT id FROM dbo.Users WHERE name=@usname);
  END;
