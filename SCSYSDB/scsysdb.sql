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


