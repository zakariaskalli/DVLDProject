-- Stored Procedures for Table: Applications

Use [DVLD];
Go


CREATE OR ALTER PROCEDURE SP_Get_Applications_ByID
(
    @ApplicationID int
)
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve data
        SELECT *
        FROM Applications
        WHERE ApplicationID = @ApplicationID;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Get_All_Applications
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve all data from the table
        SELECT *
        FROM Applications;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Add_Applications
(
    @ApplicantPersonID int,
    @ApplicationDate datetime,
    @ApplicationTypeID int,
    @ApplicationStatus tinyint,
    @LastStatusDate datetime,
    @PaidFees smallmoney,
    @CreatedByUserID int,
    @NewID INT OUTPUT

)
AS
BEGIN
    BEGIN TRY
        -- Check if any required parameters are NULL
        IF LTRIM(RTRIM(@ApplicantPersonID)) IS NULL OR LTRIM(RTRIM(@ApplicationDate)) IS NULL OR LTRIM(RTRIM(@ApplicationTypeID)) IS NULL OR LTRIM(RTRIM(@ApplicationStatus)) IS NULL OR LTRIM(RTRIM(@LastStatusDate)) IS NULL OR LTRIM(RTRIM(@PaidFees)) IS NULL OR LTRIM(RTRIM(@CreatedByUserID)) IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Insert the data into the table
        INSERT INTO Applications ([ApplicantPersonID],[ApplicationDate],[ApplicationTypeID],[ApplicationStatus],[LastStatusDate],[PaidFees],[CreatedByUserID])
        VALUES (    LTRIM(RTRIM(@ApplicantPersonID)),
    LTRIM(RTRIM(@ApplicationDate)),
    LTRIM(RTRIM(@ApplicationTypeID)),
    LTRIM(RTRIM(@ApplicationStatus)),
    LTRIM(RTRIM(@LastStatusDate)),
    LTRIM(RTRIM(@PaidFees)),
    LTRIM(RTRIM(@CreatedByUserID)));

        -- Set the new ID
        SET @NewID = SCOPE_IDENTITY();  -- Get the last inserted ID
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Error handling
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Update_Applications_ByID
(
    @ApplicationID int,
    @ApplicantPersonID int,
    @ApplicationDate datetime,
    @ApplicationTypeID int,
    @ApplicationStatus tinyint,
    @LastStatusDate datetime,
    @PaidFees smallmoney,
    @CreatedByUserID int
)
AS
BEGIN
    BEGIN TRY
        -- Check if required parameters are NULL or contain only whitespace after trimming
        IF LTRIM(RTRIM(@ApplicantPersonID)) IS NULL OR LTRIM(RTRIM(@ApplicantPersonID)) = '' OR LTRIM(RTRIM(@ApplicationDate)) IS NULL OR LTRIM(RTRIM(@ApplicationDate)) = '' OR LTRIM(RTRIM(@ApplicationTypeID)) IS NULL OR LTRIM(RTRIM(@ApplicationTypeID)) = '' OR LTRIM(RTRIM(@ApplicationStatus)) IS NULL OR LTRIM(RTRIM(@ApplicationStatus)) = '' OR LTRIM(RTRIM(@LastStatusDate)) IS NULL OR LTRIM(RTRIM(@LastStatusDate)) = '' OR LTRIM(RTRIM(@PaidFees)) IS NULL OR LTRIM(RTRIM(@PaidFees)) = '' OR LTRIM(RTRIM(@CreatedByUserID)) IS NULL OR LTRIM(RTRIM(@CreatedByUserID)) = ''
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Update the record in the table
        UPDATE Applications
        SET     [ApplicantPersonID] = LTRIM(RTRIM(@ApplicantPersonID)),
    [ApplicationDate] = LTRIM(RTRIM(@ApplicationDate)),
    [ApplicationTypeID] = LTRIM(RTRIM(@ApplicationTypeID)),
    [ApplicationStatus] = LTRIM(RTRIM(@ApplicationStatus)),
    [LastStatusDate] = LTRIM(RTRIM(@LastStatusDate)),
    [PaidFees] = LTRIM(RTRIM(@PaidFees)),
    [CreatedByUserID] = LTRIM(RTRIM(@CreatedByUserID))
        WHERE ApplicationID = @ApplicationID;
        
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


CREATE OR ALTER PROCEDURE SP_Delete_Applications_ByID
(
    @ApplicationID int
)
AS
BEGIN

    BEGIN TRY
        -- Check if the record exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM Applications WHERE ApplicationID = @ApplicationID)
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END

        -- Attempt to delete the record
        DELETE FROM Applications WHERE ApplicationID = @ApplicationID;

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


CREATE OR ALTER PROCEDURE SP_Search_Applications_ByColumn
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
                WHEN 'ApplicationID' THEN 'ApplicationID'
        WHEN 'ApplicantPersonID' THEN 'ApplicantPersonID'
        WHEN 'ApplicationDate' THEN 'ApplicationDate'
        WHEN 'ApplicationTypeID' THEN 'ApplicationTypeID'
        WHEN 'ApplicationStatus' THEN 'ApplicationStatus'
        WHEN 'LastStatusDate' THEN 'LastStatusDate'
        WHEN 'PaidFees' THEN 'PaidFees'
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
        SET @SQL = N'SELECT * FROM ' + QUOTENAME('Applications') + 
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
