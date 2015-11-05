     ALTER TABLE[dbo].[Student] DROP CONSTRAINT [FK_Student_Group]

    ALTER TABLE[dbo].[Mark] DROP CONSTRAINT [FK_Mark_Student]

    ALTER TABLE[dbo].[Mark] DROP CONSTRAINT [FK_Mark_Category]

    ALTER TABLE[dbo].[User] DROP CONSTRAINT [FK_User_Role]      
         
    DROP TABLE [dbo].[Student] 

    DROP TABLE [dbo].[Mark] 

    DROP TABLE [dbo].[Category]

    DROP TABLE [dbo].[User]

    DROP TABLE [dbo].[Group]

    DROP TABLE [dbo].[Role]

    DROP TABLE [dbo].[VersionInfo]