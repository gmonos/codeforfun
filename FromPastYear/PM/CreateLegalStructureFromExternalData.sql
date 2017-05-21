
/****** Object:  StoredProcedure [dbo].[CreateLegalStructureFromExternalData]    Script Date: 16/09/2016 11:20:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Christophe MALOUNGILA
-- Create date: 16/09/2016
-- Description:	This stored procedure allows the creation of one Legal Structure 
-- =============================================
CREATE PROCEDURE [dbo].[CreateLegalStructureFromExternalData]

	@RaisonSociale nvarchar(255),
	@Reference nvarchar(255) OUTPUT,
	@PersonMoraleId int OUTPUT 
AS
BEGIN

-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @LegalStructureId bigint;
DECLARE @Adresse1Id bigint;
DECLARE @BankAccountId bigint;
DECLARE @AdresseUsageId bigint;
DECLARE @Adresse2Id bigint;
DECLARE @InsuranceClientId bigint;
DECLARE @ReferenceLegalStructure bigint;
DECLARE @ReferenceInsuranceClient bigint;


SELECT @LegalStructureId = NEXT VALUE FOR CoreLEGALSTRUCTURE
SELECT @Adresse1Id = NEXT VALUE FOR CoreADDRESS
SELECT @BankAccountId = NEXT VALUE FOR CoreBANKACCOUNT
SELECT @AdresseUsageId = NEXT VALUE FOR CoreADDRESSUSAGE
SELECT @Adresse2Id = NEXT VALUE FOR CoreADDRESS
SELECT @InsuranceClientId = NEXT VALUE FOR CoreINSURANCECLIENT
SELECT @ReferenceLegalStructure = NEXT VALUE FOR CoreReferenceBuilderrd1PERPerson
SELECT @ReferenceInsuranceClient = NEXT VALUE FOR CoreReferenceBuilderrd3CNTInsuranceClient

SET @Reference = concat('0',@ReferenceLegalStructure)
SET @PersonMoraleId = @LegalStructureId

/* LEGAL STRUCTURE
*/
INSERT INTO [dbo].[LEGALSTRUCTURE]
           ([ID],[CLASSID],[ACTIVITYDESCRIPTION], [CAPITAL_CURRENCY], [CAPITAL_QUANTITY], [NAFCODE],[COMMERCIALNAME],[NATIONALITY],[EMPLOYECOUNT]
           ,[ADDRESSUSAGES_COUNT],[CONTACTS_COUNT],[BANKPROFILES_COUNT],[NATIONALPOTENTIALNUMBER],[POTENTIALNUMBER],[CAPITALSTATUS]
           ,[REFERENCE],[ROLE],[SENSITIVITY],[SIRETNBR],[TURNOVER_CURRENCY],[TURNOVER_QUANTITY],[TO_DELETE_2]
           ,[IDCC],[COMPANYORGANIZATIONALUNIT_ID],[COMPANYORGANIZATIONALUNIT_CLASSID],[COMPANYORGANIZATIONALUNIT_VERSION]
           ,[ADDRESS_ID],[ADDRESS_CLASSID],[ADDRESS_VERSION],[PRESCRIPTEUR_ID],[PRESCRIPTEUR_CLASSID],[PRESCRIPTEUR_VERSION]
           ,[USER_CLASSID],[USER_ID],[USER_VERSION],[MYCONCURRENTPRODUCTS_COUNT],[FILIALES_COUNT],[ISDELETED]
           ,[NAME],[VERSION],[LEGALSTRUCTUREHEADQUARTER_ID],[LEGALSTRUCTUREHEADQUARTER_CLASSID],[LEGALSTRUCTUREHEADQUARTER_VERSION]
           ,[BEGINDATE],[CREATIONDATE],[MODIFICATION_DATE])
     VALUES
           (@LegalStructureId,318770897,'Assurance','EUR',0,'12345',@RaisonSociale,0,'30',1,0,1,0,0,0,concat('0',@ReferenceLegalStructure),0,0,
		   '99999998887891','EUR',0,0,'1233',0,0,0,@Adresse1Id,318770775,-1,0,0,0,0,0,0,0,0,0,@RaisonSociale
           ,2147483647
           ,0
           ,0
           ,0
           ,null
           ,GetDate()
           ,GETDATE())

/* ADDRESS
*/

INSERT INTO [dbo].[ADDRESS]
			([ID], [CLASSID], [WAYNAME], [CODEINSEE], [CITYDESCRIPTION_CITY],
			 [CITYDESCRIPTION_ZIPCODE], [COMPLEMENT], [NUMBER], [WAYTYPE], [COUNTRY],
			  [CITYDESCRIPTION_DISTRIBUTORDESK], [CEDEX], [CODETELEPHONIQUEPAYS], [NUMEROFAX],
			   [NUMEROMOBILE], [NUMEROTEL], [CITYDESCRIPTION_DEPARTMENTORPROVINCE], [STATUS],
			    [ISDELETED], [NAME], [VERSION], [MODIFICATION_DATE])
	VALUES 
			(@Adresse1Id, 318770775, 'RUE SEVRE 2', null, 'SEVRES', '92310', null, null, 
			null, '0', null, null, null, null, null, null, null, 0, 0,'', 2147483647, GetDate())

/* INSURANCE CLIENT
*/
INSERT INTO [dbo].[INSURANCECLIENT]
			([ID], [CLASSID], [VIPDOMAIN], [REFERENCE], [NAME], [PERSON_ID], [PERSON_CLASSID], 
				[PERSON_VERSION], [ISDELETED], [VERSION], [VIPCLIENT], [MODIFICATION_DATE])
	VALUES 
			(@InsuranceClientId, 325322227, null,concat('000',@ReferenceInsuranceClient), @RaisonSociale, @LegalStructureId, 318770897, -1, 0, 2147483647, 0, GETDATE())



/* ADDRESS 2 
*/

INSERT INTO [dbo].[ADDRESS]
			([ID], [CLASSID], [WAYNAME], [CODEINSEE], [CITYDESCRIPTION_CITY],
			 [CITYDESCRIPTION_ZIPCODE], [COMPLEMENT], [NUMBER], [WAYTYPE], [COUNTRY],
			  [CITYDESCRIPTION_DISTRIBUTORDESK], [CEDEX], [CODETELEPHONIQUEPAYS], [NUMEROFAX],
			   [NUMEROMOBILE], [NUMEROTEL], [CITYDESCRIPTION_DEPARTMENTORPROVINCE], [STATUS],
			    [ISDELETED], [NAME], [VERSION], [MODIFICATION_DATE])
	VALUES 
			(@Adresse2Id, 318770775, 'RUE SEVRE 2', null, 'SEVRES', '92310', null, null, 
			null, '0', null, null, null, null, null, null, null, 0, 0,'', 2147483647, GetDate())

/* ADDRESS USAGE
*/

INSERT INTO  [dbo].[ADDRESSUSAGE]
			([ID], [CLASSID], [USAGE], [FULLNAME], [ADDRESS_ID], [ADDRESS_CLASSID], 
				[ADDRESS_VERSION], [ISDELETED], [NAME], [VERSION], [LEGALSTRUCTURE_ID],
				 [LEGALSTRUCTURE_CLASSID], [LEGALSTRUCTURE_VERSION], [VALIDFROMDATE], [VALIDTODATE], [MODIFICATION_DATE])
	VALUES 
			(@AdresseUsageId, 318770765, 2, null, @Adresse2Id, 318770775, -1, 0, '', 2147483647, @LegalStructureId, 318770897, 2147483647, null, null, GetDate())

/* BANK ACOUNT
*/

INSERT INTO [dbo].[BANKACCOUNT]
			([ID], [CLASSID], [BANKCODE], [ACCOUNTNUMBER], [RIBBANKCODE], [RIBBRANCHECODE], [RIBNUMBER], [ACCOUNTTYPE], 
			[BANKLABEL], [BANKCOUNTRY], [PAYMENTMEANS], [BANKACCOUNTTYPE], [ROUTINGNUMBER], [COUNTRYCODE], [CONTROLKEY], 
			[BBAN], [BIC], [RUM], [BANKACCOUNTSTATUS], [TYPEENVOIE], [BANKADDRESS_ID], [BANKADDRESS_CLASSID], [BANKADDRESS_VERSION], 
			[ISDELETED], [NAME], [VERSION], [SENDINGDATE], [CREATIONDATE], [CANCELLATIONDATE], [MODIFICATION_DATE])
	VALUES 
			(@BankAccountId, 318774567, null, null, null, null, null, 0, null, '', 0, 4, null, null, null, null, null, null, 0, 0, 0, 0, 0,
			 0, '', 2147483647, null, null, null, GETDATE())

/* PERSONBASE_PAYMENTMETHOD
*/
INSERT INTO  [dbo].[PERSONBASE_PAYMENTMETHOD]
			([Id], [ListOrder], [LOId], [LOCLASSID], [CLASSID], [Version])
	VALUES  
			(@BankAccountId, 0, @LegalStructureId, 318770897, 318774567, -1)

END

GO


