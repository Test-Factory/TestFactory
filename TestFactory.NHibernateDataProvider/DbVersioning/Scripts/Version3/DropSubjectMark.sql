ALTER TABLE[dbo].[SubjectMark] DROP CONSTRAINT [FK_SubjectMark_Student];
ALTER TABLE[dbo].[SubjectMark] DROP CONSTRAINT [FK_SubjectMark_Subject];
drop table [SubjectMark];