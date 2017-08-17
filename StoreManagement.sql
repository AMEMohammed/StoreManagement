USE [StoreManagement]
GO
/****** Object:  Table [dbo].[TypeQuntity]    Script Date: 08/17/2017 21:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeQuntity](
	[IDType] [int] IDENTITY(1,1) NOT NULL,
	[NameType] [nvarchar](150) NULL,
 CONSTRAINT [PK_TypeQuntity] PRIMARY KEY CLUSTERED 
(
	[IDType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequstSupply]    Script Date: 08/17/2017 21:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequstSupply](
	[IDSupply] [int] NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[Quntity] [int] NULL,
	[Price] [int] NULL,
	[NameSupply] [nvarchar](250) NULL,
	[DescSupply] [nvarchar](250) NULL,
	[DateSupply] [date] NULL,
 CONSTRAINT [PK_RequstSupply] PRIMARY KEY CLUSTERED 
(
	[IDSupply] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequstOut]    Script Date: 08/17/2017 21:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequstOut](
	[IDOut] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[IDPlace] [int] NULL,
	[Quntity] [int] NULL,
	[NameOut] [nvarchar](150) NULL,
	[DesOut] [nvarchar](150) NULL,
	[DateOut] [date] NULL,
	[Chack] [int] NULL,
 CONSTRAINT [PK_RequstOut] PRIMARY KEY CLUSTERED 
(
	[IDOut] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlaceSend]    Script Date: 08/17/2017 21:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlaceSend](
	[IDPlace] [int] IDENTITY(1,1) NOT NULL,
	[NamePlace] [nvarchar](150) NULL,
 CONSTRAINT [PK_PlaceSend] PRIMARY KEY CLUSTERED 
(
	[IDPlace] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckQuntity]    Script Date: 08/17/2017 21:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckQuntity](
	[IDCheck] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[LessQuntity] [int] NULL,
	[CurrntQuntity] [int] NULL,
 CONSTRAINT [PK_CheckQuntity] PRIMARY KEY CLUSTERED 
(
	[IDCheck] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 08/17/2017 21:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[IDCategory] [int] IDENTITY(1,1) NOT NULL,
	[NameCategory] [nvarchar](150) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[IDCategory] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 08/17/2017 21:46:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[IDAccount] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[IDPace] [int] NULL,
	[Quntity] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[IDAccount] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
