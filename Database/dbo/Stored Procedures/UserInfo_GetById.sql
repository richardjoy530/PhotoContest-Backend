﻿CREATE PROCEDURE UserInfo_GetById
    @Id int
AS

SELECT * FROM UserInfo WHERE Id = @Id And IsDeleted = 0