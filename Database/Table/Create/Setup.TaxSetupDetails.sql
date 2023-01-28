CREATE SCHEMA Setup


CREATE TABLE Setup.TaxSetupDetails(
	RowId					INT				IDENTITY(1,1),
	VechicleCategory		INT				NULL,
	FiscalYear				VARCHAR(150)	NULL,
	Province				VARCHAR(150)	NULL,
	CCFrom					VARCHAR(100)	NULL,
	CCTo					VARCHAR(100)	NULL,
	TaxRate					VARCHAR(100)	NULL,
	Status					VARCHAR(10)		NULL,
	CreatedBy				VARCHAR(150)	NULL,
	CreatedDate				DATETIME		NULL,
	ModifiedBy				VARCHAR(150)	NULL,
	ModifiedDate			DATETIME		NULL
)