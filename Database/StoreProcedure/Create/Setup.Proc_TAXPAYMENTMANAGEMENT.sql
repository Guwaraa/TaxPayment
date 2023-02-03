-- =============================================
-- Created By:	Rohit Manandhar
-- =============================================
  ALTER  PROCEDURE Setup.Proc_TAXPAYMENTMANAGEMENT
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
		@ModifiedDate 				VARCHAR(MAX)			= NULL,
		@Province					VARCHAR(MAX)			= NULL,
		@VechicleCategory			VARCHAR(MAX)			= NULL,
		@VechicleNo				VARCHAR(MAX)			= NULL,
		@TaxRate				VARCHAR(MAX)			= NULL,
		@PaidDate				VARCHAR(MAX)			= NULL,
		@LastDueDate				VARCHAR(MAX)			= NULL,
		@LateFeeAmount				VARCHAR(MAX)			= NULL,
		@VerifiedDate				VARCHAR(MAX)			= NULL,
		@RowNum				VARCHAR(MAX)			= NULL,
		@Action						VARCHAR(250)			= NULL,
		@ApproveVerify						VARCHAR(250)			= NULL



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
	SELECT ROW_NUMBER() OVER (ORDER BY kd.RowId DESC) AS RowNum,COUNT(*) OVER() AS FilterCount, 
	kd.RowId
        KYCCode,
        Province,
        VechicleCategory,
        VechicleNo,
        TaxRate,
        PaidDate,
        LastDueDate,
        LateFeeAmount,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        VerifiedBy,
        VerifiedDate,
        VerifiedRemarks,
        ApprovedBy,
        ApprovedDate,
        ApprovedRemarks,
        Status,
        RejectedRemarks,
        RejectedBy,
        RejectedDate FROM Setup.TaxPayment kd
		)
			   SELECT * FROM CTE_Report WHERE RowNum > @FirstRec AND RowNum <= @LastRec;

	END
	ELSE IF @Flag='AddTaxPayments'
	BEGIN
	    BEGIN TRY
	    	 BEGIN TRANSACTION TaxPayments
			INSERT INTO Setup.TaxPayment
			(
			    KYCCode,
			    Province,
			    VechicleCategory,
			    VechicleNo,
			    TaxRate,
			    PaidDate,
			    LastDueDate,
			    LateFeeAmount,
			    CreatedBy,
			    CreatedDate,
			    ModifiedBy,
			    ModifiedDate,
			    VerifiedBy,
			    VerifiedDate,
			    VerifiedRemarks,
			    ApprovedBy,
			    ApprovedDate,
			    ApprovedRemarks,
			    Status,
			    RejectedRemarks,
			    RejectedBy
			)
			VALUES
			(
			@KYCCode ,
			    @Province ,
			    @VechicleCategory ,
			    @VechicleNo ,
			    @TaxRate ,
			    @PaidDate ,
			    @LastDueDate ,
			    @LateFeeAmount ,
			    @CreatedBy ,
			    GETDATE() ,
			    @ModifiedBy ,
			    @ModifiedDate ,
			    @VerifiedBy ,
			    @VerifiedDate ,
			    @VerifiedRemarks ,
			    @ApprovedBy ,
			    @ApprovedDate ,
			    @ApprovedRemarks ,
			    @Status ,
			    @RejectedRemarks ,
			    @RejectedBy 
			    )
				  SELECT '000' Code,'Tax Payment Detail Added Sucessfully' Message
	    	 COMMIT TRANSACTION KYCDetails
	   END TRY
	   BEGIN CATCH
	   ROLLBACK TRANSACTION KYCDetails
	   SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	   END CATCH
	END
	ELSE IF @Flag='GetRequiredDetails'
	BEGIN
	SELECT * FROM Setup.TaxPayment WHERE RowId=@RowId
	END
	ELSE IF @Flag= 'VerifyTaxPayment'
	BEGIN
		BEGIN TRANSACTION 
	     UPDATE Setup.TaxPayment
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
		ELSE IF @Flag= 'ApproveTaxPayment'
		BEGIN
			BEGIN TRANSACTION 
		     UPDATE Setup.TaxPayment
			 SET 
			 ApprovedBy = 'admin',
			 ApprovedDate=GETDATE(),
			 ApprovedRemarks=@ApprovedRemarks
			 WHERE  RowId=@RowId
			 IF @@ERROR=0
			 BEGIN
			     COMMIT TRANSACTION 
				 SELECT '000' Code, ' Approved Successfully' Message
				 RETURN
			 END
		END
		ELSE IF @Flag= 'RejectTaxPayment'
		BEGIN
			BEGIN TRANSACTION 
			SELECT @RejectedMessageRemarks=t.RejectedRemarks FROM Setup.TaxPayment AS t  WHERE  t.RowId=@RowId
	
		     UPDATE Setup.TaxPayment
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
