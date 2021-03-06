USE [master]
GO
/****** Object:  Database [GP_EJERCICIO]    Script Date: 1/02/2022 02:01:04 ******/
CREATE DATABASE [GP_EJERCICIO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GP_EJERCICIO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MANUMAN\MSSQL\DATA\GP_EJERCICIO.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GP_EJERCICIO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MANUMAN\MSSQL\DATA\GP_EJERCICIO_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GP_EJERCICIO] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GP_EJERCICIO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GP_EJERCICIO] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET ARITHABORT OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GP_EJERCICIO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GP_EJERCICIO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GP_EJERCICIO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GP_EJERCICIO] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GP_EJERCICIO] SET  MULTI_USER 
GO
ALTER DATABASE [GP_EJERCICIO] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GP_EJERCICIO] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GP_EJERCICIO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GP_EJERCICIO] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [GP_EJERCICIO] SET DELAYED_DURABILITY = DISABLED 
GO
USE [GP_EJERCICIO]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 1/02/2022 02:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 1/02/2022 02:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [numeric](18, 2) NULL,
	[Price] [numeric](18, 2) NULL,
	[Iva] [numeric](18, 2) NULL,
	[SubTotal] [numeric](18, 2) NULL,
	[Total] [numeric](18, 2) NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 1/02/2022 02:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Iva] [decimal](18, 2) NULL,
	[SubTotal] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
	[ClientId] [int] NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/02/2022 02:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Price] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Invoices]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([id])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Products]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Clients]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Clients' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients', @level2type=N'COLUMN',@level2name=N'id'
GO
USE [master]
GO
ALTER DATABASE [GP_EJERCICIO] SET  READ_WRITE 
GO
