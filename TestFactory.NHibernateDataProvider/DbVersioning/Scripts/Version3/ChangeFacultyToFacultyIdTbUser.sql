alter table [User] DROP COLUMN Faculty; 
alter table [User] add FacultyId nvarchar(50) null 
go 
UPDATE [dbo].[User] 
SET FacultyId ='FBF5D69A-54F8-4146-AC9C-417BAA3E5122' 
where [dbo].[User].Email ='FillerFICT@gmail.com' or [dbo].[User].Email ='EditorFICT@gmail.com'; 

UPDATE [dbo].[User] 
SET FacultyId ='49132E51-720E-46AD-A139-BED84F8D616F' 
where [dbo].[User].Email ='FillerFOFF@gmail.com' or [dbo].[User].Email ='EditorFOFF@gmail.com'; 

alter table [User] add CONSTRAINT [FK_User_Faculty] FOREIGN KEY ([FacultyId]) REFERENCES [dbo].[Faculty] ([Id]) ON DELETE CASCADE;