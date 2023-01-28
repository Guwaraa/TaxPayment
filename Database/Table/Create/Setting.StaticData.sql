CREATE SCHEMA Setting

CREATE TABLE Setting.VehicleCategory(
	RowId			INT IDENTITY(1,1),
	Name			VARCHAR(150),
	Description		VARCHAR(150),
	Status			VARCHAR(10),
	CreatedBy		VARCHAR(150),
	CreatedDate		DATETIME
)

CREATE TABLE Setting.Province(
	RowId			INT		IDENTITY(1,1),
	Name			VARCHAR(150),
	NameLocal		NVARCHAR(150),
	CreatedBy		VARCHAR(150),
	CreatedDate		DATETIME
)


CREATE TABLE Setting.FiscalYear(
	RowId			INT			IDENTITY(1,1),
	Name			VARCHAR(150),
	NameLocal		NVARCHAR(150),
	CreatedBy		VARCHAR(150),
	CreatedDate		DATETIME
)