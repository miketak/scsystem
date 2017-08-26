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
	[DESCRIPTION] NVARCHAR(2048) 	NOT NULL,
	[UrlString] 				NVARCHAR(50)	
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
	[DESCRIPTION] NVARCHAR(2048) NOT NULL,
	[UrlString] 				NVARCHAR(50)
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

print 'Create Solutions_Services'
GO
Create table Solutions_Services
(
	[SolutionsID]	INT,
	[ServicesID]	INT
)
GO

print 'Created Contact Us Details Table'
GO
CREATE TABLE ContactMessages
(
	[Id]				INT NOT NULL PRIMARY KEY IDENTITY,
	[Name]				NVARCHAR(256) NOT NULL,
	[Email]				NVARCHAR(256) NOT NULL,
	[Subject]			NVARCHAR(256) NOT NULL,
	[MessageBody]		NVARCHAR(2048) NOT NULL,	
	[TimePosted]		DATETIME NOT NULL
)
GO

print 'Create Portfolio Table'
GO
CREATE TABLE Portfolio
(
	[Id]				INT NOT NULL PRIMARY KEY IDENTITY,
	[Title]				NVARCHAR(256) NOT NULL,
	[ResearchArea]		NVARCHAR(256) NOT NULL,
	[Author]			NVARCHAR(256) NOT NULL,
	[Publisher]			NVARCHAR(256)		  ,
	[RedirectLink]		NVARCHAR(256)		  ,
	[PortfolioType]		NVARCHAR(256) NOT NULL,
	[Summary]			NVARCHAR(2048) NOT NULL,
	[Thumbnail]			VARBINARY(max) default null,
	[ImageMimeType]		VARCHAR(50) default null,
)
GO

print 'Create Portfolio Images Table'
GO
CREATE TABLE PortfolioImages
(
	[Id]				INT NOT NULL PRIMARY KEY IDENTITY,
	[PortfolioId]		INT,
	[PortfolioImage]	VARBINARY(max) default null,
	[ImageMimeType]		VARCHAR(50) default null,
	CONSTRAINT FK_PortfolioID FOREIGN KEY (PortfolioId) REFERENCES Portfolio(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
		
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

print '' print  ' *** creating procedure sp_retrieve_solutions'
GO
Create PROCEDURE sp_retrieve_solutions
AS
BEGIN
SELECT SolutionsId, Name, Description, ImageMimeType, ImageData, UrlString
FROM Solutions
END

print '' print  ' *** creating procedure sp_retrieve_services'
GO
Create PROCEDURE sp_retrieve_services
AS
BEGIN
SELECT ServicesId, Name, Description, ImageMimeType, ImageData, UrlString
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
		@NewDescription			[nvarchar](2048),
		@NewImageData			[varbinary](max),
		@NewImageMimeType		[varchar](50),
		@NewUrlString			[nvarchar](50)
	)
AS
	BEGIN
		UPDATE Solutions
		  SET Name = @NewName,
			  Description = @NewDescription,
			  ImageData = @NewImageData,
			  ImageMimeType = @NewImageMimeType,
			  UrlString = @NewUrlString
		WHERE SolutionsId = @Id
		  AND Name = @OldName
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
		@NewDescription			[nvarchar](2048),
		@NewImageData			[varbinary](max),
		@NewImageMimeType		[varchar](50),
		@NewUrlString			[nvarchar](50)
	)
AS
	BEGIN
		UPDATE Services
		      SET Name = @NewName,
				  Description = @NewDescription,
				  ImageData = @NewImageData,
				  ImageMimeType = @NewImageMimeType,
				  UrlString = @NewUrlString
			WHERE ServicesId = @Id
			  AND Name = @OldName
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
	@DESCRIPTION		[NVARCHAR](2048),
	@ImageData			[varbinary](max),
	@ImageMimeType		[varchar](50),
	@UrlString			[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[Solutions]
			(NAME, DESCRIPTION, ImageData, ImageMimeType, UrlString)
		VALUES
			(@NAME, @DESCRIPTION, @ImageData, @ImageMimeType, @UrlString)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_service'
GO
CREATE PROCEDURE [dbo].[sp_create_service]
(
	@NAME				[NVARCHAR](100),
	@DESCRIPTION		[NVARCHAR](2048),
	@ImageData			[varbinary](max),
	@ImageMimeType		[varchar](50),
	@UrlString			[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[Services]
			(NAME, DESCRIPTION, ImageData, ImageMimeType, UrlString)
		VALUES
			(@NAME, @DESCRIPTION, @ImageData, @ImageMimeType, @UrlString)
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
    @MessageBody nvarchar(2048),
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
    @MessageBody nvarchar(2048),
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

--Portfolio Stored Procedures
print '' print  ' *** creating procedure sp_retrieve_portfolio'
GO
Create PROCEDURE sp_retrieve_portfolio
AS
BEGIN
SELECT Id, Title, ResearchArea, Author, Publisher, RedirectLink, PortfolioType, Summary, Thumbnail, ImageMimeType
FROM Portfolio
END
GO

print '' print  ' *** creating procedure sp_retrieve_portfolio_by_id'
GO
Create PROCEDURE sp_retrieve_portfolio_by_id
	@Id int
AS
BEGIN
SELECT Id, Title, ResearchArea, Author, Publisher, RedirectLink, PortfolioType, Summary, Thumbnail, ImageMimeType
FROM Portfolio
WHERE  ([Id] = @Id)
END
GO

print '' print   ' *** Creating sp_portfolio_images_by_id'
GO
CREATE PROCEDURE [dbo].[sp_portfolio_image_ids_by_portfolioId] 
    @PortfolioId int
AS 
	BEGIN

	SELECT [Id] 
	FROM   [dbo].[PortfolioImages] 
	WHERE  ([PortfolioId] = @PortfolioId)
	END
GO

print '' print   ' *** Creating sp_portfolio_image_by_id'
GO
CREATE PROCEDURE [dbo].[sp_portfolio_image_by_id] 
    @Id int
AS 
	BEGIN

	SELECT [Id], [PortfolioId], [PortfolioImage], [ImageMimeType] 
	FROM   [dbo].[PortfolioImages] 
	WHERE  ([Id] = @Id)
	END
GO