CREATE PROCEDURE [dbo].[Detele_FileMap]
	@Id int
AS

DELETE [dbo].[FileMap]
 WHERE [Id] = @Id
 
RETURN 0
