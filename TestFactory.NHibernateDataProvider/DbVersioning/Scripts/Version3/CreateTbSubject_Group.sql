 CREATE TABLE [dbo].[Subject_Group] (
	[GroupId] [nvarchar](45) NOT NULL,
	[SubjectId] [nvarchar](50) NOT NULL,
    PRIMARY KEY CLUSTERED ([GroupId] ASC,
						   [SubjectId] ASC),
	CONSTRAINT [FK_Subject_Group_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id]),
	CONSTRAINT [FK_Subject_Group_Subject] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([Id])
);


