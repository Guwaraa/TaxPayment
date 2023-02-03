CREATE TABLE Setup.TaxPayment
(
		RowId				INT NOT NULL IDENTITY(1, 1),
		KYCCode				VARCHAR (30) NULL,
		Province			VARCHAR (150) NULL,
		VechicleCategory	VARCHAR (150) NULL,
		VechicleNo			VARCHAR (30) NULL,
		TaxRate				VARCHAR (150) NULL,
		PaidDate			VARCHAR (150) NULL,
		LastDueDate			DATETIME NULL,
		LateFeeAmount		MONEY	 NULL,
		CreatedBy			VARCHAR (150) NULL,
		CreatedDate			DATETIME NULL,
		ModifiedBy			VARCHAR (150) NULL,
		ModifiedDate		DATETIME NULL,
		VerifiedBy			VARCHAR (150) NULL,
		VerifiedDate		DATETIME NULL,
		VerifiedRemarks		VARCHAR (max) NULL,
		ApprovedBy			VARCHAR (150) NULL,
		ApprovedDate		DATETIME NULL,
		ApprovedRemarks		VARCHAR (max) NULL,
) 

