-- Stored Procedures for Table: Licenses

Use [DVLD];
Go


CREATE OR ALTER PROCEDURE SP_Get_Licenses_ByID
(
    @LicenseID int
)
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve data
        SELECT *
        FROM Licenses
        WHERE LicenseID = @LicenseID;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Get_All_Licenses
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve all data from the table
        SELECT *
        FROM Licenses;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Add_Licenses
(
    @ApplicationID int,
    @DriverID int,
    @LicenseClass int,
    @IssueDate datetime,
    @ExpirationDate datetime,
    @Notes nvarchar(500) = NULL,
    @PaidFees smallmoney,
    @IsActive bit,
    @IssueReason tinyint,
    @CreatedByUserID int,
    @NewID INT OUTPUT

)
AS
BEGIN
    BEGIN TRY
        -- Check if any required parameters are NULL
        IF LTRIM(RTRIM(@ApplicationID)) IS NULL OR LTRIM(RTRIM(@DriverID)) IS NULL OR LTRIM(RTRIM(@LicenseClass)) IS NULL OR LTRIM(RTRIM(@IssueDate)) IS NULL OR LTRIM(RTRIM(@ExpirationDate)) IS NULL OR LTRIM(RTRIM(@PaidFees)) IS NULL OR LTRIM(RTRIM(@IsActive)) IS NULL OR LTRIM(RTRIM(@IssueReason)) IS NULL OR LTRIM(RTRIM(@CreatedByUserID)) IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Insert the data into the table
        INSERT INTO Licenses ([ApplicationID],[DriverID],[LicenseClass],[IssueDate],[ExpirationDate],[Notes],[PaidFees],[IsActive],[IssueReason],[CreatedByUserID])
        VALUES (    LTRIM(RTRIM(@ApplicationID)),
    LTRIM(RTRIM(@DriverID)),
    LTRIM(RTRIM(@LicenseClass)),
    LTRIM(RTRIM(@IssueDate)),
    LTRIM(RTRIM(@ExpirationDate)),
    LTRIM(RTRIM(@Notes)),
    LTRIM(RTRIM(@PaidFees)),
    LTRIM(RTRIM(@IsActive)),
    LTRIM(RTRIM(@IssueReason)),
    LTRIM(RTRIM(@CreatedByUserID))
);

        -- Set the new ID
        SET @NewID = SCOPE_IDENTITY();  -- Get the last inserted ID
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Error handling
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Update_Licenses_ByID
(
    @LicenseID int,
    @ApplicationID int,
    @DriverID int,
    @LicenseClass int,
    @IssueDate datetime,
    @ExpirationDate datetime,
    @Notes nvarchar(500) = NULL,
    @PaidFees smallmoney,
    @IsActive bit,
    @IssueReason tinyint,
    @CreatedByUserID int

)
AS
BEGIN
    BEGIN TRY
        -- Check if required parameters are NULL or contain only whitespace after trimming
        IF LTRIM(RTRIM(@ApplicationID)) IS NULL OR LTRIM(RTRIM(@ApplicationID)) = '' OR LTRIM(RTRIM(@DriverID)) IS NULL OR LTRIM(RTRIM(@DriverID)) = '' OR LTRIM(RTRIM(@LicenseClass)) IS NULL OR LTRIM(RTRIM(@LicenseClass)) = '' OR LTRIM(RTRIM(@IssueDate)) IS NULL OR LTRIM(RTRIM(@IssueDate)) = '' OR LTRIM(RTRIM(@ExpirationDate)) IS NULL OR LTRIM(RTRIM(@ExpirationDate)) = '' OR LTRIM(RTRIM(@PaidFees)) IS NULL OR LTRIM(RTRIM(@PaidFees)) = '' OR LTRIM(RTRIM(@IsActive)) IS NULL OR LTRIM(RTRIM(@IsActive)) = '' OR LTRIM(RTRIM(@IssueReason)) IS NULL OR LTRIM(RTRIM(@IssueReason)) = '' OR LTRIM(RTRIM(@CreatedByUserID)) IS NULL OR LTRIM(RTRIM(@CreatedByUserID)) = ''
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Update the record in the table
        UPDATE Licenses
        SET     [ApplicationID] = LTRIM(RTRIM(@ApplicationID)),
    [DriverID] = LTRIM(RTRIM(@DriverID)),
    [LicenseClass] = LTRIM(RTRIM(@LicenseClass)),
    [IssueDate] = LTRIM(RTRIM(@IssueDate)),
    [ExpirationDate] = LTRIM(RTRIM(@ExpirationDate)),
    [Notes] = LTRIM(RTRIM(@Notes)),
    [PaidFees] = LTRIM(RTRIM(@PaidFees)),
    [IsActive] = LTRIM(RTRIM(@IsActive)),
    [IssueReason] = LTRIM(RTRIM(@IssueReason)),
    [CreatedByUserID] = LTRIM(RTRIM(@CreatedByUserID))

        WHERE LicenseID = @LicenseID;
        
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


CREATE OR ALTER PROCEDURE SP_Delete_Licenses_ByID
(
    @LicenseID int
)
AS
BEGIN

    BEGIN TRY
        -- Check if the record exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM Licenses WHERE LicenseID = @LicenseID)
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END

        -- Attempt to delete the record
        DELETE FROM Licenses WHERE LicenseID = @LicenseID;

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


CREATE OR ALTER PROCEDURE SP_Search_Licenses_ByColumn
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
                WHEN 'LicenseID' THEN 'LicenseID'
        WHEN 'ApplicationID' THEN 'ApplicationID'
        WHEN 'DriverID' THEN 'DriverID'
        WHEN 'LicenseClass' THEN 'LicenseClass'
        WHEN 'IssueDate' THEN 'IssueDate'
        WHEN 'ExpirationDate' THEN 'ExpirationDate'
        WHEN 'Notes' THEN 'Notes'
        WHEN 'PaidFees' THEN 'PaidFees'
        WHEN 'IsActive' THEN 'IsActive'
        WHEN 'IssueReason' THEN 'IssueReason'
        WHEN 'CreatedByUserID' THEN 'CreatedByUserID'
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
        SET @SQL = N'SELECT * FROM ' + QUOTENAME('Licenses') + 
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
