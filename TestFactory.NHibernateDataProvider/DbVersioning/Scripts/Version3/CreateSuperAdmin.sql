INSERT INTO [dbo].[Faculty] ([Id], [Name]) VALUES (N'1', N'AdminFaculty') 

INSERT INTO [dbo].[User] ([Id], [Email], [Password], [PasswordSalt], [Roles_id], [FacultyId]) VALUES (N'6be4c229-3846-4f5e-93e5-e803cf4b233d', N'Admin@gmail.com', N'CAab3uyAXipwCE1EZiMv4g==', N'LmHjld5dwrNFRJWrLzYB/6OdQ2gcw2NOlvkk4/tPweo=', N'439bdb3f-4fe6-4def-a329-09a20ed888dc', N'1')