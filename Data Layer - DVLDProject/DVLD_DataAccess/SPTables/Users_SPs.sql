
-- Stored Procedures for Table: Users

Use [DVLD];
Go


CREATE OR ALTER PROCEDURE SP_Get_Users_ByID
(
    @UserID int
)
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve data
        SELECT *
        FROM Users_View
        WHERE UserID = @UserID;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Get_All_Users
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve all data from the table
        SELECT *
        FROM Users_View;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Add_Users
(
    @PersonID INT,
    @UserName NVARCHAR(20),
    @Password NVARCHAR(255),
    @IsActive BIT,
    @NewID INT OUTPUT
)
AS
BEGIN
    BEGIN TRY
        -- Validate required parameters
        IF @PersonID IS NULL OR LTRIM(RTRIM(@UserName)) = '' OR LTRIM(RTRIM(@Password)) = '' OR @IsActive IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or empty.', 16, 1);
            RETURN;
        END

        -- Generate a new Salt
        DECLARE @Salt UNIQUEIDENTIFIER = NEWID();
        -- Hash the password using the salt
        DECLARE @PasswordHash VARBINARY(64) = dbo.HashPassword(@Password, @Salt);

        -- Insert into Users table
        INSERT INTO Users ([PersonID], [UserName], [IsActive], [PasswordHash], [Salt])
        VALUES (@PersonID, @UserName, @IsActive, @PasswordHash, @Salt);

        -- Get the newly inserted UserID
        SET @NewID = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Error handling
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Update_Users_ByID
(
    @UserID INT,
    @PersonID INT,
    @UserName NVARCHAR(20),
    @Password NVARCHAR(255) = NULL,  -- Optional password update
    @IsActive BIT
)
AS
BEGIN
    BEGIN TRY
        -- Validate required parameters
        IF @UserID IS NULL OR @PersonID IS NULL OR LTRIM(RTRIM(@UserName)) = '' OR @IsActive IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or empty.', 16, 1);
            RETURN;
        END

        -- Declare variables for Salt & PasswordHash
        DECLARE @Salt UNIQUEIDENTIFIER;
        DECLARE @PasswordHash VARBINARY(64);

        -- Check if a new password is provided
        IF @Password IS NOT NULL AND LTRIM(RTRIM(@Password)) <> ''
        BEGIN
            SET @Salt = NEWID();  -- Generate new Salt
            SET @PasswordHash = dbo.HashPassword(@Password, @Salt);
        END
        ELSE
        BEGIN
            -- Keep existing Salt and PasswordHash
            SELECT @Salt = Salt, @PasswordHash = PasswordHash FROM Users WHERE UserID = @UserID;
        END

        -- Update the user record
        UPDATE Users
        SET PersonID = @PersonID,
            UserName = @UserName,
            IsActive = @IsActive,
            PasswordHash = @PasswordHash,
            Salt = @Salt
        WHERE UserID = @UserID;

        -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('No rows were updated. Please check the UserID or other parameters.', 16, 1);
            RETURN;
        END
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Handle errors
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Delete_Users_ByID
(
    @UserID int
)
AS
BEGIN

    BEGIN TRY
        -- Check if the record exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM Users WHERE UserID = @UserID)
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END

        -- Attempt to delete the record
        DELETE FROM Users WHERE UserID = @UserID;

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

CREATE OR ALTER PROCEDURE SP_Search_Users_ByColumn
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

        -- Map input column name to actual column name in Users_View
        SET @ActualColumn = 
            CASE @ColumnName
                WHEN 'UserID' THEN 'UserID'
                WHEN 'PersonID' THEN 'PersonID'
                WHEN 'UserName' THEN 'UserName'
                WHEN 'IsActive' THEN 'IsActive'
                WHEN 'FullName' THEN 'FullName'
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

        -- Build the dynamic SQL query safely to search in Users_View
        SET @SQL = N'SELECT * FROM ' + QUOTENAME('Users_View') + 
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



CREATE OR ALTER PROCEDURE SP_IsFoundUserByNationalNo
    @NationalNo VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        -- Attempt to check if the user exists by NationalNo
        SELECT 1 
        FROM Users 
        INNER JOIN People ON Users.PersonID = People.PersonID
        WHERE People.NationalNo = @NationalNo;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO
