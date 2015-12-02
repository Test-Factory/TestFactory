create view AverageMarkForFaculty 
as 
select g.Faculty,c.Code, c.Id,round(AVG(Cast(value as float)),2) Average 
from [Mark] m 
join Student s on m.StudentId = s.Id 
join [Group] g on s.GroupId = g.Id 
join Category c on c.Id = m.CategoryId 
group by g.Faculty, c.Code, c.Id, c.Code, c.Id, m.Value