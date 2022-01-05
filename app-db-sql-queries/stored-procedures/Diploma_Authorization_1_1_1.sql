use Edaibd;

GO
CREATE PROC FindID1
(
  @login AS NVARCHAR(100)
)
AS
  BEGIN
  SELECT id FROM Users WHERE name=@login;
  END;

GO
CREATE PROC FindLogin1
(
  @login AS NVARCHAR(100)
)
AS
  BEGIN
  SELECT COUNT(*) FROM Users WHERE name=@login;
  END;

GO
CREATE PROC FindLoginAndPassword1
(
  @login AS NVARCHAR(100),
  @pass AS NVARCHAR(100)
)
AS
  BEGIN
  SELECT COUNT(*) FROM Users WHERE name=@login AND parole=@pass;
  END;
