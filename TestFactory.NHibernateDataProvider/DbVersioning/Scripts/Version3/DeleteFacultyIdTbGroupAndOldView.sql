ALTER TABLE[dbo].[Group] DROP CONSTRAINT [FK_Group_Faculty]
go
alter table [dbo].[Group] DROP COLUMN FacultyId
go
alter table [dbo].[Group] add Faculty nvarchar(45) 
go
update [dbo].[Group]
set Faculty = 'FICT'
go
drop view AverageMarkForFaculty
go 
drop view FrequencyMarkForFacultyByCategory
go
create view AverageMarkForFaculty
                        as
                        select  g.Faculty,c.Code,  c.Id,round(AVG(Cast(value as float)),2) Average
                        from [Mark] m
                        join Student s on m.StudentId = s.Id
                        join [Group] g on s.GroupId = g.Id
                        join Category c on c.Id = m.CategoryId
                        group by g.Faculty, c.Code, c.Id
go
create view FrequencyMarkForFacultyByCategory
as
select g.Faculty, c.Code,  c.Id as CategoryId, m.Value,  COUNT(m.Value) Count
    from [Mark] m
    join Student s on m.StudentId = s.Id
    join [Group] g on s.GroupId = g.Id
    join Category c on c.Id = m.CategoryId
    group by g.Faculty, c.Code, c.Id,  m.Value