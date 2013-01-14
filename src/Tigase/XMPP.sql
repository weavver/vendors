DROP PROCEDURE TigUserLoginPlainPw

GO

CREATE PROCEDURE TigUserLoginPlainPw

@nickname varchar(55),
@password varchar(55)

AS

SELECT * FROM Weavver_Users

where nickname=@nickname and password=@password

GO

execute TigUserLoginPlainPw 'admin', 'test'