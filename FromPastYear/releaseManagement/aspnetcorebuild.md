# Comment créer un build aspnet core

**NB**: le projet a builder doit etre migré en **dotnet core 1.1** et utiliser les **.csproj** plustot que les **project.json**


## Step 1 : dotnet restore
#### definition de la tâche
* **Type**:Command Line

* **Name**:dotnet restore

#### Parametrage de la tâche
|Parameter|Value|
|----------|-----------|
|**Tool** | `dotnet` |
|**Arguments** | `restore` |
|**Advanced > Working folder** | dossier ou se trouvent les projets a builder|

## Step 2 : dotnet msbuild
#### definition de la tâche
* **Type**:Command Line

* **Name**:dotnet msbuild

#### Parametrage de la tâche

|Parameter|Value|
|----------|-----------|
|**Tool** | `dotnet` |
|**Arguments** | `msbuild /m:1 /p:Configuration=$(BuildConfiguration)` 
|**Advanced > Working folder** | dossier ou se trouvent les projets a builder|



## Step 3 : dotnet test
#### definition de la tâche
* **Type**:Command Line

* **Name**:dotnet test

#### Parametrage de la tâche

|Parameter|Value|
|----------|-----------|
|**Tool** | `dotnet` |
|**Arguments** |  `test -l trx -o $(Build.ArtifactStagingDirectory)/Path/to/tests/output/Folders`
|**Advanced > Working folder** : dossier ou se trouve le projet a tester|

## Step 4 : Publish test results
#### definition de la tâche
* **Type**:Publish test results

* **Name**:Publish test results

#### Parametrage de la tâche

|Parameter|Value|
|----------|-----------|
|**TestResultFormat** | `VSTest` |
|**Test Results Files**| `path to the trx file (accepts wildcard)` |
|**Merge test resulst**| Yes 
|**Advanced>Upload Test Attachments** | Yes|

## Step 5 : dotnet publish
#### definition de la tâche
* **Type**:Command Line

* **Name**:dotnet publish

#### Parametrage de la tâche

|Parameter|Value|
|----------|-----------|
|**Tool** | `dotnet` |
|**Arguments** | `publish -c $(BuildConfiguration) --output $(Build.ArtifactStagingDirectory) --version-suffix $(Build.BuildNumber)`|
|**Advanced>Working folder**|path to working folder|

## Step 6 : Publish Artifact :drop
#### definition de la tâche
* **Type**:Publish Artifact

* **Name**:Publish Artifact: drop

#### Parametrage de la tâche

|Parameter|Value|
|----------|-----------|
|**Path to Publish** | `$(Build.ArtifactStagingDirectory)` |
|**Artifact Name**|`drop` 
|**Artifact Type**| File share 
| **Path** | `\\zsrv-bld306.corp.zags.com\Builds\EMEA\$(Build.DefinitionName)\$(Build.BuildNumber)`|
