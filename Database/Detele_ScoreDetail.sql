CREATE PROCEDURE [dbo].[Detele_ScoreDetail]
	@Id	int
AS

DELETE FROM [dbo].[ScoreDetail]
      WHERE [Id] = @Id

RETURN 0
