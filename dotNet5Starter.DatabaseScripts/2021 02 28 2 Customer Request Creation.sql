USE [dotNet5Starter]
GO

/****** Object:  Table [dbo].[CustomerReqests]    Script Date: 28/02/2021 20:40:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerReqests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[Timestamp] [datetime] NOT NULL,
	[ReqestTitle] [nvarchar](50) NULL,
	[RequestText] [text] NULL,
 CONSTRAINT [PK_CustomerReqests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[CustomerReqests]  WITH CHECK ADD  CONSTRAINT [FK_CustomerReqests_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO

ALTER TABLE [dbo].[CustomerReqests] CHECK CONSTRAINT [FK_CustomerReqests_Companies]
GO

ALTER TABLE [dbo].[CustomerReqests]  WITH CHECK ADD  CONSTRAINT [FK_CustomerReqests_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO

ALTER TABLE [dbo].[CustomerReqests] CHECK CONSTRAINT [FK_CustomerReqests_Customers]
GO


