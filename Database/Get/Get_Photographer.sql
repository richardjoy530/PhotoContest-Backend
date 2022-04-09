CREATE PROCEDURE [dbo].[Get_Photographer]
	@Id					int				= NULL,
	@UploaderName		varchar(100)	= NULL,
	@CheckId			bit	= 0,
	@CheckUploaderName	bit	= 0
AS

SELECT [Id]
      ,[UploaderName]
  FROM [dbo].[Photographer]
  WHERE	(@CheckId			= 1	AND [Id] = @Id)						OR (@CheckId			= 0)
  AND	(@CheckUploaderName	= 1	AND [UploaderName] = @UploaderName)	OR (@CheckUploaderName	= 0)

RETURN 0
