USE [StoreManagement]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 08/27/2017 19:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[IDCurrency] [int] IDENTITY(1,1) NOT NULL,
	[NameCurrency] [nvarchar](50) NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[IDCurrency] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckQuntity]    Script Date: 08/27/2017 19:15:27 ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 08/27/2017 19:15:27 ******/
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
/****** Object:  Table [dbo].[Account]    Script Date: 08/27/2017 19:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[IDAccount] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[Quntity] [int] NULL,
	[Price] [int] NULL,
	[IDCurrency] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[IDAccount] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpdSupply]    Script Date: 08/27/2017 19:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpdSupply](
	[IDUpt] [int] IDENTITY(1,1) NOT NULL,
	[IDSupply] [int] NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[Quntity] [int] NULL,
	[Price] [int] NULL,
	[NameSupply] [nvarchar](150) NULL,
	[DescSupply] [nvarchar](250) NULL,
	[DateSupply] [datetime] NULL,
	[DescUpd] [nvarchar](250) NULL,
	[dateUpd] [datetime] NULL,
	[IDCurrency] [int] NULL,
 CONSTRAINT [PK_UpdSupply] PRIMARY KEY CLUSTERED 
(
	[IDUpt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeQuntity]    Script Date: 08/27/2017 19:15:27 ******/
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
/****** Object:  Table [dbo].[Setting]    Script Date: 08/27/2017 19:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[IDSetting] [int] NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[PassWord] [nvarchar](50) NULL,
	[img] [image] NULL,
 CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED 
(
	[IDSetting] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequstSupply]    Script Date: 08/27/2017 19:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequstSupply](
	[IDSupply] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NULL,
	[IDType] [int] NULL,
	[Quntity] [int] NULL,
	[Price] [int] NULL,
	[NameSupply] [nvarchar](150) NULL,
	[DescSupply] [nvarchar](250) NULL,
	[DateSupply] [datetime] NULL,
	[IDCurrency] [int] NULL,
 CONSTRAINT [PK_RequstSupply] PRIMARY KEY CLUSTERED 
(
	[IDSupply] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequstOut]    Script Date: 08/27/2017 19:15:27 ******/
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
	[DateOut] [datetime] NULL,
	[Chack] [int] NULL,
	[NameSend] [nvarchar](150) NULL,
	[Price] [int] NULL,
	[IDCurrency] [int] NULL,
 CONSTRAINT [PK_RequstOut] PRIMARY KEY CLUSTERED 
(
	[IDOut] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlaceSend]    Script Date: 08/27/2017 19:15:27 ******/
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
/****** Object:  StoredProcedure [dbo].[GetRequsetSupply]    Script Date: 08/27/2017 19:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetRequsetSupply]
@idSupply int
as
 SELECT  Category.NameCategory as'اسم الصنف' ,TypeQuntity.NameType as 'نوع الصنف',
RequstSupply.DateSupply as 'تاريخ التوريد',RequstSupply.DescSupply as 'وصف التوريد',
RequstSupply.NameSupply as 'اسم المورد',RequstSupply.Price as 'السعر',RequstSupply.Quntity as 'الكمية الموردة'
from Category,TypeQuntity,RequstSupply 
where RequstSupply.IDCategory=Category.IDCategory and RequstSupply.IDType=TypeQuntity.IDType and RequstSupply.IDSupply=@idSupply
GO
