-- Stored Procedures for Table: LicenseClasses

Use [DVLD];
Go


CREATE OR ALTER PROCEDURE SP_Get_LicenseClasses_ByID
(
    @LicenseClassID int
)
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve data
        SELECT *
        FROM LicenseClasses
        WHERE LicenseClassID = @LicenseClassID;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Get_All_LicenseClasses
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve all data from the table
        SELECT *
        FROM LicenseClasses;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Add_LicenseClasses
(
    @ClassName nvarchar(50),
    @ClassDescription nvarchar(500),
    @MinimumAllowedAge tinyint,
    @DefaultValidityLength tinyint,
    @ClassFees smallmoney,
    @NewID INT OUTPUT

)
AS
BEGIN
    BEGIN TRY
        -- Check if any required parameters are NULL
        IF LTRIM(RTRIM(@ClassName)) IS NULL OR LTRIM(RTRIM(@ClassDescription)) IS NULL OR LTRIM(RTRIM(@MinimumAllowedAge)) IS NULL OR LTRIM(RTRIM(@DefaultValidityLength)) IS NULL OR LTRIM(RTRIM(@ClassFees)) IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Insert the data into the table
        INSERT INTO LicenseClasses ([ClassName],[ClassDescription],[MinimumAllowedAge],[DefaultValidityLength],[ClassFees])
        VALUES (    LTRIM(RTRIM(@ClassName)),
    LTRIM(RTRIM(@ClassDescription)),
    LTRIM(RTRIM(@MinimumAllowedAge)),
    LTRIM(RTRIM(@DefaultValidityLength)),
    LTRIM(RTRIM(@ClassFees)));

        -- Set the new ID
        SET @NewID = SCOPE_IDENTITY();  -- Get the last inserted ID
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Error handling
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Update_LicenseClasses_ByID
(
    @LicenseClassID int,
    @ClassName nvarchar(50),
    @ClassDescription nvarchar(500),
    @MinimumAllowedAge tinyint,
    @DefaultValidityLength tinyint,
    @ClassFees smallmoney
)
AS
BEGIN
    BEGIN TRY
        -- Check if required parameters are NULL or contain only whitespace after trimming
        IF LTRIM(RTRIM(@ClassName)) IS NULL OR LTRIM(RTRIM(@ClassName)) = '' OR LTRIM(RTRIM(@ClassDescription)) IS NULL OR LTRIM(RTRIM(@ClassDescription)) = '' OR LTRIM(RTRIM(@MinimumAllowedAge)) IS NULL OR LTRIM(RTRIM(@MinimumAllowedAge)) = '' OR LTRIM(RTRIM(@DefaultValidityLength)) IS NULL OR LTRIM(RTRIM(@DefaultValidityLength)) = '' OR LTRIM(RTRIM(@ClassFees)) IS NULL OR LTRIM(RTRIM(@ClassFees)) = ''
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Update the record in the table
        UPDATE LicenseClasses
        SET     [ClassName] = LTRIM(RTRIM(@ClassName)),
    [ClassDescription] = LTRIM(RTRIM(@ClassDescription)),
    [MinimumAllowedAge] = LTRIM(RTRIM(@MinimumAllowedAge)),
    [DefaultValidityLength] = LTRIM(RTRIM(@DefaultValidityLength)),
    [ClassFees] = LTRIM(RTRIM(@ClassFees))
        WHERE LicenseClassID = @LicenseClassID;
        
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


CREATE OR ALTER PROCEDURE SP_Delete_LicenseClasses_ByID
(
    @LicenseClassID int
)
AS
BEGIN

    BEGIN TRY
        -- Check if the record exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID)
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END

        -- Attempt to delete the record
        DELETE FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID;

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


CREATE OR ALTER PROCEDURE SP_Search_LicenseClasses_ByColumn
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
                WHEN 'LicenseClassID' THEN 'LicenseClassID'
        WHEN 'ClassName' THEN 'ClassName'
        WHEN 'ClassDescription' THEN 'ClassDescription'
        WHEN 'MinimumAllowedAge' THEN 'MinimumAllowedAge'
        WHEN 'DefaultValidityLength' THEN 'DefaultValidityLength'
        WHEN 'ClassFees' THEN 'ClassFees'
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
        SET @SQL = N'SELECT * FROM ' + QUOTENAME('LicenseClasses') + 
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
