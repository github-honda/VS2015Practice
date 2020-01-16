USE [DBTemplate]
GO

SET IDENTITY_INSERT [dbo].[TConfig] ON 

delete TConfig where 1=1

INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (0, NULL, NULL, N'KeyList', N'代碼清單', NULL, 1, N'FKey for 程式控制.'+CHAR(13)+CHAR(10)+'FValue for 主要語言.'+CHAR(13)+CHAR(10)+'FValueB for 次要語言或匯入語言.', CAST(N'2019-11-22 10:42:31.660' AS DateTime), CAST(N'2019-11-22 10:42:31.660' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1000, 0, NULL, N'Gender', N'性別', NULL, NULL, N'Male 中文為男,'+CHAR(13)+CHAR(10)+'Female 中文是女.', CAST(N'2019-11-22 10:42:31.660' AS DateTime), CAST(N'2019-12-08 17:17:27.553' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1001, 1000, 10, N'M', N'男', N'Male', NULL, NULL, CAST(N'2019-11-22 10:43:00.870' AS DateTime), CAST(N'2019-12-08 17:20:05.107' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1002, 1000, 20, N'F', N'女', N'Female', NULL, NULL, CAST(N'2019-11-22 10:43:10.233' AS DateTime), CAST(N'2019-11-22 10:43:10.233' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1003, 0, NULL, N'Marriage', N'婚姻', NULL, NULL, N'婚姻狀態除了單身和已婚以外,'+CHAR(13)+CHAR(10)+'還可以細分為單身未婚、單身已婚.', CAST(N'2019-11-22 10:44:25.577' AS DateTime), CAST(N'2019-12-08 17:19:50.053' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1004, 1003, 10, N'Single', N'單身', NULL, NULL, NULL, CAST(N'2019-11-22 10:44:55.327' AS DateTime), CAST(N'2019-11-22 10:44:55.327' AS DateTime))
INSERT [dbo].[TConfig] ([FID], [FParentID], [FSeqNo], [FKey], [FValue], [FValueB], [FReadonly], [FNote], [FCreateTime], [FUpdateTime]) VALUES (1005, 1003, 20, N'Married', N'已婚', NULL, NULL, NULL, CAST(N'2019-11-22 10:45:22.400' AS DateTime), CAST(N'2019-11-22 10:45:22.400' AS DateTime))
SET IDENTITY_INSERT [dbo].[TConfig] OFF

DBCC CHECKIDENT(TConfig, RESEED, 1005)


GO
