-- =============================================
-- Created By:	Rohit Manandhar
-- Create date: 2023/01/29
-- Description:	Premium Setup Management
-- =============================================
  ALTER PROCEDURE Setup.PROC_PREMIUMSETUPMANAGEMENT
	(
		@Flag						NVARCHAR(100),
		@RowId						VARCHAR(100)			= NULL,
		@FromDate					VARCHAR(10)				= NULL,
		@ToDate						VARCHAR(10)				= NULL,
		@VechicleCategory			VARCHAR(100)			= NULL,
		@FiscalYear					VARCHAR(100)			= NULL,
		@Province					VARCHAR(100)			= NULL,
		@InsuranceCompany			VARCHAR(100)			= NULL,
		@InsuranceRate				VARCHAR(100)			= NULL,
		@DisplayLength				INT						= NULL,					
		@DisplayStart				INT						= NULL,
		@SortCol					INT						= NULL,	
		@SortDir					NVARCHAR(10)			= NULL,	
		@Search						NVARCHAR(100)			= NULL,
		@UserName					NVARCHAR(100)			= NULL,
		@Branch						VARCHAR(100)			= NULL,
		@BranchUnit					VARCHAR(100)			= NULL,
		@Status 					VARCHAR(1)				= NULL,
		@CreatedBy					VARCHAR(250)			= NULL,
		@CreatedDate				DATETIME2				= NULL,
		@ModifiedDate				DATETIME2				= NULL,
		@VerifiedBy					VARCHAR(250)			= NULL,
		@VechicleCategoryList		VARCHAR(250)			= NULL,
		@FiscalYearList				VARCHAR(250)			= NULL,
		@ProvinceList				VARCHAR(250)			= NULL,
		@InsuranceCompanyList		VARCHAR(250)			= NULL,
		@ApprovedBy 				VARCHAR(250)			= NULL,
		@RejectedBy					VARCHAR(250)			= NULL,
		@ModifiedBy					VARCHAR(250)			= NULL,
		@RowNum						VARCHAR(250)			= NULL,
		@VerifiedRemarks			VARCHAR(250)			= NULL,
		@ApprovedRemarks 			VARCHAR(250)			= NULL,
		@RejectedRemarks			VARCHAR(250)			= NULL,
		@ModifiedRemarks			VARCHAR(250)			= NULL,
		@RejectedMessageRemarks		VARCHAR(MAX)			= NULL,
		@RejectedDate				VARCHAR(250)			= NULL
		
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
	IF @Flag='GetRequiredDetailList'
	BEGIN
	
	Select ROW_NUMBER() over (ORDER BY P.RowId DESC) AS RowNum,COUNT(*) OVER() AS FilterCount, 
				p.RowId,
               V.Name VechicleCategory,
               F.Name FiscalYear,
               Pr.Name Province,
               C.Name InsuranceCompany,
               P.InsuranceRate,
               P.Status
               FROM Setup.PremiumSetupDetails P
			   INNER JOIN Setting.VehicleCategory V ON V.RowId=P.VechicleCategory
			   INNER JOIN Setting.FiscalYear F ON F.RowId=P.FiscalYear
			   INNER JOIN Setting.Province Pr ON Pr.RowId=P.Province
			   INNER JOIN Setting.CompanyName C ON C.RowId=P.InsuranceCompany
	END
	ELSE IF @Flag='AddPremiumDetails'
	BEGIN
	INSERT INTO Setup.PremiumSetupDetails
	(
	    VechicleCategory,
	    FiscalYear,
	    Province,
	    InsuranceCompany,
	    InsuranceRate,
	    Status,
	    CreatedBy,
	    CreatedDate
	)
	VALUES
	(   @VechicleCategory, 
	    @FiscalYear, 
	    @Province, 
	    @InsuranceCompany, 
	    @InsuranceRate, 
	    @Status, 
	    @CreatedBy, 
	    GETDATE() 
	    )
		SELECT '000' Code ,'Premium Setup Added Successfully' Message
		RETURN
	END
	ELSE IF @Flag='GetRequiredList'
	BEGIN
		SELECT RowId Value,Name Description FROM Setting.VehicleCategory 
		SELECT RowId Value,Name Description FROM Setting.FiscalYear
		SELECT RowId Value,Name Description FROM Setting.Province
		SELECT RowId Value,Name Description FROM Setting.CompanyName 
		RETURN
		
	END
	ELSE IF @Flag='GetRequiredPremiumDetails'
	BEGIN
		SELECT RowId,
               VechicleCategory,
               FiscalYear,
               Province,
               InsuranceCompany,
               InsuranceRate,
               Status
               FROM Setup.PremiumSetupDetails	
	END
	ELSE IF @Flag='UpdatePremiumSetupDetails'
	BEGIN
	UPDATE Setup.PremiumSetupDetails SET
		VechicleCategory	=	@VechicleCategory,
		FiscalYear			=	@FiscalYear,
		Province			=	@Province,
		InsuranceCompany	=	@InsuranceCompany,
		InsuranceRate		=	@InsuranceRate,
		ModifiedBy			=	@ModifiedBy,
		ModifiedDate		=	GETDATE()
		WHERE RowId=@RowId
	END
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
