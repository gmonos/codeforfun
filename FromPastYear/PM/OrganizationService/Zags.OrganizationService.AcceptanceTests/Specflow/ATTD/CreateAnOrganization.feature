Feature: CreateAnOrganization
	190332
	L’objectif de cette US est de créer une personne morale dans le cadre de la mise en gestion d’un contrat 
	afin de déterminer le souscripteur du contrat cadre et l’entreprise assurée du contrat groupe.

@IntegrationTest, @Organization
Scenario Outline: Creating new organization
	Given I have entered into the new organization form the <RaisonSociale> and <Reference> and <Effectif> and <FormeJuridique>		
	When I save the new organization <RaisonSociale>
	Then the new organization <RaisonSociale> should be selected

	Examples: 
	| RaisonSociale            | Reference                              | Effectif | FormeJuridique |
	| 'The First Test Company' | 'C37805E9-6D6B-49BF-B667-B74ABBC34D13' | 50       | 'SARL'         |
