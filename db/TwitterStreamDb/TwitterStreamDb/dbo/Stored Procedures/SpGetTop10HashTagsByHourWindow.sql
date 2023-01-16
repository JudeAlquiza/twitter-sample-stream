
CREATE PROCEDURE [dbo].[SpGetTop10HashTagsByHourWindow]  
   @hourWindow INT
AS  
BEGIN  
   DECLARE @CURRENT_DATE DATETIME = GETDATE()

   SELECT TOP 10 H.HashTag, COUNT(H.HashTag) AS [HashTagCount] FROM 
	(
		SELECT 
			* 
		FROM [dbo].[HashTags] 
		WHERE [CreatedAt] < @CURRENT_DATE AND [CreatedAt] >= DATEADD(HOUR, -@hourWindow, @CURRENT_DATE)
	) AS H
	GROUP BY H.HashTag
	ORDER BY COUNT(H.HashTag) DESC
END
