-- Stored Procedures for Table: TestAppointments

Use [DVLD];
Go


CREATE OR ALTER PROCEDURE SP_Get_TestAppointments_ByID
(
    @TestAppointmentID int
)
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve data
        SELECT *
        FROM TestAppointments
        WHERE TestAppointmentID = @TestAppointmentID;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Get_All_TestAppointments
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve all data from the table
        SELECT *
        FROM TestAppointments;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Add_TestAppointments
(
    @TestTypeID int,
    @LocalDrivingLicenseApplicationID int,
    @AppointmentDate smalldatetime,
    @PaidFees smallmoney,
    @CreatedByUserID int,
    @IsLocked bit,
    @RetakeTestApplicationID int = NULL,
    @NewID INT OUTPUT

)
AS
BEGIN
    BEGIN TRY
        -- Check if any required parameters are NULL
        IF LTRIM(RTRIM(@TestTypeID)) IS NULL OR LTRIM(RTRIM(@LocalDrivingLicenseApplicationID)) IS NULL OR LTRIM(RTRIM(@AppointmentDate)) IS NULL OR LTRIM(RTRIM(@PaidFees)) IS NULL OR LTRIM(RTRIM(@CreatedByUserID)) IS NULL OR LTRIM(RTRIM(@IsLocked)) IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Insert the data into the table
        INSERT INTO TestAppointments ([TestTypeID],[LocalDrivingLicenseApplicationID],[AppointmentDate],[PaidFees],[CreatedByUserID],[IsLocked],[RetakeTestApplicationID])
        VALUES (    LTRIM(RTRIM(@TestTypeID)),
    LTRIM(RTRIM(@LocalDrivingLicenseApplicationID)),
    LTRIM(RTRIM(@AppointmentDate)),
    LTRIM(RTRIM(@PaidFees)),
    LTRIM(RTRIM(@CreatedByUserID)),
    LTRIM(RTRIM(@IsLocked)),
    LTRIM(RTRIM(@RetakeTestApplicationID))
);

        -- Set the new ID
        SET @NewID = SCOPE_IDENTITY();  -- Get the last inserted ID
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Error handling
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Update_TestAppointments_ByID
(
    @TestAppointmentID int,
    @TestTypeID int,
    @LocalDrivingLicenseApplicationID int,
    @AppointmentDate smalldatetime,
    @PaidFees smallmoney,
    @CreatedByUserID int,
    @IsLocked bit,
    @RetakeTestApplicationID int = NULL

)
AS
BEGIN
    BEGIN TRY
        -- Check if required parameters are NULL or contain only whitespace after trimming
        IF LTRIM(RTRIM(@TestTypeID)) IS NULL OR LTRIM(RTRIM(@TestTypeID)) = '' OR LTRIM(RTRIM(@LocalDrivingLicenseApplicationID)) IS NULL OR LTRIM(RTRIM(@LocalDrivingLicenseApplicationID)) = '' OR LTRIM(RTRIM(@AppointmentDate)) IS NULL OR LTRIM(RTRIM(@AppointmentDate)) = '' OR LTRIM(RTRIM(@PaidFees)) IS NULL OR LTRIM(RTRIM(@PaidFees)) = '' OR LTRIM(RTRIM(@CreatedByUserID)) IS NULL OR LTRIM(RTRIM(@CreatedByUserID)) = '' OR LTRIM(RTRIM(@IsLocked)) IS NULL OR LTRIM(RTRIM(@IsLocked)) = ''
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Update the record in the table
        UPDATE TestAppointments
        SET     [TestTypeID] = LTRIM(RTRIM(@TestTypeID)),
    [LocalDrivingLicenseApplicationID] = LTRIM(RTRIM(@LocalDrivingLicenseApplicationID)),
    [AppointmentDate] = LTRIM(RTRIM(@AppointmentDate)),
    [PaidFees] = LTRIM(RTRIM(@PaidFees)),
    [CreatedByUserID] = LTRIM(RTRIM(@CreatedByUserID)),
    [IsLocked] = LTRIM(RTRIM(@IsLocked)),
    [RetakeTestApplicationID] = LTRIM(RTRIM(@RetakeTestApplicationID))

        WHERE TestAppointmentID = @TestAppointmentID;
        
        -- Optionally, you can check if the update was successful and raise an error if no rows were updated
        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('No rows were updated. Please check the PersonID or other parameters.', 16, 1);
            RETURN;
        END
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Handle errors
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Delete_TestAppointments_ByID
(
    @TestAppointmentID int
)
AS
BEGIN

    BEGIN TRY
        -- Check if the record exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID)
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END

        -- Attempt to delete the record
        DELETE FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID;

        -- Ensure at least one row was deleted
        IF @@ROWCOUNT = 0
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END
    END TRY
    BEGIN CATCH
        -- Handle all errors (including FK constraint violations)
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Search_TestAppointments_ByColumn
(
    @ColumnName NVARCHAR(128),  -- Column name without spaces
    @SearchValue NVARCHAR(255), -- Value to search for
    @Mode NVARCHAR(20) = 'Anywhere' -- Search mode (default: Anywhere)
)
AS
BEGIN
    BEGIN TRY
        DECLARE @ActualColumn NVARCHAR(128);
        DECLARE @SQL NVARCHAR(MAX);
        DECLARE @SearchPattern NVARCHAR(255);

        -- Map input column name to actual database column name
        SET @ActualColumn = 
            CASE @ColumnName
                WHEN 'TestAppointmentID' THEN 'TestAppointmentID'
        WHEN 'TestTypeID' THEN 'TestTypeID'
        WHEN 'LocalDrivingLicenseApplicationID' THEN 'LocalDrivingLicenseApplicationID'
        WHEN 'AppointmentDate' THEN 'AppointmentDate'
        WHEN 'PaidFees' THEN 'PaidFees'
        WHEN 'CreatedByUserID' THEN 'CreatedByUserID'
        WHEN 'IsLocked' THEN 'IsLocked'
        WHEN 'RetakeTestApplicationID' THEN 'RetakeTestApplicationID'
                ELSE NULL
            END;

        -- Validate the column name
        IF @ActualColumn IS NULL
        BEGIN
            RAISERROR('Invalid Column Name provided.', 16, 1);
            RETURN;
        END

        -- Validate the search value (ensure it's not empty or NULL)
        IF ISNULL(LTRIM(RTRIM(@SearchValue)), '') = ''
        BEGIN
            RAISERROR('Search value cannot be empty.', 16, 1);
            RETURN;
        END

        -- Prepare the search pattern based on the mode
        SET @SearchPattern =
            CASE 
                WHEN @Mode = 'Anywhere' THEN '%' + LTRIM(RTRIM(@SearchValue)) + '%'
                WHEN @Mode = 'StartsWith' THEN LTRIM(RTRIM(@SearchValue)) + '%'
                WHEN @Mode = 'EndsWith' THEN '%' + LTRIM(RTRIM(@SearchValue))
                WHEN @Mode = 'ExactMatch' THEN LTRIM(RTRIM(@SearchValue))
                ELSE '%' + LTRIM(RTRIM(@SearchValue)) + '%'
            END;

        -- Build the dynamic SQL query safely
        SET @SQL = N'SELECT * FROM ' + QUOTENAME('TestAppointments') + 
                   N' WHERE ' + QUOTENAME(@ActualColumn) + N' LIKE @SearchPattern OPTION (RECOMPILE)';

        -- Execute the dynamic SQL with parameterized search pattern
        EXEC sp_executesql @SQL, N'@SearchPattern NVARCHAR(255)', @SearchPattern;
    END TRY
    BEGIN CATCH
        -- Handle errors
        EXEC SP_HandleError;
    END CATCH
END;
GO
