USE [DXCOrdersDB]
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 


DECLARE @i int = 5

WHILE @i < 10
BEGIN
    SET @i = @i + 1
    /* do some work */
/*PRINT @i*/
INSERT [dbo].[Orders] ([Id], [Creator], [TaskName], [TaskDescription], [CreatedOn], [Assignee], [Status], [Note], [LockStatus]) VALUES (@i, N'Juraj', N'Obed', N'Kika', CAST(N'2018-05-24T10:35:03.000' AS DateTime), N'', N'Closed', N'', 0)
END


SET IDENTITY_INSERT [dbo].[Orders] OFF
