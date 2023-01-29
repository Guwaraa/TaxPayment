CREATE TABLE Setting.VehicleCategory(
	RowId			INT IDENTITY(1,1),
	Name			VARCHAR(150),
	Description		VARCHAR(150),
	Status			VARCHAR(10),
	CreatedBy		VARCHAR(150),
	CreatedDate		DATETIME
)