print '' print '*** Created sp_insert_contactmessages'
GO
CREATE PROCEDURE sp_insert_contactmessages
	@NAME 				nvarchar(256),
	@EMAIL 				nvarchar(256),
	@SUBJECT 			nvarchar(256),
	@MESSAGEBODY 		nvarchar(1024),
	@TIMEPOSTED 		date 
AS
	BEGIN
		INSERT INTO ContactMessages (Name,Email,Subject,MessageBody,TimePosted)
		VALUES(@NAME,@EMAIL,@SUBJECT,@MESSAGEBODY,@TIMEPOSTED)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_contactmessages'
GO
CREATE PROCEDURE [dbo].[sp_update_contactmessages]
	(
		@ID					int,
		
		@NEWNAME			nvarchar(256),
		@NEWEMAIL 			nvarchar(256),
		@NEWSUBJECT 		nvarchar(256),
		@NEWMESSAGEBODY 	nvarchar(1024),
		@TIMEPOSTED 		date,
		
		@OLDNAME			nvarchar(256),
		@OLDEMAIL 			nvarchar(256),
		@OLDSUBJECT 		nvarchar(256),
		@OLDMESSAGEBODY 	nvarchar(1024)
	)
AS
	BEGIN
		UPDATE ContactMessages
		  SET NAME = @NEWNAME,
			  EMAIL = @NEWEMAIL,
			  SUBJECT = @NEWSUBJECT,
			  MESSAGEBODY = @NEWMESSAGEBODY,
			  TIMEPOSTED = @TIMEPOSTED
		WHERE Id = @Id
		  AND NAME = @OLDNAME
		  AND EMAIL = @OLDEMAIL
		  AND SUBJECT = @OLDSUBJECT
		  AND MESSAGEBODY = @OLDMESSAGEBODY
		RETURN @@ROWCOUNT
	END
GO

print '' print  ' *** creating procedure sp_retrieve_contactmessages'
GO
Create PROCEDURE sp_retrieve_contactmessages
AS
	BEGIN
		SELECT Name, Email, Subject, MessageBody, TimePosted
		FROM ContactMessages
	END
GO

print '' print  ' *** creating procedure sp_retrieve_contactmessage_by_id'
GO
Create PROCEDURE sp_retrieve_contactmessage_by_id
(
	@ID			INT
)
AS
	BEGIN
		SELECT Name, Email, Subject, MessageBody, TimePosted
		FROM ContactMessages
		WHERE Id = @ID
	END
GO

print '' print  ' *** creating procedure sp_delete_contactmessage'
GO
Create PROCEDURE sp_delete_contactmessages
(
	@ID			INT
)
AS
	BEGIN
		DELETE FROM ContactMessages
		WHERE ID = @ID
		RETURN @@ROWCOUNT
	END
GO