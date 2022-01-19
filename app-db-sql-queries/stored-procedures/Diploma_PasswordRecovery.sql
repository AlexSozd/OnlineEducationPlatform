use Edaibd;

/*CREATE TABLE dbo.Users
(
   id INT NOT NULL PRIMARY KEY,
   name NVARCHAR(255) NOT NULL,
   cell_tel NVARCHAR(255) NULL,
   e_mail NVARCHAR(255) NULL,
   parole NVARCHAR(255) NOT NULL
);
INSERT INTO dbo.Users(id, name, cell_tel, e_mail, parole) VALUES
       (1, 'Tester1', '+7(917)777-33-55', 'tester@mail.ru', 'RQSP4511');

*/

GO
CREATE PROCEDURE UpdatePassword
(
  @log AS NVARCHAR,
  @pass AS NVARCHAR
)
AS
  BEGIN
    UPDATE dbo.Users
    SET parole=@pass WHERE name=@log;
  END;
