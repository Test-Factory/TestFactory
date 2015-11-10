CREATE TABLE [dbo].[Subject](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[GroupId] [nvarchar](45) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Subject_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id]));


