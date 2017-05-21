create schema Organization

CREATE TABLE [Organization].[Address] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [CategorieAdresse]  INT            NOT NULL,
    [NumVoie]           NVARCHAR (MAX) NULL,
    [NomVoie]           NVARCHAR (MAX) NULL,
    [ComplementVoie]    NVARCHAR (MAX) NULL,
    [CodePostal]        NVARCHAR (MAX) NULL,
    [Ville]             INT            NOT NULL,
    [Pays]              INT            NOT NULL,
    [TypeVoie]          INT            NOT NULL,
    [Organization_Id] INT            NULL,
	[AggregateId]                     UNIQUEIDENTIFIER NOT NULL,
    [Version]                         INT              NOT NULL,
    CONSTRAINT [PK_dbo.Address] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [Organization].[Address].[IX_Organization_Id]...';


GO
CREATE NONCLUSTERED INDEX [IX_Organization_Id]
    ON [Organization].[Address]([Organization_Id] ASC);


GO
PRINT N'Creating [Organization].[Iban]...';


GO
CREATE TABLE [Organization].[Iban] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Titulaire]         NVARCHAR (MAX) NULL,
    [IBAN]              NVARCHAR (MAX) NULL,
    [BIC]               NVARCHAR (MAX) NULL,
    [Banque]            INT            NOT NULL,
    [Organization_Id] INT            NULL,
	[AggregateId]                     UNIQUEIDENTIFIER NOT NULL,
    [Version]                         INT              NOT NULL,
    CONSTRAINT [PK_dbo.Iban] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [Organization].[Iban].[IX_Organization_Id]...';


GO
CREATE NONCLUSTERED INDEX [IX_Organization_Id]
    ON [Organization].[Iban]([Organization_Id] ASC);


GO
PRINT N'Creating [Organization].[Organization]...';


GO
CREATE TABLE [Organization].[Organization] (
    [Id]                              INT              IDENTITY (1, 1) NOT NULL,
    [Reference]                       NVARCHAR (MAX)   NULL,
    [RaisonSociale]                   NVARCHAR (MAX)   NULL,
    [FormeJuridique]                  NVARCHAR (MAX)   NULL,
    [Effectif]                        INT              NULL,
    [SIRET]                           NVARCHAR (MAX)   NULL,
    [CodeNAF]                         NVARCHAR (MAX)   NULL,
    [IdentifiantConventionCollective] NVARCHAR (MAX)   NULL,
    [Activite]                        NVARCHAR (MAX)   NULL,
    [Telephone1]                      NVARCHAR (MAX)   NULL,
    [Telephone2]                      NVARCHAR (MAX)   NULL,
    [NumeroFax]                       NVARCHAR (MAX)   NULL,
    [Email]                           NVARCHAR (MAX)   NULL,
    [SiteWeb]                         NVARCHAR (MAX)   NULL,
    [AggregateId]                     UNIQUEIDENTIFIER NOT NULL,
    [Version]                         INT              NOT NULL,
    CONSTRAINT [PK_dbo.Organization] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [Organization].[FK_dbo.Address_dbo.Organization_Organization_Id]...';


GO
ALTER TABLE [Organization].[Address] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Address_dbo.Organization_Organization_Id] FOREIGN KEY ([Organization_Id]) REFERENCES [Organization].[Organization] ([Id]);


GO
PRINT N'Creating [Organization].[FK_dbo.Iban_dbo.Organization_Organization_Id]...';


GO
ALTER TABLE [Organization].[Iban] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Iban_dbo.Organization_Organization_Id] FOREIGN KEY ([Organization_Id]) REFERENCES [Organization].[Organization] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';




GO
ALTER TABLE [Organization].[Address] WITH CHECK CHECK CONSTRAINT [FK_dbo.Address_dbo.Organization_Organization_Id];

ALTER TABLE [Organization].[Iban] WITH CHECK CHECK CONSTRAINT [FK_dbo.Iban_dbo.Organization_Organization_Id];


GO