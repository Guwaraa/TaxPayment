-- =============================================
-- Created By:	Sushant Manandhar

-- =============================================
  CREATE OR ALTER  PROCEDURE Setting.Proc_RegistrationManagement
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
		@VechicleType 				VARCHAR(250)			= NULL,
		@VechicleNumber 			VARCHAR(250)			= NULL,
		@RegisteredDate 			VARCHAR(250)			= NULL,
		@Ownername 					VARCHAR(250)			= NULL,
		@CompanyName 				VARCHAR(250)			= NULL,
		@VechicleModel 				VARCHAR(250)			= NULL,
		@DateOfModification 		VARCHAR(250)			= NULL,
		@VechiclePower 				VARCHAR(250)			= NULL,
		@Color 						VARCHAR(250)			= NULL,
		@UserId 					INT						= NULL,
		@EngineNumber 				VARCHAR(250)			= NULL,
		@LastTaxPaidDateFrom 		VARCHAR(250)			= NULL,
		@LastTaxPaidDateTo 			VARCHAR(250)			= NULL,
		@FrontPageImagePath 		VARCHAR(MAX)			= NULL,
		@VechicleInfoImagePath 		VARCHAR(MAX)			= NULL,
		@TaxPaidImagePath 			VARCHAR(MAX)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetGridDetailList'
	BEGIN
	    SELECT 
			ROW_NUMBER() OVER (ORDER BY kd.KYCCode DESC) AS RowNum,
			kd.KYCCode,
			Kd.UserId,
			kd.VechicleType,
			kd.VechicleNumber,
			kd.RegisteredDate,
			kd.Ownername
		FROM Setup.BlueBookDocument AS kd 
		--WHERE kd.UserId=@UserId
	END
	ELSE IF @Flag='AddVechicleRegistration'
	BEGIN
	    BEGIN TRY
	    	  BEGIN TRANSACTION Registration
				INSERT INTO Setup.BlueBookDocument
				(
				    KYCCode,
				    VechicleType,
				    VechicleNumber,
				    RegisteredDate,
				    Ownername,
				    FrontPageImage,
				    CompanyName,
				    VechicleModel,
				    DateOfModification,
				    VechiclePower,
				    Color,
				    EngineNumber,
				    VechicleInfoImage,
				    LastTaxPaidDateFrom,
				    LastTaxPaidDateTo,
				    TaxPaidImage
				)
				VALUES
				(   @KYCCode,
				    @VechicleType,
				    @VechicleNumber,
				    @RegisteredDate,
				    @Ownername,
				    @FrontPageImagePath,
				    @CompanyName,
				    @VechicleModel,
				    @DateOfModification,
				    @VechiclePower,
				    @Color,
				    @EngineNumber,
				    @VechicleInfoImagePath,
				    @LastTaxPaidDateFrom,
				    @LastTaxPaidDateTo,
				    @TaxPaidImagePath 
				 )
				 SELECT '000' Code,'Vechicle Registred Sucessfully' Message
	    	 COMMIT TRANSACTION Registration
	    	END TRY
	    	BEGIN CATCH
	    	ROLLBACK TRANSACTION Registration
	    	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    	END CATCH
	END
	ELSE IF @Flag='GetRegisteredDetails'
	BEGIN
	    SELECT bbd.RowId,
               bbd.KYCCode,
               bbd.VechicleType,
               bbd.VechicleNumber,
               bbd.RegisteredDate,
               bbd.Ownername,
               bbd.FrontPageImage FrontPageImagePath,
               bbd.CompanyName,
               bbd.VechicleModel,
               bbd.DateOfModification,
               bbd.VechiclePower,
               bbd.Color,
               bbd.EngineNumber,
               bbd.VechicleInfoImage VechicleInfoImagePath,
               bbd.LastTaxPaidDateFrom,
               bbd.LastTaxPaidDateTo,
               bbd.TaxPaidImage TaxPaidImagePath,
               bbd.UserId FROM Setup.BlueBookDocument AS bbd WHERE bbd.UserId=@UserId
	END
	END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
