Feature: SearchAnOrganizationSimpleSearch
	US 190314
	En tant que gestionnaire habilité.e,
	Je veux rechercher une personne morale au sein du référentiel des personnes morales avec un nombre minimal de critères
	Afin de pouvoir identifier un souscripteur ou une entreprise assurée  

@IntegrationTest, @Organization
Scenario: Searching an organization without criteria check that all the organizations are returned
	Given There are 3 organizations
	When I search an organization
	Then The result should contains 3 organizations

@IntegrationTest, @Organization
Scenario Outline: Searching an organization by the RaisonSociale and Reference check that matching organization is returned
	Given There are 3 organizations with this following data:
	| RaisonSociale				| Reference                            | Effectif | FormeJuridique |
	| The First Test Company	| C37805E9-6D6B-49BF-B667-B74ABBC34D13 | 50       | SARL           |
	| The Second Test Company	| 5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 30       | SARL           |
	| The Third Test Company	| 6A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 60       | SA	           |
	When I search an organization by the <RaisonSociale> and <Reference>
	Then The result should be the organization <Result>
	
	Examples: 
	| RaisonSociale           | Reference                            | Result                  |
	| The First Test Company  | C37805E9-6D6B-49BF-B667-B74ABBC34D13 | The First Test Company  |
	| The First Test Company  |	 						             | The First Test Company  |
	|                         | C37805E9-6D6B-49BF-B667-B74ABBC34D13 | The First Test Company  |
	| The Second Test Company | 5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | The Second Test Company |

@IntegrationTest, @Organization
Scenario Outline: Searching an organization by RaisonSociale and Reference check that matching severals organizations are returned
	Given There are 3 organizations with this following data:
	| RaisonSociale				| Reference                            | Effectif | FormeJuridique |
	| The First Test Company	| C37805E9-6D6B-49BF-B667-B74ABBC34D13 | 50       | SARL           |
	| The Second Test Company	| 5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 30       | SARL           |
	| The Third Test Company	| 6A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 60       | SA	           |
	When I search an organization by the <RaisonSociale> and <Reference>
	Then The result should be <OrganizationCount>
	
	Examples: 
	| RaisonSociale | Reference           | OrganizationCount |
	| The*          |                     | 3                 |
	| *Test Company |                     | 3                 |
	|               | *F36                | 2                 |
	|               | *A8EE1A0-25BE-4E00* | 2                 |

@IntegrationTest, @Organization
Scenario Outline: Searching an organization by RaisonSociale and Reference check that none organization are returned
	Given There are 3 organizations with this following data:
	| RaisonSociale				| Reference                            | Effectif | FormeJuridique |
	| The First Test Company	| C37805E9-6D6B-49BF-B667-B74ABBC34D13 | 50       | SARL           |
	| The Second Test Company	| 5A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 30       | SARL           |
	| The Third Test Company	| 6A8EE1A0-25BE-4E00-BEAD-A9BF76988F36 | 60       | SA	           |
	When I search an organization by the <RaisonSociale> and <Reference>
	Then The result should be empty
	
	Examples: 
	| RaisonSociale           | Reference                            |
	| The Fourth Test Company |                                      |
	|                         | 12345678-25BE-4E00-BEAD-A9BF76988F36 |