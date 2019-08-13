-- Create a new table called '[Urls]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Url]', 'U') IS NOT NULL
DROP TABLE [dbo].[Url]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Url]
(
    [id] INT NOT NULL IDENTITY PRIMARY KEY, -- Primary Key column
    [site_id] INT NOT NULL,
    [name] NVARCHAR(50) NOT NULL,
   
);
GO

-- Select rows from a Table or View '[Urls]' in schema '[dbo]'
SELECT * FROM [dbo].[Url]

-- Insert rows into table 'Urls' in schema '[dbo]'
INSERT INTO [dbo].[Url]
( -- Columns to insert data into
 [site_id], [name]
)
VALUES
( -- First row: values for the columns in the list above
 1, 'https://www.landsofamerica.com'
)
-- Add more rows here
GO

