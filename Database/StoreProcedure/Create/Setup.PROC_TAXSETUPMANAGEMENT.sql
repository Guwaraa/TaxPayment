-- =============================================
-- Created By:	Sushant Manandhar
-- Create date: 2022/09/22
-- Description:	Tax Setup Management
-- =============================================
  CREATE OR ALTER  PROCEDURE Setup.PROC_TAXSETUPMANAGEMENT
	(
		@Flag						NVARCHAR(100),
		@RowId						VARCHAR(100)			= NULL,
		@FromDate					VARCHAR(10)				= NULL,
		@ToDate						VARCHAR(10)				= NULL,
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
		@ModifiedBy					VARCHAR(250)			= NULL,
		@VechicleCategory			VARCHAR(250)			= NULL,
		@FiscalYear 				VARCHAR(250)			= NULL,
		@Province 					VARCHAR(250)			= NULL,
		@TaxSetupUploadJson 		VARCHAR(MAX)			= NULL,
		@TaxCount					INT						= NULL
		
)
AS 
SET NOCOUNT ON
BEGIN TRY
		DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;
	IF @Flag='GetRequiredDetailList'
	BEGIN
		SELECT tsd.TaxCode,
               tsd.VechicleCategory,
               tsd.FiscalYear,
               tsd.Province,
               tsd.Status
               FROM Setup.TaxSetupDetails AS tsd
			   GROUP BY tsd.TaxCode,tsd.VechicleCategory, tsd.FiscalYear,tsd.Province,tsd.Status
	END
	ELSE IF @Flag='AddTaxSetupDetails'
	BEGIN
	    BEGIN TRY
			BEGIN TRANSACTION TaxSetup
			SELECT 
				temp.CCFrom,
			    temp.CCTo,
			    temp.TaxRate
				INTO #TempTaxSetup
				FROM OPENJSON(@TaxSetupUploadJson) 
				WITH(
					CCFrom			VARCHAR(200)		'$.CCFrom',
					CCTo 			VARCHAR(200)		'$.CCTo',
					TaxRate 		VARCHAR(200)		'$.TaxRate'
				)temp
				SELECT @TaxCount=COUNT(*) FROM Setup.TaxSetupDetails AS tsd
				SET @TaxCount = @TaxCount +1 

				INSERT INTO Setup.TaxSetupDetails
				(
				    TaxCode,
				    VechicleCategory,
				    FiscalYear,
				    Province,
				    CCFrom,
				    CCTo,
				    TaxRate,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				SELECT
					'T'+CONVERT(VARCHAR(200),@TaxCount), 
				    @VechicleCategory, 
				    @FiscalYear, 
				    @Province, 
				    temp.CCFrom,
					temp.CCTo,
					temp.TaxRate,
					'A', 
				    @CreatedBy, 
				    GETDATE() 
					FROM #TempTaxSetup temp
					SELECT '000' Code,'Tax Setup Added Sucessfully' Message
	    		 COMMIT TRANSACTION TaxSetup
				 DROP TABLE #TempTaxSetup
	    	   END TRY
	    	   BEGIN CATCH
	    		ROLLBACK TRANSACTION TaxSetup
	    		SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    	   END CATCH
	END
	ELSE IF @Flag='UpdateTaxSetupDetails'
	BEGIN
	    BEGIN TRY
			BEGIN TRANSACTION TaxSetup
			SELECT 
				temp.CCFrom,
			    temp.CCTo,
			    temp.TaxRate
				INTO #TempUpdateTaxSetup
				FROM OPENJSON(@TaxSetupUploadJson) 
				WITH(
					CCFrom			VARCHAR(200)		'$.CCFrom',
					CCTo 			VARCHAR(200)		'$.CCTo',
					TaxRate 		VARCHAR(200)		'$.TaxRate'
				)temp
				
				DELETE FROM Setup.TaxSetupDetails WHERE TaxCode=@TaxCount

				INSERT INTO Setup.TaxSetupDetails
				(
				    TaxCode,
				    VechicleCategory,
				    FiscalYear,
				    Province,
				    CCFrom,
				    CCTo,
				    TaxRate,
				    Status,
				    CreatedBy,
				    CreatedDate
				)
				SELECT
					'T'+CONVERT(VARCHAR(200),@TaxCount), 
				    @VechicleCategory, 
				    @FiscalYear, 
				    @Province, 
				    temp.CCFrom,
					temp.CCTo,
					temp.TaxRate,
					'A', 
				    @CreatedBy, 
				    GETDATE() 
					FROM #TempUpdateTaxSetup temp
					SELECT '000' Code,'Tax Setup Updated Sucessfully' Message
	    		 COMMIT TRANSACTION TaxSetup
				 DROP TABLE #TempUpdateTaxSetup
	    	   END TRY
	    	   BEGIN CATCH
	    		ROLLBACK TRANSACTION TaxSetup
	    		SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
	    	   END CATCH
	END
	
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Message, '' Id
END CATCH
GO
