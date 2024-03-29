﻿CREATE PROCEDURE [dbo].[UserInfo_Insert]
    @Id     int OUTPUT,
    @Name       varchar(100) = NULL,
    @RefId       varchar(100) = NULL,
	@Email varchar(100) = NULL,
	@RegistrationDate date = NULL
AS

INSERT INTO UserInfo (Name, RefId, Email, RegistrationDate) VALUES (@Name, @RefId, @Email, @RegistrationDate)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0