﻿CREATE PROCEDURE Contest_Update
    @Id             int,
    
    @Theme          varchar(100) = NULL,
    @EndDate        datetime    = NULL,
    
    @UpdateTheme    bit = 0,
    @updateEndDate  bit = 0
AS

UPDATE Contest SET 
    Theme    = IIF(@UpdateTheme     = 1, @Theme,    Theme),
    EndDate  = IIF(@updateEndDate   = 1, @EndDate,  EndDate)
 WHERE Id = @Id And IsDeleted = 0