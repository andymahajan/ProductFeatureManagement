USE ProductFeatureMgmt;
-- Check if the table 'Features' exists
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Features]') AND type = N'U')
BEGIN
    -- Create table 'Features'
    CREATE TABLE dbo.Features
    (
        FeaturesId BIGINT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing FeaturesId with primary key
        Title NVARCHAR(1000) NOT NULL,                -- Title with maximum 1000 characters, required
        Description NVARCHAR(MAX) NULL,               -- Description with maximum characters, nullable
        ComplexityId INT,                             -- Foreign key to Complexity table
        StatusId INT,                                 -- Foreign key to Status table
        TargetCompletionDate DATE NULL,               -- Nullable date
        ActualCompletionDate DATE NULL,               -- Nullable date
        
        -- Define foreign key constraints
        CONSTRAINT FK_Features_Complexity FOREIGN KEY (ComplexityId) REFERENCES dbo.Complexity(ComplexityId),
        CONSTRAINT FK_Features_Status FOREIGN KEY (StatusId) REFERENCES dbo.Status(StatusId)
    );
    
    PRINT 'Table "Features" created successfully.';
END
ELSE
BEGIN
    PRINT 'Table "Features" already exists.';
END

-- Verify the table creation
SELECT * FROM sys.tables WHERE name = 'Features';
