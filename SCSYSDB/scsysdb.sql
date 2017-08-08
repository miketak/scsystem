IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'SCSYSDB')
BEGIN
  Print '' print  ' *** dropping database SCSYSDB'
  DROP DATABASE SCSYSDB
End
GO
print '' print '*** creating database SCSYSDB'
GO
CREATE DATABASE SCSYSDB
GO

print '' print '*** using database SCSYSDB'
GO
USE [SCSYSDB]
GO

print 'Created Solutions Table'
GO
Create table Solutions
(
	[SolutionsId] 				INT NOT NULL PRIMARY KEY IDENTITY,
	[ImageData] 				varbinary (max) default null,
	[ImageMimeType]				varchar (50) default null,
	[Name] NVARCHAR(100) 		NOT NULL,
	[DESCRIPTION] NVARCHAR(512) 	NOT NULL,
)
GO

print 'Created Services Table'
GO
Create table Services
(
	[ServicesId] 				INT NOT NULL PRIMARY KEY IDENTITY,
	[ImageData] 				varbinary (max) default null,
	[ImageMimeType]				varchar (50) default null,
	[Name] 						NVARCHAR(100) NOT NULL,
	[DESCRIPTION] NVARCHAR(512) NOT NULL,
)
GO

print 'Create Testimonials Table'
GO
Create table Testimonials
(
	[Id] 					INT NOT NULL PRIMARY KEY IDENTITY,
	[Message] 				varchar(512) not null,
	[Author]				varchar (512) default null
)
GO

Create table Solutions_Services
(
	[SolutionsID]	INT,
	[ServicesID]	INT
)

print 'Created Contact Us Details Table'
GO
CREATE TABLE ContactMessages
(
	[Id]				INT NOT NULL PRIMARY KEY IDENTITY,
	[Name]				NVARCHAR(256) NOT NULL,
	[Email]				NVARCHAR(256) NOT NULL,
	[Subject]			NVARCHAR(256) NOT NULL,
	[MessageBody]		NVARCHAR(1024) NOT NULL,	
	[TimePosted]		DATE NOT NULL
)
GO

print '' print '*** Creating Foreign Key Solutions_Services SolutionsID'
GO
ALTER TABLE [dbo].[Solutions_Services] with nocheck
  ADD CONSTRAINT[fk_SS_SolutionsID] FOREIGN KEY (SolutionsID)
  References [dbo].[Solutions](SolutionsID)
  ON UPDATE CASCADE
  ON DELETE CASCADE
GO

print '' print '*** Creating Foreign Key Solutions_Services ServicesID'
GO
ALTER TABLE [dbo].[Solutions_Services] with nocheck
  ADD CONSTRAINT[fk_SS_ServicesID] FOREIGN KEY (ServicesID)
  References [dbo].[Services](ServicesID)
  ON UPDATE CASCADE
  ON DELETE CASCADE
GO

print '*** Inserting Solutions Data ***'
GO
INSERT INTO [dbo].[Solutions]
	(Name, Description)
	VALUES
	('Agriculture', 'The need for agriculture has been at the epicenter of human survival since the beginning of time. Between 1990 and 2010, the world population grew by 30%. This has necessitated rethinking agriculture to cope with this global population growth.'),
	('MANUFACTURING TECHNOOGY', 'Manufacturing technology has evolved phenomenally since the industrial evolution. Human innovation has been the backbone of the modern life of everyone. Along with the constant increase in complexity and efficiency of manufacturing tech, has also been major leaps in engineering design methods.'),
	('RENEWABLE ENERGY', 'Renewable Energy is the future for energy generation in the near future. The transition from fossil fuel energy to renewables is globally being adopted to ensure a sustainable future. Renewable energy has now become an important component to consider to bridge the gap between globally declared goals on sustainability and our current reliance on fossil fuels for energy.')
GO

print '*** Inserting Services Data ***'
GO
INSERT INTO [dbo].[Services]
	(Name, Description)
	VALUES
	('Cad Drafting', 'The modern engineer or inventor in a world of increasing complexity and technology has one problem they have to overcome: to deliver.'),
	('Finite Element Analysis', 'Evolution of engineering since the industrial age has been hand-in-hand with increasing computational power. This has given us the ability to analyze and make predictions at an unprecedented pace.'),
	('Multiphysics Simulation', 'At SCCL, engineers are more dedicated to deliver and solve your engineering challenge to the highest precision possible. To ensure an ultimate solution to any engineering problem, this is the most assured way SCCL could help.')
GO

print '*** Inserting Solutions Services Data ***'
GO
INSERT INTO [dbo].[Solutions_Services]
	(SolutionsID, ServicesID)
	VALUES
	(1,1),
	(2,1),
	(2,2),
	(3,1),
	(3,2),
	(3,3)
GO

print '*** Inserting Testimonials Data ***'
GO
INSERT INTO [dbo].[Testimonials]
	(Message, Author)
	VALUES
	('These people are the best', 'Dr. Frank Nyarko - Mechanical Engineering Dept, KNUST'),
	('I worked with them before and they are sure a mark of excellence above normal standards', 'Richard Adokoh - Ghana Standards Authority'),
	('Diverse talents grouped in one single place','Mr. Aveyire - Agricultural Engineering Dept, KNUST' )
GO

print '' print  ' *** creating procedure sp_retrieve_solutions'
GO
Create PROCEDURE sp_retrieve_solutions
AS
BEGIN
SELECT SolutionsId, Name, Description, ImageMimeType, ImageData
FROM Solutions
END

print '' print  ' *** creating procedure sp_retrieve_services'
GO
Create PROCEDURE sp_retrieve_services
AS
BEGIN
SELECT ServicesId, Name, Description, ImageMimeType, ImageData
FROM Services
END
GO

print '' print  ' *** creating procedure sp_retrieve_testimonials'
GO
Create PROCEDURE sp_retrieve_testimonials
AS
BEGIN
SELECT Id, Message, Author
FROM Testimonials
END

print '' print '*** Creating sp_update_solution'
GO
CREATE PROCEDURE [dbo].[sp_update_solution]
	(
		@Id						[int],
		
		@OldName				[nvarchar](100), 
		@OldDescription			[nvarchar](512), 
		
		@NewName				[nvarchar](100), 
		@NewDescription			[nvarchar](512),
		@NewImageData			[varbinary](max),
		@NewImageMimeType		[varchar](50)
	)
AS
	BEGIN
		UPDATE Solutions
		  SET Name = @NewName,
			  Description = @NewDescription,
			  ImageData = @NewImageData,
			  ImageMimeType = @NewImageMimeType
		WHERE SolutionsId = @Id
		  AND Name = @OldName
		  AND Description = @OldDescription
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_service'
GO
CREATE PROCEDURE [dbo].[sp_update_service]
	(
		@Id					[int],
		
		@OldName				[nvarchar](100), 
		@OldDescription			[nvarchar](512),
		
		@NewName				[nvarchar](100), 
		@NewDescription			[nvarchar](512),
		@NewImageData			[varbinary](max),
		@NewImageMimeType		[varchar](50) 
	)
AS
	BEGIN
		UPDATE Services
		      SET Name = @NewName,
				  Description = @NewDescription,
				  ImageData = @NewImageData,
				  ImageMimeType = @NewImageMimeType
			WHERE ServicesId = @Id
			  AND Name = @OldName
			  AND Description = @OldDescription
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_testimonial'
GO
CREATE PROCEDURE [dbo].[sp_update_testimonial]
	(
		@Id						[int],
		
		@oldauthor				[varchar](512), 
		@oldmessage				[varchar](512), 
		
		@newauthor				[varchar](512), 
		@newmessage				[varchar](512)
		
		
	)
AS
	BEGIN
		UPDATE Testimonials
		  SET Author = @newauthor,
			  Message = @newmessage
		WHERE Id = @Id
		  AND Author = @oldauthor
		  AND Message = @oldmessage
		RETURN @@ROWCOUNT
	END
GO

print '' print  '*** Creating procedure sp_delete_testimonial'
GO
CREATE PROCEDURE sp_delete_testimonial
(
	@ID				[INT]
)
AS
	BEGIN
		DELETE FROM Testimonials
		WHERE Id = @ID
		RETURN @@ROWCOUNT
	END
GO

print '' print  '*** Creating procedure sp_delete_solution'
GO
CREATE PROCEDURE sp_delete_solution
(
	@ID[INT]
)
AS
	BEGIN
		DELETE FROM Solutions
		WHERE SolutionsId = @ID
		RETURN @@ROWCOUNT
	END
GO



print '' print  '*** Creating procedure sp_delete_service'
GO
CREATE PROCEDURE sp_delete_service
(
	@ID[INT]
)
AS
	BEGIN
		DELETE FROM Services
		WHERE ServicesId = @ID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_solution'
GO
CREATE PROCEDURE [dbo].[sp_create_solution]
(
	@NAME				[NVARCHAR](100),
	@DESCRIPTION		[NVARCHAR](512),
	@ImageData			[varbinary](max),
	@ImageMimeType		[varchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[Solutions]
			(NAME, DESCRIPTION, ImageData, ImageMimeType)
		VALUES
			(@NAME, @DESCRIPTION, @ImageData, @ImageMimeType)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_service'
GO
CREATE PROCEDURE [dbo].[sp_create_service]
(
	@NAME				[NVARCHAR](100),
	@DESCRIPTION		[NVARCHAR](512),
	@ImageData			[varbinary](max),
	@ImageMimeType		[varchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[Services]
			(NAME, DESCRIPTION, ImageData, ImageMimeType)
		VALUES
			(@NAME, @DESCRIPTION, @ImageData, @ImageMimeType)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_testimonial'
GO
CREATE PROCEDURE [dbo].[sp_create_testimonial]
(
	@AUTHOR				[VARCHAR](512),
	@MESSAGE			[VARCHAR](512)
)
AS
	BEGIN
		INSERT INTO [dbo].[Testimonials]
			(AUTHOR, MESSAGE)
		VALUES
			(@AUTHOR, @MESSAGE)
		RETURN @@ROWCOUNT
	END
GO

--Contact Messages Stored Procedures

print 'Creating sp_ContactMessagesSelect_By_Id'
IF OBJECT_ID('[dbo].[sp_ContactMessagesSelect_By_Id]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_ContactMessagesSelect_By_Id] 
END 
GO
CREATE PROC [dbo].[sp_ContactMessagesSelect_By_Id] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN

	SELECT [Id], [Name], [Email], [Subject], [MessageBody], [TimePosted] 
	FROM   [dbo].[ContactMessages] 
	WHERE  ([Id] = @Id OR @Id IS NULL)
	
	END
GO

print 'Creating sp_ContactMessagesSelect'
IF OBJECT_ID('[dbo].[sp_ContactMessagesSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_ContactMessagesSelect] 
END 
GO
CREATE PROC [dbo].[sp_ContactMessagesSelect] 
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN

	SELECT [Id], [Name], [Email], [Subject], [MessageBody], [TimePosted] 
	FROM   [dbo].[ContactMessages]
	
	END
GO

print 'Creating sp_ContactMessagesInsert'
IF OBJECT_ID('[dbo].[sp_ContactMessagesInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_ContactMessagesInsert] 
END 
GO
CREATE PROC [dbo].[sp_ContactMessagesInsert] 
    @Name nvarchar(256),
    @Email nvarchar(256),
    @Subject nvarchar(256),
    @MessageBody nvarchar(1024),
    @TimePosted date
AS 
	SET NOCOUNT OFF 
	SET XACT_ABORT ON  
	
	BEGIN 
	
	INSERT INTO [dbo].[ContactMessages] ([Name], [Email], [Subject], [MessageBody], [TimePosted])
	SELECT @Name, @Email, @Subject, @MessageBody, @TimePosted
	
	RETURN @@ROWCOUNT
	
	END
GO

print 'Creating sp_ContactMessagesUpdate'
IF OBJECT_ID('[dbo].[sp_ContactMessagesUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_ContactMessagesUpdate] 
END 
GO
CREATE PROC [dbo].[sp_ContactMessagesUpdate] 
    @Id int,
    @Name nvarchar(256),
    @Email nvarchar(256),
    @Subject nvarchar(256),
    @MessageBody nvarchar(1024),
    @TimePosted date
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN 

	UPDATE [dbo].[ContactMessages]
	SET    [Name] = @Name, [Email] = @Email, [Subject] = @Subject, [MessageBody] = @MessageBody, [TimePosted] = @TimePosted
	WHERE  [Id] = @Id
	
	RETURN @@ROWCOUNT
	END
GO

print 'Creating sp_ContactMessagesDelete'
IF OBJECT_ID('[dbo].[sp_ContactMessagesDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_ContactMessagesDelete] 
END 
GO
CREATE PROC [dbo].[sp_ContactMessagesDelete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN

	DELETE
	FROM   [dbo].[ContactMessages]
	WHERE  [Id] = @Id
	
	END
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

