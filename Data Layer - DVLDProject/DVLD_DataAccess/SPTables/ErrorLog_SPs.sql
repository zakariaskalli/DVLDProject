-- Stored Procedures for Table: ErrorLog

Use [DVLD];
Go


CREATE OR ALTER PROCEDURE SP_Get_ErrorLog_ByID
(
    @ErrorID int
)
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve data
        SELECT *
        FROM ErrorLog
        WHERE ErrorID = @ErrorID;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Get_All_ErrorLog
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve all data from the table
        SELECT *
        FROM ErrorLog;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Add_ErrorLog
(
    @ErrorMessage nvarchar(MAX),
    @StackTrace nvarchar(MAX) = NULL,
    @Timestamp datetime = NULL,
    @Severity nvarchar(50) = NULL,
    @AdditionalInfo nvarchar(MAX) = NULL,
    @NewID INT OUTPUT

)
AS
BEGIN
    BEGIN TRY
        -- Check if any required parameters are NULL
        IF LTRIM(RTRIM(@ErrorMessage)) IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Insert the data into the table
        INSERT INTO ErrorLog ([ErrorMessage],[StackTrace],[Timestamp],[Severity],[AdditionalInfo])
        VALUES (    LTRIM(RTRIM(@ErrorMessage)),
    LTRIM(RTRIM(@StackTrace)),
    LTRIM(RTRIM(@Timestamp)),
    LTRIM(RTRIM(@Severity)),
    LTRIM(RTRIM(@AdditionalInfo)));

        -- Set the new ID
        SET @NewID = SCOPE_IDENTITY();  -- Get the last inserted ID
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Error handling
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Update_ErrorLog_ByID
(
    @ErrorID int,
    @ErrorMessage nvarchar(MAX),
    @StackTrace nvarchar(MAX) = NULL,
    @Timestamp datetime = NULL,
    @Severity nvarchar(50) = NULL,
    @AdditionalInfo nvarchar(MAX) = NULL
)
AS
BEGIN
    BEGIN TRY
        -- Check if required parameters are NULL or contain only whitespace after trimming
        IF LTRIM(RTRIM(@ErrorMessage)) IS NULL OR LTRIM(RTRIM(@ErrorMessage)) = ''
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Update the record in the table
        UPDATE ErrorLog
        SET     [ErrorMessage] = LTRIM(RTRIM(@ErrorMessage)),
    [StackTrace] = LTRIM(RTRIM(@StackTrace)),
    [Timestamp] = LTRIM(RTRIM(@Timestamp)),
    [Severity] = LTRIM(RTRIM(@Severity)),
    [AdditionalInfo] = LTRIM(RTRIM(@AdditionalInfo))
        WHERE ErrorID = @ErrorID;
        
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


CREATE OR ALTER PROCEDURE SP_Delete_ErrorLog_ByID
(
    @ErrorID int
)
AS
BEGIN

    BEGIN TRY
        -- Check if the record exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM ErrorLog WHERE ErrorID = @ErrorID)
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END

        -- Attempt to delete the record
        DELETE FROM ErrorLog WHERE ErrorID = @ErrorID;

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


CREATE OR ALTER PROCEDURE SP_Search_ErrorLog_ByColumn
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
                WHEN 'ErrorID' THEN 'ErrorID'
        WHEN 'ErrorMessage' THEN 'ErrorMessage'
        WHEN 'StackTrace' THEN 'StackTrace'
        WHEN 'Timestamp' THEN 'Timestamp'
        WHEN 'Severity' THEN 'Severity'
        WHEN 'AdditionalInfo' THEN 'AdditionalInfo'
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
        SET @SQL = N'SELECT * FROM ' + QUOTENAME('ErrorLog') + 
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
