﻿CREATE PROCEDURE Contest_GetAllIds
AS 
SELECT Id FROM Contest WHERE IsDeleted = 0