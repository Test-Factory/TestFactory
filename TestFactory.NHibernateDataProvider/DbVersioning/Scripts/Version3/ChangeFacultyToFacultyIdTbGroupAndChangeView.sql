alter table [Group] DROP COLUMN Faculty;
alter table [Group] add  FacultyId nvarchar(50) null
go
UPDATE [dbo].[Group]
SET FacultyId ='FBF5D69A-54F8-4146-AC9C-417BAA3E5122'
go
ALTER TABLE [dbo].[Group]
ALTER COLUMN FacultyId nvarchar(50) not null
go
alter table [Group] add CONSTRAINT [FK_Group_Faculty] FOREIGN KEY ([FacultyId]) REFERENCES [dbo].[Faculty] ([Id]) ON DELETE CASCADE
go
drop view AverageMarkForFaculty
go 
drop view FrequencyMarkForFacultyByCategory
go
create view AverageMarkForFaculty
                        as
                        select  g.FacultyId,c.Code,  c.Id,round(AVG(Cast(value as float)),2) Average
                        from [Mark] m
                        join Student s on m.StudentId = s.Id
                        join [Group] g on s.GroupId = g.Id
                        join Category c on c.Id = m.CategoryId
                        group by g.FacultyId, c.Code, c.Id
go
create view FrequencyMarkForFacultyByCategory
as
select g.FacultyId, c.Code,  c.Id as CategoryId, m.Value,  COUNT(m.Value) Count
    from [Mark] m
    join Student s on m.StudentId = s.Id
    join [Group] g on s.GroupId = g.Id
    join Category c on c.Id = m.CategoryId
	group by g.FacultyId, c.Code, c.Id,  m.Value
