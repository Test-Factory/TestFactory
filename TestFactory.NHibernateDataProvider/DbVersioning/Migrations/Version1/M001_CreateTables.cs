using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations
{
    [Migration(201510220252)]
    public class M001_Initial: Migration 
    {
        public override void Up()
        {
            Execute.Sql(
                @" 
                CREATE TABLE [dbo].[Category] ( 
                    [Id]                       NVARCHAR (45)   NOT NULL, 
                    [Name]                     NVARCHAR (30)   NULL,     
                    [Code]                     NVARCHAR (10)   NULL,     
                    [ACloseTypes]              NVARCHAR (300)  NULL,     
                    [Appreciate]               NVARCHAR (300)  NULL,     
                    [Details]                  NVARCHAR (2500) NULL,     
                    [Features]                 NVARCHAR (300)  NULL,     
                    [Likes]                    NVARCHAR (300)  NULL,     
                    [OppositeType]             NVARCHAR (30)   NULL,     
                    [PreferredAreasOfActivity] NVARCHAR (1300) NULL,     
                    PRIMARY KEY CLUSTERED ([Id] ASC)                     
                    );

                CREATE TABLE [dbo].[Group] (  
                    [Id]        NVARCHAR (45) NOT NULL, 
                    [FullName]  NVARCHAR (50) NULL,     
                    [ShortName] NVARCHAR (20) NULL,
                    [Faculty]   NVARCHAR (20) NULL,     
                    PRIMARY KEY CLUSTERED ([Id] ASC));  
					

                CREATE TABLE [dbo].[Student] (    
                    [Id]        NVARCHAR (45) NOT NULL,
                    [FirstName] NVARCHAR (70) NULL,
                    [LastName]  NVARCHAR (70) NULL,
                    [GroupId]   NVARCHAR (45) NOT NULL,
                    PRIMARY KEY CLUSTERED ([Id] ASC),
	                CONSTRAINT [FK_Student_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id])
                );


	                CREATE TABLE [dbo].[Mark] (
                    [Id]         NVARCHAR (45) NOT NULL,
                    [StudentId]  NVARCHAR (45) NOT NULL,
                    [CategoryId] NVARCHAR (45) NOT NULL,
                    [Value]      INT           NULL,
                    PRIMARY KEY CLUSTERED ([Id] ASC),
                    CONSTRAINT [FK_Mark_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([Id]),
	                CONSTRAINT [FK_Mark_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
                );
                  
                CREATE TABLE [dbo].[Role] (
                    [Id]   NVARCHAR (45) NOT NULL,
                    [Name] NVARCHAR (30) NULL,
                    PRIMARY KEY CLUSTERED ([Id] ASC)
                );

                CREATE TABLE [dbo].[User] (
                    [Id]           NVARCHAR (45)  NOT NULL,
                    [Email]        NVARCHAR (50)  NULL,
                    [Password]     NVARCHAR (255) NULL,
                    [PasswordSalt] NVARCHAR (255) NULL,
                    [FirstName]    NVARCHAR (30)  NULL,
                    [LastName]     NVARCHAR (30)  NULL,
                    [Roles_id]     NVARCHAR (45)  NULL,
                    [Faculty]      NVARCHAR (20) NULL,
                    PRIMARY KEY CLUSTERED ([Id] ASC),
                    CONSTRAINT [FK_User_Role] FOREIGN KEY ([Roles_id]) REFERENCES [dbo].[Role] ([Id])
                );
            ");
        }

        public override void Down()
        {
            Execute.Sql(@"
                DROP TABLE [dbo].[Category]

                DROP TABLE [dbo].[Group]

                DROP TABLE [dbo].[Student]

                DROP TABLE [dbo].[Mark]

                DROP TABLE [dbo].[Role]

                DROP TABLE [dbo].[User]

            ");
        }
    }
}
