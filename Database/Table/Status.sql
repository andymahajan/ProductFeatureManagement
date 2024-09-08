USE ProductFeatureMgmt;
-- Check if the table 'Status' exists
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type = N'U')
BEGIN
    -- Create table 'Status'
    CREATE TABLE dbo.Status
    (
        StatusId INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing StatusId with primary key
        StatusName NVARCHAR(50) NOT NULL        -- StatusName with datatype nvarchar(50)
    );
    
	INSERT INTO dbo.Status (StatusName)
	VALUES 
	('New'),
	('Active'),
	('Closed'),
	('Abandoned');

    PRINT 'Table "Status" created successfully.';
END
ELSE
BEGIN
    PRINT 'Table "Status" already exists.';
END

-- Verify the table values
SELECT * FROM Status;