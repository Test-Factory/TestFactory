create view SubjectWithGroup  as
select s.Name,sg.SubjectId, g.Id as GroupId
 from Subject s
 join Subject_Group sg on s.Id = sg.SubjectId
 join [Group] g on g.Id = sg.GroupId
 group by s.Name, sg.SubjectId, g.Id










