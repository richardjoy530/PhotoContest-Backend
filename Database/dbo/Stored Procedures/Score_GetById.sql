﻿CREATE PROCEDURE Score_GetById
@Id int
AS

SELECT * FROM Score WHERE Id = @Id