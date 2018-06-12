USE [master]
GO

/****** Object:  Database [DXCOrdersDB]    Script Date: 06/04/18 09:40:02 ******/
CREATE DATABASE [DXCOrdersDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DXCOrdersDB_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DXCOrdersDB_Data.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'DXCOrdersDB_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DXCOrdersDB_Log.ldf' , SIZE = 3072KB , MAXSIZE = 2048GB , FILEGROWTH = 1024KB )
GO

ALTER DATABASE [DXCOrdersDB] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DXCOrdersDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [DXCOrdersDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [DXCOrdersDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [DXCOrdersDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET  ENABLE_BROKER 
GO

ALTER DATABASE [DXCOrdersDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [DXCOrdersDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET RECOVERY FULL 
GO

ALTER DATABASE [DXCOrdersDB] SET  MULTI_USER 
GO

ALTER DATABASE [DXCOrdersDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [DXCOrdersDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [DXCOrdersDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [DXCOrdersDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [DXCOrdersDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [DXCOrdersDB] SET QUERY_STORE = OFF
GO

USE [DXCOrdersDB]
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [DXCOrdersDB] SET  READ_WRITE 
GO


