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
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[GroupForUser] (
    [Id]      NVARCHAR (45) NOT NULL,
    [GroupId] NVARCHAR (45) NOT NULL,
    [UserId]  NVARCHAR (45) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FKF2AAE8E313290C64] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

CREATE TABLE [dbo].[Mark] (
    [Id]         NVARCHAR (45) NOT NULL,
    [StudentId]  NVARCHAR (45) NOT NULL,
    [CategoryId] NVARCHAR (45) NOT NULL,
    [Value]      INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK787D19F3F7CDB4A8] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([Id])
);


CREATE TABLE [dbo].[Role] (
    [Id]   NVARCHAR (45) NOT NULL,
    [Name] NVARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



CREATE TABLE [dbo].[Student] (
    [Id]        NVARCHAR (45) NOT NULL,
    [FirstName] NVARCHAR (70) NULL,
    [LastName]  NVARCHAR (70) NULL,
    [GroupId]   NVARCHAR (45) NOT NULL,
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
    [Faculty]      NVARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK7185C17C4ED5C847] FOREIGN KEY ([Roles_id]) REFERENCES [dbo].[Role] ([Id])
);

CREATE TABLE [dbo].[VersionInfo] (
    [Version]     BIGINT          NOT NULL,
    [AppliedOn]   DATETIME        NULL,
    [Description] NVARCHAR (1024) NULL
);
