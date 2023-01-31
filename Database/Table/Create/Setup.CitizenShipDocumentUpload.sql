CREATE TABLE Setup.CitizenShipDocumentUpload(
	RowId					INT				IDENTITY(1,1),
	KYCCode					VARCHAR(200)	NULL,
	CitizenShipNumber		VARCHAR(200)	NULL,
	IssueDate				DATETIME        NULL,
	IssuePlace				VARCHAR(200)	NULL,
	FrontImage				VARCHAR(max)	NULL,
	BackImage				VARCHAR(max)	NULL,
	CreatedBy				VARCHAR(max)	NULL,
	CreatedDate				DATETIME		NULL
)