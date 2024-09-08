-- Check if the database exists before creating
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ProductFeatureMgmt')
BEGIN
    -- Create Database Script
    CREATE DATABASE ProductFeatureMgmt
    ON PRIMARY 
    (
        NAME = ProductFeatureMgmt_Data,  -- Logical name of the data file
        FILENAME = 'D:\ProductFeatureManagement\ProductFeatureManagement\Database\Schema\ProductFeatureMgmt_Data.mdf',  -- Path to the physical file
        SIZE = 10MB,  -- Initial size
        MAXSIZE = 100MB,  -- Maximum size (optional)
        FILEGROWTH = 5MB  -- Growth increment
    )
    LOG ON
    (
        NAME = ProductFeatureMgmt_Log,  -- Logical name of the log file
        FILENAME = 'D:\ProductFeatureManagement\ProductFeatureManagement\Database\Schema\ProductFeatureMgmt_Log.ldf',  -- Path to the physical log file
        SIZE = 5MB,  -- Initial size
        MAXSIZE = 50MB,  -- Maximum size (optional)
        FILEGROWTH = 1MB  -- Growth increment
    );

    PRINT 'Database "ProductFeatureMgmt" created successfully.';
END
ELSE
BEGIN
    PRINT 'Database "ProductFeatureMgmt" already exists.';
END

-- Verify the database creation
SELECT name, state_desc 
FROM sys.databases
WHERE name = 'ProductFeatureMgmt';
