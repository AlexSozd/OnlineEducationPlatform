USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[ShowBestResultsByTest1]    Script Date: 18.05.2019 18:28:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ShowBestResultsByTest1]
(
  @tname AS NVARCHAR(255),
  @procnt AS INT
)
AS
  BEGIN
    SELECT ExamResults.id AS id, name, dt, real_balls, max_res, procnt, mark FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) AND
	procnt=(SELECT MAX(procnt) FROM (dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) WHERE id_bl=(SELECT id FROM dbo.TestBlocks WHERE name=@tname) GROUP BY Users.id) AND procnt>=@procnt;
  END;
