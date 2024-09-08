USE ProductFeatureMgmt;
-- Check if the table 'Complexity' exists
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Complexity]') AND type = N'U')
BEGIN
    -- Create table 'Complexity'
    CREATE TABLE dbo.Complexity
    (
        ComplexityId INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing ComplexityId with primary key
        ComplexityName NVARCHAR(20) NOT NULL         -- ComplexityName with datatype nvarchar(20)
    );
	
	-- Insert values into the Complexity table
	INSERT INTO dbo.Complexity (ComplexityName)
	VALUES 
	('S'),
	('M'),
	('L'),
	('XL');
    
    PRINT 'Table "Complexity" created successfully.';
END
ELSE
BEGIN
    PRINT 'Table "Complexity" already exists.';
END

-- Verify the table values
SELECT * FROM dbo.Complexity
