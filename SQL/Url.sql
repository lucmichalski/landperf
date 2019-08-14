-- Create a new table called '[Urls]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Urls]', 'U') IS NOT NULL
DROP TABLE [dbo].[Urls]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Urls]
(
    [id] INT NOT NULL IDENTITY PRIMARY KEY, -- Primary Key column
    [siteId] INT NOT NULL,
    [name] NVARCHAR(50) NOT NULL,
   
);
GO



-- Insert rows into table 'Urls' in schema '[dbo]'
INSERT INTO [dbo].[Urls]
( -- Columns to insert data into
 [siteId], [name]
)
VALUES
( -- First row: values for the columns in the list above
 1, 'https://www.landsofamerica.com'
),
( -- First row: values for the columns in the list above
 2, 'https://www.landwatch.com'
)
-- Add more rows here
GO



-- Select rows from a Table or View '[Urls]' in schema '[dbo]'
SELECT * FROM [dbo].[Urls]
