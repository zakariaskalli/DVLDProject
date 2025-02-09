-- Stored Procedures for Table: People

Use [DVLD];
Go


CREATE OR ALTER PROCEDURE SP_Get_People_ByID
(
    @PersonID int
)
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve data
        SELECT *
        FROM People
        WHERE PersonID = @PersonID;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Get_All_People
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve all data from the table
        SELECT *
        FROM People;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Add_People
(
    @NationalNo nvarchar(20),
    @FirstName nvarchar(20),
    @SecondName nvarchar(20) = NULL,
    @ThirdName nvarchar(20) = NULL,
    @LastName nvarchar(20) = NULL,
    @DateOfBirth datetime,
    @Gendor tinyint,
    @Address nvarchar(500),
    @Phone nvarchar(20),
    @Email nvarchar(50) = NULL,
    @NationalityCountryID int,
    @ImagePath nvarchar(250) = NULL,
    @NewID INT OUTPUT

)
AS
BEGIN
    BEGIN TRY
        -- Check if any required parameters are NULL
        IF LTRIM(RTRIM(@NationalNo)) IS NULL OR LTRIM(RTRIM(@FirstName)) IS NULL OR LTRIM(RTRIM(@DateOfBirth)) IS NULL OR LTRIM(RTRIM(@Gendor)) IS NULL OR LTRIM(RTRIM(@Address)) IS NULL OR LTRIM(RTRIM(@Phone)) IS NULL OR LTRIM(RTRIM(@NationalityCountryID)) IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Insert the data into the table
        INSERT INTO People ([NationalNo],[FirstName],[SecondName],[ThirdName],[LastName],[DateOfBirth],[Gendor],[Address],[Phone],[Email],[NationalityCountryID],[ImagePath])
        VALUES (    LTRIM(RTRIM(@NationalNo)),
    LTRIM(RTRIM(@FirstName)),
    LTRIM(RTRIM(@SecondName)),
    LTRIM(RTRIM(@ThirdName)),
    LTRIM(RTRIM(@LastName)),
    LTRIM(RTRIM(@DateOfBirth)),
    LTRIM(RTRIM(@Gendor)),
    LTRIM(RTRIM(@Address)),
    LTRIM(RTRIM(@Phone)),
    LTRIM(RTRIM(@Email)),
    LTRIM(RTRIM(@NationalityCountryID)),
    LTRIM(RTRIM(@ImagePath))
);

        -- Set the new ID
        SET @NewID = SCOPE_IDENTITY();  -- Get the last inserted ID
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Error handling
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Update_People_ByID
(
    @PersonID int,
    @NationalNo nvarchar(20),
    @FirstName nvarchar(20),
    @SecondName nvarchar(20) = NULL,
    @ThirdName nvarchar(20) = NULL,
    @LastName nvarchar(20) = NULL,
    @DateOfBirth datetime,
    @Gendor tinyint,
    @Address nvarchar(500),
    @Phone nvarchar(20),
    @Email nvarchar(50) = NULL,
    @NationalityCountryID int,
    @ImagePath nvarchar(250) = NULL

)
AS
BEGIN
    BEGIN TRY
        -- Check if required parameters are NULL or contain only whitespace after trimming
        IF LTRIM(RTRIM(@NationalNo)) IS NULL OR LTRIM(RTRIM(@NationalNo)) = '' OR LTRIM(RTRIM(@FirstName)) IS NULL OR LTRIM(RTRIM(@FirstName)) = '' OR LTRIM(RTRIM(@DateOfBirth)) IS NULL OR LTRIM(RTRIM(@DateOfBirth)) = '' OR LTRIM(RTRIM(@Gendor)) IS NULL OR LTRIM(RTRIM(@Gendor)) = '' OR LTRIM(RTRIM(@Address)) IS NULL OR LTRIM(RTRIM(@Address)) = '' OR LTRIM(RTRIM(@Phone)) IS NULL OR LTRIM(RTRIM(@Phone)) = '' OR LTRIM(RTRIM(@NationalityCountryID)) IS NULL OR LTRIM(RTRIM(@NationalityCountryID)) = ''
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Update the record in the table
        UPDATE People
        SET     [NationalNo] = LTRIM(RTRIM(@NationalNo)),
    [FirstName] = LTRIM(RTRIM(@FirstName)),
    [SecondName] = LTRIM(RTRIM(@SecondName)),
    [ThirdName] = LTRIM(RTRIM(@ThirdName)),
    [LastName] = LTRIM(RTRIM(@LastName)),
    [DateOfBirth] = LTRIM(RTRIM(@DateOfBirth)),
    [Gendor] = LTRIM(RTRIM(@Gendor)),
    [Address] = LTRIM(RTRIM(@Address)),
    [Phone] = LTRIM(RTRIM(@Phone)),
    [Email] = LTRIM(RTRIM(@Email)),
    [NationalityCountryID] = LTRIM(RTRIM(@NationalityCountryID)),
    [ImagePath] = LTRIM(RTRIM(@ImagePath))

        WHERE PersonID = @PersonID;
        
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


CREATE OR ALTER PROCEDURE SP_Delete_People_ByID
(
    @PersonID int
)
AS
BEGIN

    BEGIN TRY
        -- Check if the record exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM People WHERE PersonID = @PersonID)
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END

        -- Attempt to delete the record
        DELETE FROM People WHERE PersonID = @PersonID;

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


CREATE OR ALTER PROCEDURE SP_Search_People_ByColumn
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
                WHEN 'PersonID' THEN 'PersonID'
        WHEN 'NationalNo' THEN 'NationalNo'
        WHEN 'FirstName' THEN 'FirstName'
        WHEN 'SecondName' THEN 'SecondName'
        WHEN 'ThirdName' THEN 'ThirdName'
        WHEN 'LastName' THEN 'LastName'
        WHEN 'DateOfBirth' THEN 'DateOfBirth'
        WHEN 'Gendor' THEN 'Gendor'
        WHEN 'Address' THEN 'Address'
        WHEN 'Phone' THEN 'Phone'
        WHEN 'Email' THEN 'Email'
        WHEN 'NationalityCountryID' THEN 'NationalityCountryID'
        WHEN 'ImagePath' THEN 'ImagePath'
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
        SET @SQL = N'SELECT * FROM ' + QUOTENAME('People') + 
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
