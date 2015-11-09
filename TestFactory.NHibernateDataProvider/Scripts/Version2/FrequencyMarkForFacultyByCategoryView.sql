create view FrequencyMarkForFacultyByCategory
as
select  g.Faculty, c.Code,  c.Id, m.Value,  COUNT(m.Value) Count
    from [Mark] m
    join Student s on m.StudentId = s.Id
    join [Group] g on s.GroupId = g.Id
    join Category c on c.Id = m.CategoryId
    group by g.Faculty, c.Code, c.Id,  m.Value
