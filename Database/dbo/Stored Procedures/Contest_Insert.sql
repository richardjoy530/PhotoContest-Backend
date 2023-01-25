CREATE PROCEDURE Contest_Insert
    @Id         int OUTPUT,
    @Theme      varchar(50),
    @EndDate    datetime
AS

INSERT INTO Contest (theme, enddate)
    VALUES (@Theme , @EndDate)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0