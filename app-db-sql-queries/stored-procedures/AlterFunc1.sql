USE [Edaibd]
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 17.07.2018 17:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[AddUser]
(
   @id_us AS INT,
   @name AS NVARCHAR(255),
   @c_tel AS NVARCHAR(255),
   @e_m AS NVARCHAR(255),
   @par AS NVARCHAR(255)
)
AS
  BEGIN
    INSERT INTO dbo.Users(id, name, cell_tel, e_mail, parole) VALUES (@id_us, @name, @c_tel, @e_m, @par);
  END;
