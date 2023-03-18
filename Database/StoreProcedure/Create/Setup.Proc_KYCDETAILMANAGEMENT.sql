-- =============================================
-- Created By:	Sushant Manandhar
-- =============================================
  ALTER  PROCEDURE Setup.Proc_KYCDETAILMANAGEMENT
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
		@UserId						VARCHAR(250)			= NULL,
		@KYCCode 					VARCHAR(250)			= NULL,
		@FirstName 					VARCHAR(250)			= NULL,
		@MiddleName 				VARCHAR(250)			= NULL,
		@LastName 					VARCHAR(250)			= NULL,
		@DateOfBirth 				DATETIME				= NULL,
		@CurrentAddress 			VARCHAR(250)			= NULL,
		@ParmanentAddress 			VARCHAR(250)			= NULL,
		@ContactNumber 				VARCHAR(250)			= NULL,
		@CitizenshipNumber 			VARCHAR(250)			= NULL,
		@Gender 					VARCHAR(250)			= NULL,
		@Email	 					VARCHAR(250)			= NULL,
		@FrontImagePath 			VARCHAR(MAX)			= NULL,
		@BackImagePath 				VARCHAR(MAX)			= NULL,
		@Remarks	 				VARCHAR(MAX)			= NULL,
		@FilterCount 				VARCHAR(MAX)			= NULL,
		@ApprovedDate 				VARCHAR(MAX)			= NULL,
		@ModifiedDate 				VARCHAR(MAX)			= NULL




)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetGridDetailList'
	BEGIN
	
	SET @FirstRec = @DisplayStart;
	SET @LastRec  = @DisplayStart + @DisplayLength;
	;WITH CTE_Report AS
	(
	Select ROW_NUMBER() over (ORDER BY kd.RowId DESC) AS RowNum,COUNT(*) OVER() AS FilterCount, 
			kd.RowId,
               kd.FirstName+' '+kd.MiddleName+' '+ kd.LastName FullName,
               kd.DateOfBirth,
               kd.CurrentAddress,
               kd.ContactNumber,
               kd.Email,
               kd.Status,
               kd.VerifiedBy,
               kd.ApprovedBy,
               kd.RejectedBy
			   FROM Setup.KYCDetails AS kd
			   )
			   SELECT * FROM CTE_Report WHERE RowNum > @FirstRec AND RowNum <= @LastRec;

	END
	ELSE IF @Flag='AddKYCDetails'
	BEGIN
	    BEGIN TRY
	    	 BEGIN TRANSACTION KYCDetails
				INSERT INTO Setup.KYCDetails
				(
				    KYCCode,
				    FirstName,
				    MiddleName,
				    LastName,
				    DateOfBirth,
				    CurrentAddress,
				    ParmanentAddress,
				    ContactNumber,
				    Gender,
				    Email,
				    Status
				)
				VALUES
				(   NULL,
				    @FirstName,
				    @MiddleName,
				    @LastName,
				    @DateOfBirth,
				    @CurrentAddress,
				    @ParmanentAddress,
				    @ContactNumber,
				    @Gender,
				    @Email,
					'A'
				  )
				  SELECT '000' Code,'KYC Detail Added Sucessfully' Message
	    	 COMMIT TRANSACTION KYCDetails
	   END TRY
	   BEGIN CATCH
	   ROLLBACK TRANSACTION KYCDetails
	   SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	   END CATCH
	END
	ELSE IF @Flag='GetRequiredKycDetails'
	BEGIN
	SELECT RowId,
          KYCCode,
          FirstName,
          MiddleName,
          LastName,
          DateOfBirth,
          CurrentAddress,
          ParmanentAddress,
          ContactNumber,
          Gender,
          Email,
          Status,
          VerifiedBy,
          VerifiedDate,
          VerifiedRemarks,
          ApprovedBy,
          ApprovedDate,
          ApprovedRemarks,
          RejectedBy,
          RejectedDate,
          RejectedRemarks FROM Setup.KYCDetails WHERE RowId=@RowId
	END
	ELSE IF @Flag= 'VerifyKYCDetail'
	BEGIN
		BEGIN TRANSACTION 
	     UPDATE Setup.KYCDetails
		 SET 
		 Status='V',
		 VerifiedBy = 'admin',
		 VerifiedDate=GETDATE(),
		 VerifiedRemarks=@VerifiedRemarks
		 WHERE RowId=@RowId
		 IF @@ERROR=0
		 BEGIN
		     COMMIT TRANSACTION 
			 SELECT '000' Code, ' Verifed Successfully' Message
			 RETURN
		 END
	END
		
		ELSE IF @Flag= 'ApproveKYCDetail'
		BEGIN
			BEGIN TRANSACTION  

			SELECT @KYCCode=MAX(ISNULL(KYCCode,0))+1 FROM Setup.KYCDetails WHERE KYCCode=@KYCCode 
			UPDATE Setup.KYCDetails
			 SET 
			 ApprovedBy = 'admin',
			 ApprovedDate=GETDATE(),
			 ApprovedRemarks=@ApprovedRemarks,
			 KYCCode=@KYCCode
			 WHERE  RowId=@RowId
			 IF @@ERROR=0
			 BEGIN
			     COMMIT TRANSACTION 
				 SELECT '000' Code, ' Approved Successfully' Message
				 RETURN
			 END
		END
		ELSE IF @Flag= 'RejectKYCDetail'
		BEGIN
			BEGIN TRANSACTION 
			SELECT @RejectedMessageRemarks=t.RejectedRemarks FROM Setup.KYCDetails AS t  WHERE  UserId=@UserId
	
		     UPDATE Setup.KYCDetails
			 SET 
			 Status				=		 'R',
			 RejectedBy			=		 'admin',
			 RejectedDate		=		 GETDATE(),
			 RejectedRemarks	=		 CASE WHEN @RejectedMessageRemarks IS NULL THEN @RejectedRemarks ELSE @RejectedMessageRemarks+' , '+@RejectedRemarks END
			 WHERE  RowId=@RowId
			 IF @@ERROR=0
			 BEGIN
			     COMMIT TRANSACTION 
				 SELECT '000' Code, ' Rejected Successfully' Message
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
