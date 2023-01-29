


CREATE TABLE Setup.PremiumSetupDetails(
	RowId					INT				IDENTITY(1,1),
	VechicleCategory		INT				NULL,
	FiscalYear				VARCHAR(150)	NULL,
	Province				VARCHAR(150)	NULL,
	InsuranceCompany		VARCHAR(500)	NULL,
	InsuranceRate			VARCHAR(100)	NULL,
	Status					VARCHAR(10)		NULL,
	CreatedBy				VARCHAR(150)	NULL,
	CreatedDate				DATETIME		NULL,
	ModifiedBy				VARCHAR(150)	NULL,
	ModifiedDate			DATETIME		NULL
)


