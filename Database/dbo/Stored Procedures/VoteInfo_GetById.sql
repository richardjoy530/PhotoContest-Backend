﻿CREATE PROCEDURE VoteInfo_GetById
    @Id int
AS

SELECT * FROM VoteInfo WHERE Id = @Id And IsDeleted = 0