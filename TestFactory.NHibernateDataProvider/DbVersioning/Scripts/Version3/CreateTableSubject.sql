CREATE TABLE [dbo].[Subject](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[FacultyId] [nvarchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Subject_Faculty] FOREIGN KEY ([FacultyId]) REFERENCES [dbo].[Faculty] ([Id]));


