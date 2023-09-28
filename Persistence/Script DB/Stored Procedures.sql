﻿
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'GetJobs'
    AND ROUTINE_TYPE = N'PROCEDURE'
)

DROP PROCEDURE dbo.GetJobs
GO

CREATE OR ALTER PROCEDURE dbo.GetJobs(@pageSize integer, @page integer)

AS
BEGIN
    SELECT * from Job where isactive = 1 order by namejob
    offset @pageSize*@page rows 
    fetch next @pageSize rows only;
END;

exec dbo.GetJobs 10,1;