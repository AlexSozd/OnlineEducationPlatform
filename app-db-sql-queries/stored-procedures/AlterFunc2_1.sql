USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[ShowUserAnswersInTestForTimePeriod]    Script Date: 13.05.2019 20:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ShowUserAnswersInTestForTimePeriod]
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @dt1 AS DATE,
  @dt2 AS DATE
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, procnt, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND (dt>=@dt1) AND (dt<=@dt2);
  END;
