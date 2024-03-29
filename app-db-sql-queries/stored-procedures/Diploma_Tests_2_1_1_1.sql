USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[ShowUserAnswersInTest1]    Script Date: 17.05.2019 17:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ShowUserAnswersInTest1]
(
  @tname AS NVARCHAR(255),
  @usname AS NVARCHAR(100),
  @procnt AS INT
)
AS
  BEGIN
    SELECT id, mark, real_balls, max_res, procnt, dt FROM dbo.ExamResults WHERE id_bl=(SELECT id FROM TestBlocks WHERE name=@tname) AND id_us=(SELECT id FROM Users WHERE name=@usname) AND procnt>=@procnt;
  END;
