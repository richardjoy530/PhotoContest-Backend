CREATE PROCEDURE [dbo].[Get_PhotoTheme]
	@Id					int,
	@Theme				varchar(50),
	@ContestDate		datetime,
	@CheckId			bit,
	@CheckTheme			bit,
	@CheckContestDate	bit
AS

SELECT [Id]
      ,[Theme]
      ,[ContestDate]
  FROM [dbo].[PhotoTheme]
  WHERE (@CheckId = 1			AND [Id] = @Id)						OR (@CheckId = 0)
  AND	(@CheckTheme = 1		AND [Theme] = @Theme)				OR (@CheckTheme = 0)
  AND	(@CheckContestDate = 1	AND [ContestDate] = @ContestDate)	OR (@CheckContestDate = 0)

RETURN 0
