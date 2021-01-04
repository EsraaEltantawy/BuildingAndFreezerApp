USE [BuildingandFreezer]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 5/22/2020 3:45:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankFreezer]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankFreezer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CurrentPrices] [decimal](30, 2) NULL,
	[FrID] [int] NULL,
 CONSTRAINT [PK_BankFreezer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Building]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Building](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[buildName] [nvarchar](50) NULL,
	[NamberHouses] [int] NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BuildingEmployee]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuildingEmployee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BuildID] [int] NULL,
	[empID] [int] NULL,
	[Price] [decimal](30, 2) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_BuildingEmployee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BuildingMatrial]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuildingMatrial](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BuildID] [int] NULL,
	[MatID] [int] NULL,
	[Prices] [decimal](30, 2) NULL,
	[MumOfMat] [decimal](30, 2) NULL,
	[PricePerOne] [decimal](30, 2) NULL,
	[Date] [datetime] NULL,
	[BillNumbe] [int] NULL,
 CONSTRAINT [PK_BuildingMatrial] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClientReserve]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientReserve](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NULL,
	[BuildID] [int] NULL,
	[FiatNum] [int] NULL,
	[Prices] [decimal](30, 2) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_ClientReserve] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Number] [decimal](30, 2) NULL,
	[PricePerOneSm] [decimal](18, 2) NULL,
	[PricePerOneLa] [decimal](30, 2) NULL,
	[NewPricePerOneLa] [decimal](18, 2) NULL,
	[NewPricePerOneSm] [decimal](18, 2) NULL,
	[Date] [datetime] NULL,
	[Size] [nvarchar](50) NULL,
	[Cash] [decimal](30, 2) NULL,
	[Change] [decimal](30, 2) NULL,
	[Section] [nvarchar](50) NULL,
	[Cat_id] [int] NULL,
	[FrID] [int] NOT NULL,
	[billID] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Document]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DocName] [nvarchar](50) NULL,
	[DocPath] [nvarchar](max) NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employess]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employess](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Employess] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FreezerCeteogry]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreezerCeteogry](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[PricePerTon] [int] NULL,
	[PricePerSmall] [int] NULL,
	[PricePerLarge] [int] NULL,
 CONSTRAINT [PK_FreezerCeteogry] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Fridage]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fridage](
	[Fr_id] [int] IDENTITY(1,1) NOT NULL,
	[Fr_Name] [nvarchar](100) NULL,
	[Fr_NumOfSection] [int] NULL,
 CONSTRAINT [PK_Fridage] PRIMARY KEY CLUSTERED 
(
	[Fr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Matriel]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matriel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[matName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Matriel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NonWorks]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NonWorks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[NonId] [int] NULL,
	[Det] [nvarchar](max) NULL,
	[Prices] [decimal](30, 2) NULL,
	[Data] [datetime] NULL,
	[BuildID] [int] NULL,
 CONSTRAINT [PK_NonWorks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NonWorksTable]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NonWorksTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nonName] [nvarchar](50) NULL,
 CONSTRAINT [PK_NonWorksTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payment]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Type] [nvarchar](50) NULL,
	[Details] [nvarchar](150) NULL,
	[Price] [decimal](30, 2) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[period]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[period](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PeriodName] [nchar](50) NULL,
	[isActive] [bit] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[FrID] [int] NULL,
 CONSTRAINT [PK_period] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reserve]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserve](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BuildID] [int] NULL,
	[FlatNumber] [int] NULL,
	[ClientName] [nvarchar](50) NULL,
	[ClientAddress] [nvarchar](max) NULL,
	[ClientPhoto] [nvarchar](100) NULL,
	[RePrices] [decimal](30, 2) NULL,
	[CashPrices] [decimal](30, 2) NULL,
	[Comission] [decimal](30, 2) NULL,
	[ChangePrices] [decimal](30, 2) NULL,
	[NumPrem] [int] NULL,
	[DateFirstPrem] [datetime] NULL,
	[Photo] [nvarchar](100) NULL,
	[Period] [int] NULL,
 CONSTRAINT [PK_Reserve] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Trader]    Script Date: 5/22/2020 3:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trader](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Number] [decimal](30, 4) NULL,
	[PricePerOne] [decimal](30, 2) NULL,
	[Date] [datetime] NULL,
	[Cash] [decimal](30, 2) NULL,
	[Change] [decimal](30, 2) NULL,
	[Section] [nvarchar](50) NULL,
	[NumberOfOne] [int] NULL,
	[Weight] [decimal](18, 2) NULL,
	[cat_Id] [int] NULL,
	[FrID] [int] NOT NULL,
	[NewPrice] [decimal](18, 2) NULL,
	[billID] [int] NULL,
 CONSTRAINT [PK_Trader] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BankFreezer]  WITH CHECK ADD  CONSTRAINT [FK_BankFreezer_Fridage] FOREIGN KEY([FrID])
REFERENCES [dbo].[Fridage] ([Fr_id])
GO
ALTER TABLE [dbo].[BankFreezer] CHECK CONSTRAINT [FK_BankFreezer_Fridage]
GO
ALTER TABLE [dbo].[BuildingEmployee]  WITH CHECK ADD  CONSTRAINT [FK_BuildingEmployee_Building] FOREIGN KEY([BuildID])
REFERENCES [dbo].[Building] ([id])
GO
ALTER TABLE [dbo].[BuildingEmployee] CHECK CONSTRAINT [FK_BuildingEmployee_Building]
GO
ALTER TABLE [dbo].[BuildingEmployee]  WITH CHECK ADD  CONSTRAINT [FK_BuildingEmployee_Employess] FOREIGN KEY([empID])
REFERENCES [dbo].[Employess] ([id])
GO
ALTER TABLE [dbo].[BuildingEmployee] CHECK CONSTRAINT [FK_BuildingEmployee_Employess]
GO
ALTER TABLE [dbo].[BuildingMatrial]  WITH CHECK ADD  CONSTRAINT [FK_BuildingMatrial_Building] FOREIGN KEY([BuildID])
REFERENCES [dbo].[Building] ([id])
GO
ALTER TABLE [dbo].[BuildingMatrial] CHECK CONSTRAINT [FK_BuildingMatrial_Building]
GO
ALTER TABLE [dbo].[BuildingMatrial]  WITH CHECK ADD  CONSTRAINT [FK_BuildingMatrial_Matriel] FOREIGN KEY([MatID])
REFERENCES [dbo].[Matriel] ([id])
GO
ALTER TABLE [dbo].[BuildingMatrial] CHECK CONSTRAINT [FK_BuildingMatrial_Matriel]
GO
ALTER TABLE [dbo].[ClientReserve]  WITH CHECK ADD  CONSTRAINT [FK_ClientReserve_Building] FOREIGN KEY([BuildID])
REFERENCES [dbo].[Building] ([id])
GO
ALTER TABLE [dbo].[ClientReserve] CHECK CONSTRAINT [FK_ClientReserve_Building]
GO
ALTER TABLE [dbo].[ClientReserve]  WITH CHECK ADD  CONSTRAINT [FK_ClientReserve_Reserve] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Reserve] ([id])
GO
ALTER TABLE [dbo].[ClientReserve] CHECK CONSTRAINT [FK_ClientReserve_Reserve]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_FreezerCeteogry] FOREIGN KEY([Cat_id])
REFERENCES [dbo].[FreezerCeteogry] ([id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_FreezerCeteogry]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Fridage] FOREIGN KEY([FrID])
REFERENCES [dbo].[Fridage] ([Fr_id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Fridage]
GO
ALTER TABLE [dbo].[NonWorks]  WITH CHECK ADD  CONSTRAINT [FK_NonWorks_Building] FOREIGN KEY([BuildID])
REFERENCES [dbo].[Building] ([id])
GO
ALTER TABLE [dbo].[NonWorks] CHECK CONSTRAINT [FK_NonWorks_Building]
GO
ALTER TABLE [dbo].[NonWorks]  WITH CHECK ADD  CONSTRAINT [FK_NonWorks_NonWorksTable] FOREIGN KEY([NonId])
REFERENCES [dbo].[NonWorksTable] ([Id])
GO
ALTER TABLE [dbo].[NonWorks] CHECK CONSTRAINT [FK_NonWorks_NonWorksTable]
GO
ALTER TABLE [dbo].[period]  WITH CHECK ADD  CONSTRAINT [FK_period_Fridage] FOREIGN KEY([FrID])
REFERENCES [dbo].[Fridage] ([Fr_id])
GO
ALTER TABLE [dbo].[period] CHECK CONSTRAINT [FK_period_Fridage]
GO
ALTER TABLE [dbo].[Reserve]  WITH CHECK ADD  CONSTRAINT [FK_Reserve_Building] FOREIGN KEY([BuildID])
REFERENCES [dbo].[Building] ([id])
GO
ALTER TABLE [dbo].[Reserve] CHECK CONSTRAINT [FK_Reserve_Building]
GO
ALTER TABLE [dbo].[Trader]  WITH CHECK ADD  CONSTRAINT [FK_Trader_FreezerCeteogry] FOREIGN KEY([cat_Id])
REFERENCES [dbo].[FreezerCeteogry] ([id])
GO
ALTER TABLE [dbo].[Trader] CHECK CONSTRAINT [FK_Trader_FreezerCeteogry]
GO
ALTER TABLE [dbo].[Trader]  WITH CHECK ADD  CONSTRAINT [FK_Trader_Fridage] FOREIGN KEY([FrID])
REFERENCES [dbo].[Fridage] ([Fr_id])
GO
ALTER TABLE [dbo].[Trader] CHECK CONSTRAINT [FK_Trader_Fridage]
GO
