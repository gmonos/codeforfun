USE [PM]
GO

/****** Object:  Table [dbo].[Adresse]    Script Date: 08/09/2016 09:47:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Adresse](
	[AddressId] [int] NOT NULL,
	[OrganizationId] [int] NULL,
	[NomVoie] [nvarchar](50) NULL,
	[NumVoie] [nvarchar](50) NULL,
	[ComplementVoie] [nvarchar](50) NULL,
	[CodePostal] [nvarchar](5) NULL,
	[Ville] [nvarchar](50) NULL,
	[Pays] [nvarchar](50) NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Adresse] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Adresse]  WITH CHECK ADD  CONSTRAINT [FK_Adresse_PM] FOREIGN KEY([PMId])
REFERENCES [dbo].[PM] ([PMId])
GO

ALTER TABLE [dbo].[Adresse] CHECK CONSTRAINT [FK_Adresse_PM]
GO


