USE [DXCOrdersDB]
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [Creator], [TaskName], [TaskDescription], [CreatedOn], [Assignee], [Status], [Note], [LockStatus]) VALUES (1, N'Ivan', N'asd', N'asd', CAST(N'2018-05-20T10:00:00.000' AS DateTime), NULL, N'New', NULL, 0)
INSERT [dbo].[Orders] ([Id], [Creator], [TaskName], [TaskDescription], [CreatedOn], [Assignee], [Status], [Note], [LockStatus]) VALUES (2, N'Jano', N'Central', N'Pijeme', CAST(N'2018-05-24T10:29:46.000' AS DateTime), N'', N'Assigned', N'', 0)
INSERT [dbo].[Orders] ([Id], [Creator], [TaskName], [TaskDescription], [CreatedOn], [Assignee], [Status], [Note], [LockStatus]) VALUES (3, N'Jaro', N'Vecera', N'Pivaren', CAST(N'2018-05-24T10:30:20.000' AS DateTime), N'', N'InProgress', N'', 0)
INSERT [dbo].[Orders] ([Id], [Creator], [TaskName], [TaskDescription], [CreatedOn], [Assignee], [Status], [Note], [LockStatus]) VALUES (4, N'Juraj', N'Obed', N'Kika', CAST(N'2018-05-24T10:35:03.000' AS DateTime), N'', N'Closed', N'', 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
