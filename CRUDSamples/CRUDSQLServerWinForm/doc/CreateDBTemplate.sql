USE [master]
GO
/****** Object:  Database [DBTemplate]    Script Date: 2020/1/16 下午 01:56:14 ******/
CREATE DATABASE [DBTemplate]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBTemplate', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DBTemplate.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBTemplate_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DBTemplate_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBTemplate] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBTemplate].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBTemplate] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBTemplate] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBTemplate] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBTemplate] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBTemplate] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBTemplate] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBTemplate] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DBTemplate] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBTemplate] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBTemplate] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBTemplate] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBTemplate] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBTemplate] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBTemplate] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBTemplate] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBTemplate] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBTemplate] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBTemplate] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBTemplate] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBTemplate] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBTemplate] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBTemplate] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBTemplate] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBTemplate] SET RECOVERY FULL 
GO
ALTER DATABASE [DBTemplate] SET  MULTI_USER 
GO
ALTER DATABASE [DBTemplate] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBTemplate] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBTemplate] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBTemplate] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DBTemplate]
GO
/****** Object:  User [W10T470\honda]    Script Date: 2020/1/16 下午 01:56:14 ******/
CREATE USER [W10T470\honda] FOR LOGIN [W10T470\honda] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [W10T470\honda]
GO
/****** Object:  Table [dbo].[TConfig]    Script Date: 2020/1/16 下午 01:56:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TConfig](
	[FID] [int] IDENTITY(1000,1) NOT NULL,
	[FParentID] [int] NULL,
	[FSeqNo] [int] NULL,
	[FKey] [nvarchar](256) NOT NULL,
	[FValue] [nvarchar](max) NULL,
	[FValueB] [nvarchar](max) NULL,
	[FReadonly] [int] NULL,
	[FNote] [nvarchar](max) NULL,
	[FCreateTime] [datetime] NOT NULL CONSTRAINT [DF_TConfig_FCreateTime]  DEFAULT (getdate()),
	[FUpdateTime] [datetime] NOT NULL CONSTRAINT [DF_TConfig_FUpdateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_TConfig] PRIMARY KEY CLUSTERED 
(
	[FID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[TConfig] ON 

/*

use InsertData.sql instead:

INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (0, NULL, NULL, N'KeyList', N'代碼清單', NULL, 1, N'FKey for 程式控制.
FValue for 主要語言.
FValueB for 次要語言或匯入語言.', CAST(N'2019-11-22 10:42:31.660' AS DateTime), CAST(N'2019-11-22 10:42:31.660' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1000, 0, NULL, N'Gender', N'性別', NULL, NULL, N'Male 中文為男,
Female 中文是女.', CAST(N'2019-11-22 10:42:31.660' AS DateTime), CAST(N'2019-12-08 17:17:27.553' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1001, 1000, 10, N'M', N'男', N'Male', NULL, NULL, CAST(N'2019-11-22 10:43:00.870' AS DateTime), CAST(N'2019-12-08 17:20:05.107' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1002, 1000, 20, N'F', N'女', N'Female', NULL, NULL, CAST(N'2019-11-22 10:43:10.233' AS DateTime), CAST(N'2019-11-22 10:43:10.233' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1003, 0, NULL, N'Marriage', N'婚姻', NULL, NULL, N'婚姻狀態除了單身和已婚以外,
還可以細分為單身未婚、單身已婚.', CAST(N'2019-11-22 10:44:25.577' AS DateTime), CAST(N'2019-12-08 17:19:50.053' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1004, 1003, 10, N'Single', N'單身', NULL, NULL, NULL, CAST(N'2019-11-22 10:44:55.327' AS DateTime), CAST(N'2019-11-22 10:44:55.327' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1005, 1003, 20, N'Married', N'已婚', NULL, NULL, NULL, CAST(N'2019-11-22 10:45:22.400' AS DateTime), CAST(N'2019-11-22 10:45:22.400' AS DateTime))

*/


SET IDENTITY_INSERT [dbo].[TConfig] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TConfig]    Script Date: 2020/1/16 下午 01:56:14 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TConfig] ON [dbo].[TConfig]
(
	[FParentID] ASC,
	[FKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FID and FKey are unique equal for coding.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TConfig', @level2type=N'COLUMN',@level2name=N'FID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FID and FKey are unique equal for coding.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TConfig', @level2type=N'COLUMN',@level2name=N'FKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bilingual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TConfig', @level2type=N'COLUMN',@level2name=N'FValueB'
GO
USE [master]
GO
ALTER DATABASE [DBTemplate] SET  READ_WRITE 
GO
