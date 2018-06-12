USE [DXCOrdersDB]
GO

/****** Object:  Table [dbo].[Orders]    Script Date: 06/04/18 09:42:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Creator] [nvarchar](20) NOT NULL,
	[TaskName] [nvarchar](20) NOT NULL,
	[TaskDescription] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Assignee] [nvarchar](20) NULL,
	[Status] [nvarchar](15) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[LockStatus] [bit] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [DXCOrdersDB]
GO

/****** Object:  Table [dbo].[OrdersInfo]    Script Date: 06/04/18 09:44:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrdersInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[header] [nchar](20) NULL,
	[dropdownmenu] [nvarchar](15) NULL,
 CONSTRAINT [PK_OrdersInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


