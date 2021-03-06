USE [GSP]
GO
/****** Object:  Table [dbo].[Broker]    Script Date: 16/02/2022 20:40:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [dbo].[Users]    Script Date: 16/02/2022 20:40:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleId] [int] NOT NULL,
	[UserName] [varchar](70) NOT NULL,
	[PasswordHash] [varbinary](64) NOT NULL,
	[Salt] [uniqueidentifier] NULL,
	[FirstName] [varchar](60) NOT NULL,
	[LastName] [varchar](60) NOT NULL,
	[PhoneNumber] [varchar](25) NULL,
	[Token] [uniqueidentifier] NULL,
	[TokenDate] [datetime] NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_User_Active]  DEFAULT ((1)),
	[ActiveDate] [datetime] NULL,
	[InactiveDate] [datetime] NULL,
	[CurrentHerdCode] [varchar](8) NULL,
	[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_User_CreatedDateTime]  DEFAULT (getdate()),
	[LastModifiedDateTime] [datetime] NOT NULL CONSTRAINT [DF_User_LastModifiedDateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO


INSERT [dbo].[Users] ([UserRoleId], [UserName], [PasswordHash], [Salt], [FirstName], [LastName], [PhoneNumber], [Token], [TokenDate], [Active], [ActiveDate], [InactiveDate], [CurrentHerdCode], [CreatedDateTime], [LastModifiedDateTime]) VALUES (1, N'jalter_roberto@hotmail.com', 0xEE4E6C61163C01AE0AF63AE38211442F5CC9F2700D91E78BE6989655FC363621A924D0C3C2741254103AC309EFE9230DC7606E0D3DEE108637E9154C350A766E, N'9b55dccb-bf8f-4455-9c84-90835acad979', N'JALTER', N'SILVA', N'16997510595', NULL, NULL, 1, NULL, NULL, N'999', CAST(N'2021-12-12 21:00:35.347' AS DateTime), CAST(N'2021-12-12 21:00:35.347' AS DateTime))

CREATE TABLE [dbo].[Broker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CNPJ] [varchar](14) NOT NULL,
	[BrokerName] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF__Broker__STATUS__300424B4]  DEFAULT ((1)),
	[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF__Broker__CreatedD__30F848ED]  DEFAULT (getdate()),
	[LastModifiedDateTime] [datetime] NOT NULL CONSTRAINT [DF__Broker__LastModi__31EC6D26]  DEFAULT (getdate()),
 CONSTRAINT [PK__Broker__3214EC0723480716] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeviceType]    Script Date: 16/02/2022 20:40:41 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DeviceType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeviceTypeName] [varchar](30) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_DeviceType_Active]  DEFAULT ((1)),
	[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_DeviceType_CreatedDateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_DeviceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[UserLog]    Script Date: 16/02/2022 20:40:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserLogActionId] [int] NOT NULL,
	[DeviceTypeId] [int] NULL,
	[ActionDate] [datetime] NOT NULL CONSTRAINT [DF_UserLog_ActionDate]  DEFAULT (getdate()),
	[ApplicationVersion] [varchar](10) NULL,
	[DeviceName] [varchar](30) NULL,
	[DeviceVersion] [varchar](10) NULL,
	[DevicePlatform] [varchar](10) NULL,
	[DeviceLatitude] [decimal](9, 5) NULL,
	[DeviceLongitude] [decimal](9, 5) NULL,
	[IP] [varchar](20) NULL,
 CONSTRAINT [PK_UserLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserLogAction]    Script Date: 16/02/2022 20:40:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLogAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](20) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_UserLogAction_Active]  DEFAULT ((1)),
	[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_UserLogAction_CreatedDateTime]  DEFAULT (getdate()),
	[LastModifiedDateTime] [datetime] NOT NULL CONSTRAINT [DF_UserLogAction_LastModifiedDateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_UserLogAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 16/02/2022 20:40:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](100) NOT NULL,
	[Active] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDateTime] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

INSERT [dbo].[DeviceType] ([DeviceTypeName], [Active], [CreatedDateTime]) VALUES ( N'Web
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[DeviceType] ([DeviceTypeName], [Active], [CreatedDateTime]) VALUES ( N'Desktop
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[DeviceType] ([DeviceTypeName], [Active], [CreatedDateTime]) VALUES ( N'Mobile
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime))

INSERT [dbo].[UserLogAction] ([Description], [Active], [CreatedDateTime], [LastModifiedDateTime]) VALUES (N'Login
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[UserLogAction] ([Description], [Active], [CreatedDateTime], [LastModifiedDateTime]) VALUES (N'SignUp
', 1, CAST(N'2021-12-12 19:27:50.440' AS DateTime), CAST(N'2021-12-12 19:27:50.440' AS DateTime))
INSERT [dbo].[UserLogAction] ( [Description], [Active], [CreatedDateTime], [LastModifiedDateTime]) VALUES (N'Forgot Password
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[UserLogAction] ([Description], [Active], [CreatedDateTime], [LastModifiedDateTime]) VALUES (N'Changed Password
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[UserLogAction] ([Description], [Active], [CreatedDateTime], [LastModifiedDateTime]) VALUES (N'Activate Account
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[UserLogAction] ([Description], [Active], [CreatedDateTime], [LastModifiedDateTime]) VALUES (N'Verify pass token
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[UserLogAction] ( [Description], [Active], [CreatedDateTime], [LastModifiedDateTime]) VALUES (N'Created User
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[UserLogAction] ([Description], [Active], [CreatedDateTime], [LastModifiedDateTime]) VALUES (N'User Registered
', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime), CAST(N'2021-01-01 00:00:00.000' AS DateTime))

INSERT [dbo].[UserRole] ([RoleName], [Active], [CreatedDateTime]) VALUES ( N'Administrator', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[UserRole] ([RoleName], [Active], [CreatedDateTime]) VALUES ( N'Manager', 1, CAST(N'2021-01-01 00:00:00.000' AS DateTime))


ALTER TABLE [dbo].[UserLog]  WITH CHECK ADD  CONSTRAINT [FK_UserLog_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserLog] CHECK CONSTRAINT [FK_UserLog_User]
GO
ALTER TABLE [dbo].[UserLog]  WITH CHECK ADD  CONSTRAINT [FK_UserLog_UserLog] FOREIGN KEY([DeviceTypeId])
REFERENCES [dbo].[DeviceType] ([Id])
GO
ALTER TABLE [dbo].[UserLog] CHECK CONSTRAINT [FK_UserLog_UserLog]
GO
ALTER TABLE [dbo].[UserLog]  WITH CHECK ADD  CONSTRAINT [FK_UserLog_UserLogAction] FOREIGN KEY([UserLogActionId])
REFERENCES [dbo].[UserLogAction] ([Id])
GO
ALTER TABLE [dbo].[UserLog] CHECK CONSTRAINT [FK_UserLog_UserLogAction]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Users] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_UserRole_Users]
GO



