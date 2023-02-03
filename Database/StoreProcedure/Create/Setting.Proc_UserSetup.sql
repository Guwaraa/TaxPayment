-- =============================================
-- Created By:	Sushant Manandhar
-- =============================================
  Create  PROCEDURE Setting.Proc_UserSetup
	(
		@Flag						NVARCHAR(100),
		@RowId						VARCHAR(100)			= NULL,
		@FromDate					VARCHAR(10)				= NULL,
		@ToDate						VARCHAR(10)				= NULL,
		@DisplayLength				INT					= NULL,					
		@DisplayStart				INT					= NULL,
		@SortCol					INT					= NULL,	
		@SortDir					NVARCHAR(10)			= NULL,	
		@Search						NVARCHAR(100)			= NULL,
		@UserName					NVARCHAR(100)			= NULL,
		@Branch						VARCHAR(100)			= NULL,
		@BranchUnit					VARCHAR(100)			= NULL,
		@Status 					VARCHAR(1)				= NULL,
		@CreatedBy					VARCHAR(250)			= NULL,
		@CreatedDate				DATETIME2				= NULL,
		@VerifiedBy					VARCHAR(250)			= NULL,
		@ApprovedBy 				VARCHAR(250)			= NULL,
		@RejectedBy					VARCHAR(250)			= NULL,
		@ModifiedBy					VARCHAR(250)			= NULL,
		@VerifiedRemarks			VARCHAR(250)			= NULL,
		@ApprovedRemarks 			VARCHAR(250)			= NULL,
		@RejectedRemarks			VARCHAR(250)			= NULL,
		@ModifiedRemarks			VARCHAR(250)			= NULL,
		@RejectedMessageRemarks		VARCHAR(MAX)			= NULL,
		@RejectedDate				VARCHAR(250)			= NULL,
		@FullName					VARCHAR(250)			= NULL,
		@Email 						VARCHAR(250)			= NULL,
		@PhoneNumber 				VARCHAR(250)			= NULL,
		@Password 					VARCHAR(250)			= NULL
		
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='RegiterDetail'
	BEGIN
	    BEGIN TRY
	          BEGIN TRANSACTION UserSetup
					INSERT INTO Setting.UserSetup
					(
					    UserName,
					    FullName,
					    Email,
					    PhoneNumber,
					    Password
					)
					VALUES
					(   @UserName,
					    @FullName,
					    @Email,
					    @PhoneNumber,
					    @Password 
					  )

				SELECT '000' Code,'New User Registration Sucessfully' Message
	       COMMIT TRANSACTION UserSetup
	      END TRY
	      BEGIN CATCH
	    ROLLBACK TRANSACTION UserSetup
	    SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	      END CATCH
	END
	ELSE IF @Flag='CheckUserName'
	BEGIN
		IF EXISTS(SELECT 'a' FROM  Setting.UserSetup AS us WHERE us.Email=@Email AND  us.Password=@Password)    
		BEGIN
		    SELECT '000' Code,'Login Sucessfull'Message,us.RowId Data,us.UserName Extras FROM Setting.UserSetup AS us
			RETURN
		END
		ELSE
        BEGIN
		    SELECT '111' Code,'Login Failed'Message
            RETURN
        END
	END
	
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
