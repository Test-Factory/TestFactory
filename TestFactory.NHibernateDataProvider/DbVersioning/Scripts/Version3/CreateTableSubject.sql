CREATE TABLE [dbo].[Subject](
	[Id] [nvarchar](50) NOT NULL,
	[LongName] [nvarchar](100) NOT NULL,
	[ShortName] [nvarchar](15) NOT NULL,
	[FacultyId] [nvarchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Subject_Faculty] FOREIGN KEY ([FacultyId]) REFERENCES [dbo].[Faculty] ([Id]));


