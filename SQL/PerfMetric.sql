-- Create a new table called '[PerfMetric]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[PerfMetric]', 'U') IS NOT NULL
DROP TABLE [dbo].[PerfMetric]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[PerfMetric]
(
   [id] INT NOT NULL IDENTITY PRIMARY KEY, -- Primary Key column
   [reportId] INT NOT NULL,
   [score] INT NOT NULL,
   [displayValue] NVARCHAR(max) NOT NUll,
   [numericValue] NUMERIC(10,3) NOT NULL,
   [title] NVARCHAR(max) NOT NUll,
   
    -- Specify more columns here
);
GO



-- Select rows from a Table or View '[TableOrViewName]' in schema '[dbo]'
SELECT * FROM [dbo].[PerfMetric]

GO



