-- Create a new table called '[Reports]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Reports]', 'U') IS NOT NULL
DROP TABLE [dbo].[Reports]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Reports]
(
    [id] INT NOT NULL IDENTITY PRIMARY KEY, -- Primary Key column
    [urlId] INT NOT NULL,
    [fetchTime] NVARCHAR(50) NOT NULL,
    [performance] DECIMAL(3,2) NOT NULL
    -- Specify more columns here
);
GO

SELECT * FROM Reports


