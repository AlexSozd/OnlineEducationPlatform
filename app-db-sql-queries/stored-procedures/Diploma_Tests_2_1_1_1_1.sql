﻿USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[ShowPassedProcentTestResults]    Script Date: 17.05.2019 17:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[ShowPassedProcentTestResults]
(
  @stud_proc AS INT,
  @ex_proc AS INT
)
AS
  BEGIN
    DECLARE @kol AS INT
    SET @kol = (SELECT COUNT(*) FROM Users WHERE id=(SELECT id FROM UserAccess WHERE test_av='1'));
    SELECT ExamResults.id AS id, Users.name AS UserName, TestBlocks.name AS TestName, mark, real_balls, max_res, procnt, dt, trynum FROM ((dbo.ExamResults JOIN dbo.Users ON ExamResults.id_us=Users.id) JOIN dbo.TestBlocks ON ExamResults.id_bl=TestBlocks.id)
	WHERE ExamResults.id_bl=(SELECT id_bl FROM (SELECT id_bl, COUNT(*)*100/@kol AS stud_count FROM dbo.ExamResults GROUP BY id_bl, id_us) AS D WHERE stud_count>=@stud_proc) AND procnt>=@ex_proc;
  END;
