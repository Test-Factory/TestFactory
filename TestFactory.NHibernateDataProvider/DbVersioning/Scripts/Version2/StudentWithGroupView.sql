CREATE VIEW StudentWithGroup as
SELECT        s.Id,s.LastName, s.Year, s.FirstName, g.ShortName, s.GroupId
FROM            dbo.[Group] g INNER JOIN
                         dbo.Student s ON g.Id = s.GroupId