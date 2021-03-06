USE [DXCOrdersDB]
GO
SET IDENTITY_INSERT [dbo].[OrdersInfo] ON 

INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (1, N'Id                  ', N'New')
INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (2, N'Creator             ', N'InProgress')
INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (3, N'TaskName            ', N'Closed')
INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (4, N'TaskDescription     ', N'Implemented')
INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (5, N'CreatedOn           ', N'Assigned')
INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (6, N'Assignee            ', NULL)
INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (7, N'Status              ', NULL)
INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (8, N'Note                ', NULL)
INSERT [dbo].[OrdersInfo] ([Id], [header], [dropdownmenu]) VALUES (9, N'LockStatus          ', NULL)
SET IDENTITY_INSERT [dbo].[OrdersInfo] OFF
