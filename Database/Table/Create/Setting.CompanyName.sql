CREATE TABLE Setting.CompanyName(
	RowId			INT			IDENTITY(1,1),
	Name			VARCHAR(150),
	NameLocal		NVARCHAR(150),
	ContactNo		NVARCHAR(150),
	Location		NVARCHAR(150),
	Description		NVARCHAR(150),
	CreatedBy		VARCHAR(150),
	CreatedDate		DATETIME
)

DROP TABLE Setting.CompanyName