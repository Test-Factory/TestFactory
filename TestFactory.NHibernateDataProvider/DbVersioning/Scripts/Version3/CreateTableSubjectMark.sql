CREATE TABLE [dbo].[SubjectMark](
	[Id] [nvarchar](50) NOT NULL,
	[SubjectId] [nvarchar](50) NOT NULL,
	[StudentId] [nvarchar](45) NOT NULL,
	[Value] FLOAT NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_SubjectMark_Subject] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([Id]),
	CONSTRAINT [FK_SubjectMark_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([Id])
	);