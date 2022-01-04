USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[ShowBestUserAnswerInTest]    Script Date: 15.05.2019 22:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ShowBestUserAnswerInTest]
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100)
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, procnt, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND
	procnt=(SELECT MAX(procnt) FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname));
  END;
