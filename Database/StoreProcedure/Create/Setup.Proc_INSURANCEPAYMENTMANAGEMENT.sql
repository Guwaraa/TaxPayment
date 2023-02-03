-- =============================================
-- Created By:	Sushant Manandhar
-- =============================================
  Create OR ALTER PROCEDURE Setup.Proc_INSURANCEPAYMENTMANAGEMENT
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
		@KYCCode					VARCHAR(250)			= NULL,
		@UserId						VARCHAR(250)			= NULL,
		@Province					VARCHAR(250)			= NULL,
		@VechicleCategory			VARCHAR(250)			= NULL,
		@CompanyName				VARCHAR(250)			= NULL,
		@InsuranceRate				VARCHAR(250)			= NULL,
		@PaidDate					VARCHAR(250)			= NULL,
		@VechicleNo					VARCHAR(250)			= NULL,
		@BankCode					VARCHAR(250)			= NULL

)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetRequiredDetail'
	BEGIN
	SELECT @KYCCode=k.KYCCode FROM Setup.KYCDetails AS k WHERE k.UserId=@UserId
	    SELECT  vc.Name,vc.Description  FROM Setting.VehicleCategory AS vc

       SELECT  cn.Name,cn.NameLocal Description FROM Setting.CompanyName AS cn

	   SELECT p.Name,p.NameLocal Description FROM Setting.Province AS p

	   SELECT bbd.VechicleNumber Name,bbd.VechicleNumber Description  FROM Setup.BlueBookDocument AS bbd WHERE bbd.KYCCode=@KYCCode
	END
	ELSE IF @Flag='AddInsurancePayemnt'
	BEGIN
		SELECT @KYCCode=k.KYCCode FROM Setup.KYCDetails AS k WHERE k.UserId=@UserId

	    INSERT INTO Setup.InsurancePayment
	    (
	        KYCCode,
	        Province,
	        VechicleCategory,
	        CompanyName,
	        InsuranceRate,
	        PaidDate,
	        VechicleNo,
	        BankCode
	    )
	    VALUES
	    (   @KYCCode,
	        @Province,
	        @VechicleCategory,
	        @CompanyName,
	        @InsuranceRate,
	        GETDATE(),
	        @VechicleNo,
	        @BankCode
	     )
	END
	ELSE IF @Flag='GetRequiredDetailList'
	BEGIN
		SELECT @KYCCode=k.KYCCode FROM Setup.KYCDetails AS k WHERE k.UserId=@UserId

	    SELECT ip.KYCCode,
               ip.Province,
               ip.VechicleCategory,
               ip.CompanyName,
               ip.InsuranceRate,
               ip.PaidDate
               FROM Setup.InsurancePayment AS ip 
			   WHERE ip.KYCCode=@KYCCode
	END
		ELSE IF @Flag= 'VerifyInsuranceDetail'
		BEGIN
			BEGIN TRANSACTION 
		     UPDATE Setup.InsurancePayment
			 SET 
			 VerifiedBy = @VerifiedBy,
			 VerifiedDate=GETDATE(),
			 VerifiedRemarks=@VerifiedRemarks
			 WHERE KYCCode=@KYCCode
			 IF @@ERROR=0
			 BEGIN
			     COMMIT TRANSACTION 
				 SELECT '000' Code, ' Verifed Successfully' Message
				 RETURN
			 END
		END
			ELSE IF @Flag= 'ApproveInsuranceDetail'
			BEGIN
				BEGIN TRANSACTION 
			     UPDATE Setup.InsurancePayment
				 SET 
				 ApprovedBy = @ApprovedBy,
				 ApprovedDate=GETDATE(),
				 ApprovedRemarks=@ApprovedRemarks
				 WHERE KYCCode=@KYCCode
				 IF @@ERROR=0
				 BEGIN
				     COMMIT TRANSACTION 
					 SELECT '000' Code, ' Approved Successfully' Message
					 RETURN
				 END
			END
			ELSE IF @Flag= 'RejectInsuranceDetail'
			BEGIN
				BEGIN TRANSACTION 
				SELECT @RejectedMessageRemarks=RejectedRemarks FROM Setup.InsurancePayment  WHERE KYCCode=@KYCCode

	
			     UPDATE Setup.InsurancePayment
				 SET 
				 Status				=		 'R',
				 RejectedBy			=		 @RejectedBy,
				 RejectedDate		=		 GETDATE(),
				 RejectedRemarks	=		 CASE WHEN @RejectedMessageRemarks IS NULL THEN @RejectedRemarks ELSE @RejectedMessageRemarks+' , '+@RejectedRemarks END,
				 VerifiedBy			=		 NULL,
				 VerifiedDate		=		 NULL,
				 VerifiedRemarks	=		 NULL
				 WHERE KYCCode=@KYCCode
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
