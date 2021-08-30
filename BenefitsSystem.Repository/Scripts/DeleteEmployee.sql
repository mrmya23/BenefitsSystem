CREATE procedure DeleteEmployee @Id int
AS
BEGIN TRY
	BEGIN TRANSACTION
	DELETE from Dependants WHERE EmployeeID = @Id;
	DELETE from Employees WHERE Id = @Id;
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
    -- if error, roll back any chanegs done by any of the sql statements
    ROLLBACK TRANSACTION
	THROW

END CATCH

GO