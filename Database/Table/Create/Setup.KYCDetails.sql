CREATE TABLE Setup.KYCDetails(
	RowId					INT					IDENTITY(1,1),
	KYCCode					NVARCHAR(150),
	FirstName				NVARCHAR(150)		NULL,
	MiddleName				NVARCHAR(150)		NULL,
	LastName				NVARCHAR(150)		NULL,
	DateOfBirth				NVARCHAR(150)		NULL,
	CurrentAddress			NVARCHAR(150)		NULL,
	ParmanentAddress		NVARCHAR(150)		NULL,
	ContactNumber			NVARCHAR(150)		NULL,
	Gender					VARCHAR(20)			NULL,
	Email					NVARCHAR(250)		NULL,
	Status					VARCHAR(10)			NULL,
	VerifiedBy				NVARCHAR(150)		NULL,
	VerifiedDate			DATETIME			NULL,
	VerifiedRemarks			NVARCHAR(150)		NULL,
	ApprovedBy				NVARCHAR(150)		NULL,
	ApprovedDate			DATETIME			NULL,
	ApprovedRemarks			NVARCHAR(150)		NULL,
	RejectedBy				NVARCHAR(150)		NULL,
	RejectedDate			DATETIME			NULL,
	RejectedRemarks			NVARCHAR(150)		NULL
)

