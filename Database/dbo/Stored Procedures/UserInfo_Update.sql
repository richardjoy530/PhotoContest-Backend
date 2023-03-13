CREATE PROCEDURE [dbo].[UserInfo_Update]
    @Id         int,
    @Name       varchar(100) = NULL,
    @RefId       varchar(100) = NULL,
	@Email varchar(100) = NULL,
	@RegistrationDate date = NULL,
    @UpdateName bit = 0,
    @UpdateEmail bit = 0,
    @UpdateRefId bit = 0,
    @UpdateRegistrationDate bit = 0
	
AS

UPDATE UserInfo SET Name = IIF(@UpdateName = 1, @Name, Name),
RefId = IIF(@UpdateRefId = 1, @RefId, RefId),
Email = IIF(@UpdateEmail = 1, @Email, Email),
RegistrationDate = IIF(@UpdateRegistrationDate = 1, @RegistrationDate, RegistrationDate)
WHERE Id = @Id And IsDeleted = 0