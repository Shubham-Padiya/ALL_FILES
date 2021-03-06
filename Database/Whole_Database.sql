USE [master]
GO
/****** Object:  Database [Database]    Script Date: 21-01-2022 14:46:18 ******/
CREATE DATABASE [Database]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Database', FILENAME = N'D:\tatvasoft\folder\Database\Database.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Database_log', FILENAME = N'D:\tatvasoft\folder\Database\Database_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Database] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Database].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Database] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Database] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Database] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Database] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Database] SET ARITHABORT OFF 
GO
ALTER DATABASE [Database] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Database] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Database] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Database] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Database] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Database] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Database] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Database] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Database] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Database] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Database] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Database] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Database] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Database] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Database] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Database] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Database] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Database] SET RECOVERY FULL 
GO
ALTER DATABASE [Database] SET  MULTI_USER 
GO
ALTER DATABASE [Database] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Database] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Database] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Database] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Database] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Database] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Database', N'ON'
GO
ALTER DATABASE [Database] SET QUERY_STORE = OFF
GO
USE [Database]
GO
/****** Object:  Table [dbo].[AddressOfCustomer]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressOfCustomer](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[StreetName] [varchar](80) NOT NULL,
	[HouseNo] [int] NOT NULL,
	[City] [varchar](50) NOT NULL,
	[PostalCode] [char](6) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlockedCustomerBySP]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockedCustomerBySP](
	[CustBlockedBySPID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CancelledService]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CancelledService](
	[ServiceRequestID] [int] NOT NULL,
	[Reason] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer_language]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_language](
	[UserID] [int] NOT NULL,
	[Languages] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustToSPRating]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustToSPRating](
	[CustomerID] [int] NOT NULL,
	[SPID] [int] NOT NULL,
	[OnTimeArrival] [int] NOT NULL,
	[Friendly] [int] NOT NULL,
	[Quality] [int] NOT NULL,
	[Feedback] [varchar](100) NULL,
	[Rating] [decimal](18, 0) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailsOfUser]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailsOfUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[First_Name] [varchar](50) NOT NULL,
	[Last_Name] [varchar](50) NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[MobileNumber] [char](10) NULL,
	[DateOfBirth] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FavoritSP]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavoritSP](
	[UserID] [int] NOT NULL,
	[Fav_SP_ID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GetInTouch]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GetInTouch](
	[GetInTouchID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[MobileNo] [char](10) NULL,
	[Email] [varchar](30) NOT NULL,
	[Subjects] [varchar](30) NOT NULL,
	[Messages] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GetInTouchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GetNewsLetter]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GetNewsLetter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceProviderRating]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceProviderRating](
	[SPID] [int] NOT NULL,
	[SPRating] [decimal](18, 0) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceSchedual]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceSchedual](
	[ServiceRequestID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[ServiceDate] [date] NOT NULL,
	[ServiceTime] [time](7) NOT NULL,
	[Comments] [varchar](100) NULL,
	[HasPet] [bit] NOT NULL,
	[AcceptedBySP] [int] NULL,
	[PaymentAmount] [decimal](18, 0) NOT NULL,
	[ServiceStatus] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SPMoreDetails]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SPMoreDetails](
	[UserID] [int] NOT NULL,
	[Nationality] [varchar](20) NULL,
	[Gender] [varchar](10) NOT NULL,
	[AccountStatus] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfRole]    Script Date: 21-01-2022 14:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfRole](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AddressOfCustomer]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[BlockedCustomerBySP]  WITH CHECK ADD FOREIGN KEY([CustBlockedBySPID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[BlockedCustomerBySP]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[CancelledService]  WITH CHECK ADD FOREIGN KEY([ServiceRequestID])
REFERENCES [dbo].[ServiceSchedual] ([ServiceRequestID])
GO
ALTER TABLE [dbo].[Customer_language]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[CustToSPRating]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[CustToSPRating]  WITH CHECK ADD FOREIGN KEY([SPID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[DetailsOfUser]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[TypeOfRole] ([RoleID])
GO
ALTER TABLE [dbo].[FavoritSP]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[FavoritSP]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[ServiceProviderRating]  WITH CHECK ADD FOREIGN KEY([SPID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[ServiceSchedual]  WITH CHECK ADD FOREIGN KEY([AcceptedBySP])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[ServiceSchedual]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
ALTER TABLE [dbo].[SPMoreDetails]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[DetailsOfUser] ([UserID])
GO
USE [master]
GO
ALTER DATABASE [Database] SET  READ_WRITE 
GO
