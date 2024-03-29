USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[ShowLastUserAnswerInTest]    Script Date: 15.05.2019 22:47:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ShowLastUserAnswerInTest]
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, procnt, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND
	dt=(SELECT MAX(dt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname));
  END;
