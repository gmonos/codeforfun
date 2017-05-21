/****** Object:  Table [dbo].[Organization]    Script Date: 15/09/2016 19:10:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Organization](
	[OrganizationId] [int] NOT NULL,
	[RaisonSociale] [nvarchar](200) NULL,
	[Reference] [int] NULL,
	[Effectif] [int] NULL,
	[FormeJuridique] [nvarchar](50) NULL,
	[CodeNAF] [nvarchar](50) NULL,
	[IdentifiantConventionCollective] [nvarchar](50) NULL,
	[SIRET] [nvarchar](50) NULL,
	[AggregateId] [uniqueidentifier] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PM] PRIMARY KEY CLUSTERED 
(
	[PMId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


