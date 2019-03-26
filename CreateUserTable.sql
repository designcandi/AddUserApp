USE [TestAddUser]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 26/03/2019 09:54:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Salt] [varchar](50) NULL
) ON [PRIMARY]
GO


