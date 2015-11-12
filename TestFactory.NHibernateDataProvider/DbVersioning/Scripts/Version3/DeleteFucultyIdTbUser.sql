ALTER TABLE[dbo].[User] DROP CONSTRAINT [FK_User_Faculty]
go
alter table [dbo].[User] DROP COLUMN FacultyId
go

alter table [dbo].[User] add Faculty nvarchar(45) 
go
update [dbo].[User]
set Faculty = 'FICT'
