USE [PM]
GO

/****** Object:  Table [dbo].[IBAN]    Script Date: 08/09/2016 09:47:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IBAN](
	[IbanId] [int] NOT NULL,
	[OrganizationId] [int] NULL,
	[TitulaireCompte] [nvarchar](50) NULL,
	[IBAN] [nvarchar](50) NULL,
	[BIC] [nvarchar](50) NULL,
	[Banque] [nvarchar](50) NULL,
	[EtatduCompte] [nvarchar](50) NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_IBAN] PRIMARY KEY CLUSTERED 
(
	[IbanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[IBAN]  WITH CHECK ADD  CONSTRAINT [FK_IBAN_PM] FOREIGN KEY([PMId])
REFERENCES [dbo].[PM] ([PMId])
GO

ALTER TABLE [dbo].[IBAN] CHECK CONSTRAINT [FK_IBAN_PM]
GO


