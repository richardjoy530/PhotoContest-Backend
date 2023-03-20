create procedure Contest_GetAllIds
as 
select Id from Contest WHERE IsDeleted = 0