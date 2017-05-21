Feature: Organization
	
@IntegrationTest, @Organization
Scenario: When searching a organization by Raison Sociale check that matching person is returned
	Given I create a new organization with following data:
	| RaisonSociale          | Reference                            | Effectif | FormeJuridique |
	| The First Test Company | C37805E9-6D6B-49BF-B667-B74ABBC34D13 | 50       | SARL           |
	And I save the organization
	When I search with the following criteria:
	| Field				| Value										|
	| RaisonSociale		| The First Test Company					|	
	Then The search result contains an organization with following data:
	| Field				| Value										|
	| RaisonSociale     | The First Test Company					|
	| Reference			| C37805E9-6D6B-49BF-B667-B74ABBC34D13		|
	| Effectif			| 50										|
	| FormeJuridique    | SARL										|
	
@IntegrationTest, @Organization
Scenario: Selecting an organization from the organization search list check that the selected organization details are showed
	Given I create 3 organizations with following data:
	| RaisonSociale				| Reference                            | Effectif | FormeJuridique |
	| The First Test Company	| C37805E9-6D6B-49BF-B667-B74ABBC34D13 | 50       | SARL           |
	| The Second Test Company	| 5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 30       | SARL           |
	| The Third Test Company	| 6A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 60       | SA	           |
	And I save these organizations
	And I search organizations without criteria
	When I select the organization with the following data:
	| RaisonSociale          |
	| The First Test Company |
	Then the result should be display on the screen the following data:
	| RaisonSociale				| Reference                            | Effectif | FormeJuridique |
	| The First Test Company	| C37805E9-6D6B-49BF-B667-B74ABBC34D13 | 50       | SARL           |