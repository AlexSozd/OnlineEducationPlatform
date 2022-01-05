use Edaibd;
/*CREATE TABLE dbo.UserAccess
(
   id INT NOT NULL PRIMARY KEY,
   name NVARCHAR(255) NOT NULL FOREIGN KEY REFERENCES dbo.Users(name),
   test_av NVARCHAR(255) NULL,
   test_results NVARCHAR(255) NULL,
   stat_av NVARCHAR(255) NOT NULL
);*/

GO
CREATE PROC CheckTestAccess
(
  @id_us AS INT
)
AS
  BEGIN
    SELECT test_av FROM UserAccess WHERE id_us=@id_us;
  END;
GO
CREATE PROC CheckContentAccess
(
  @id_us AS INT
)
AS
  BEGIN
    SELECT cont_av FROM UserAccess WHERE id_us=@id_us;
  END;
GO
CREATE PROC CheckStatAccess
(
  @id_us AS INT
)
AS
  BEGIN
    SELECT stat_av FROM UserAccess WHERE id_us=@id_us;
  END;
