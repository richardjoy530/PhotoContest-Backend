CREATE PROCEDURE [dbo].[Delete_PhotographerVoteDetails]
    @Id int = NULL
AS

DELETE FROM [dbo].[PhotographerVoteDetails]
      WHERE [Id] = @Id

RETURN 0
