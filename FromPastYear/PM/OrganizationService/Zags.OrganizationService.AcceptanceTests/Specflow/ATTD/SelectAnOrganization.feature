Feature: SelectAnOrganization
	US 190316
	En tant que gestionnaire habilité.e,
	Je veux désigner la PM qui résulte de la recherche réalisée au sein du référentiel des personnes morales
	Afin de désigner le souscripteur d’un contrat cadre et l’entreprise assurée d’un contrat groupe

@IntegrationTest, @Organization
Scenario Outline: Selecting an organization from the organization search list check that the selected organization details are showed
	Given I have this following organizations in the search list:
	| RaisonSociale           | Reference                            | Effectif | FormeJuridique |
	| The First Test Company  | C37805E9-6D6B-49BF-B667-B74ABBC34D13 | 50       | SARL           |
	| The Second Test Company | 5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 30       | SARL           |
	| The Third Test Company  | 6A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 60       | SA             |
	When I select the organization <RaisonSociale>
	Then the organization <RaisonSociale> with these <Reference> and <Effectif> and <FormeJuridique> details should be displayed on the screen

	Examples: 
	| RaisonSociale          | Reference                            | Effectif | FormeJuridique |
	| The First Test Company | C37805E9-6D6B-49BF-B667-B74ABBC34D13 | 50       | SARL           |