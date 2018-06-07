
/****** Object:  Sequence [dbo].[accountseq]    Script Date: 6/7/2018 5:56:54 AM ******/
CREATE SEQUENCE [dbo].[accountseq] 
 AS [bigint]
 START WITH 21
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE  100 
GO
/****** Object:  Sequence [dbo].[customerseq]    Script Date: 6/7/2018 5:56:54 AM ******/
CREATE SEQUENCE [dbo].[customerseq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE  100 
GO
/****** Object:  Sequence [dbo].[transactionseq]    Script Date: 6/7/2018 5:56:54 AM ******/
CREATE SEQUENCE [dbo].[transactionseq] 
 AS [int]
 START WITH 2
 INCREMENT BY 1
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE  100 
GO
/****** Object:  Table [dbo].[Account]    Script Date: 6/7/2018 5:56:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountNo] [int] NOT NULL,
	[AccountName] [nvarchar](128) NOT NULL,
	[Balance] [money] NULL,
	[Notes] [ntext] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[AccountNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 6/7/2018 5:56:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[ContactInfo] [nvarchar](256) NULL,
	[Notes] [ntext] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_CUSTOMER] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerAccount]    Script Date: 6/7/2018 5:56:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAccount](
	[CustomerId] [int] NOT NULL,
	[AccountNo] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 6/7/2018 5:56:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionId] [int] NOT NULL,
	[PostedOn] [datetime2](7) NOT NULL,
	[Amount] [money] NOT NULL,
	[Notes] [ntext] NULL,
	[AccountFrom] [int] NULL,
	[AccountTo] [int] NULL,
 CONSTRAINT [PK_TRANSACTION] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Account] ([AccountNo], [AccountName], [Balance], [Notes], [IsActive]) VALUES (21, N'Robert Ford', 136056.0000, NULL, 1)
GO
INSERT [dbo].[Account] ([AccountNo], [AccountName], [Balance], [Notes], [IsActive]) VALUES (22, N'Bernard A', 15956.0000, NULL, 1)
GO
INSERT [dbo].[Account] ([AccountNo], [AccountName], [Balance], [Notes], [IsActive]) VALUES (23, N'Delos Inc.', 25000000.0000, NULL, 1)
GO
INSERT [dbo].[Customer] ([CustomerId], [FirstName], [LastName], [ContactInfo], [Notes], [IsActive]) VALUES (1, N'John', N'Bernard', N'7th Avenue|213213123|123123312', NULL, 1)
GO
INSERT [dbo].[Customer] ([CustomerId], [FirstName], [LastName], [ContactInfo], [Notes], [IsActive]) VALUES (2, N'Robert', N'Ford', N'Some Contect Info|224124214', NULL, 1)
GO
INSERT [dbo].[CustomerAccount] ([CustomerId], [AccountNo]) VALUES (1, 21)
GO
INSERT [dbo].[CustomerAccount] ([CustomerId], [AccountNo]) VALUES (2, 22)
GO
INSERT [dbo].[CustomerAccount] ([CustomerId], [AccountNo]) VALUES (1, 23)
GO
INSERT [dbo].[CustomerAccount] ([CustomerId], [AccountNo]) VALUES (2, 23)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (0, CAST(N'2018-06-07T02:57:17.3328403' AS DateTime2), 18000.0000, N'hello', NULL, 22)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (2, CAST(N'2018-06-07T03:19:31.2037512' AS DateTime2), -100.0000, N'hello', 22, NULL)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (3, CAST(N'2018-06-07T03:19:48.2525156' AS DateTime2), -100.0000, N'hello', 22, NULL)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (4, CAST(N'2018-06-07T03:19:52.4371719' AS DateTime2), -100.0000, N'hello', 22, NULL)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (5, CAST(N'2018-06-07T03:19:56.4616562' AS DateTime2), -100.0000, N'hello', 22, NULL)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (6, CAST(N'2018-06-07T03:23:00.5209173' AS DateTime2), 1000.0000, N'', 22, 21)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (7, CAST(N'2018-06-07T03:23:37.1998911' AS DateTime2), 1000.0000, N'', 22, 21)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (8, CAST(N'2018-06-07T03:23:50.7843319' AS DateTime2), 1000.0000, N'', 22, 21)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (9, CAST(N'2018-06-07T03:24:48.6587117' AS DateTime2), 100.0000, N'', 21, 22)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (10, CAST(N'2018-06-07T04:13:39.7027093' AS DateTime2), 100.0000, N'', 21, 22)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (11, CAST(N'2018-06-07T04:35:41.0931426' AS DateTime2), -100.0000, N'hello', 22, NULL)
GO
INSERT [dbo].[Transaction] ([TransactionId], [PostedOn], [Amount], [Notes], [AccountFrom], [AccountTo]) VALUES (12, CAST(N'2018-06-07T04:36:06.0443644' AS DateTime2), 100000.0000, N'hello', NULL, 21)
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Balance__25869641]  DEFAULT ('0.0') FOR [Balance]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__IsActi__38996AB5]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF__Transacti__Amoun__286302EC]  DEFAULT ('0.0') FOR [Amount]
GO
ALTER TABLE [dbo].[CustomerAccount]  WITH CHECK ADD  CONSTRAINT [CustomerAccount_fk0] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CustomerAccount] CHECK CONSTRAINT [CustomerAccount_fk0]
GO
ALTER TABLE [dbo].[CustomerAccount]  WITH CHECK ADD  CONSTRAINT [CustomerAccount_fk1] FOREIGN KEY([AccountNo])
REFERENCES [dbo].[Account] ([AccountNo])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CustomerAccount] CHECK CONSTRAINT [CustomerAccount_fk1]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([AccountFrom])
REFERENCES [dbo].[Account] ([AccountNo])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([AccountTo])
REFERENCES [dbo].[Account] ([AccountNo])
GO

