# Documentation relative au release management

[KPI mesurés par la build pipeline](./buildPipelineKPI.md)

## politique de versionning et packaging
Le format de versions utilisées par nos assemblies est le semantic versionning utilisé par nuget.

Le versionning est la responsabilité du developpeur.

Les informations de packaging doivent êtres placées dans le fichier `.csproj`.


[comment versionner des assemblies pour nuget](https://codingforsmarties.wordpress.com/2016/01/21/how-to-version-assemblies-destined-for-nuget/)

[nuget pack and restore as MSBuild targets](https://docs.microsoft.com/en-us/nuget/schema/msbuild-targets#pack-target)

- La build CI d'un repository de package livrera par defaut le .symbols.nupkg se package contiendra les sources et les symboles.
    + cela permettra au dev de step-in dans le package avec la certitude de toujours avoir la bonne version des sources. 
- La build d'integraion d'un repository de package livrera le .nupkg sans symboles et sans sources. 
- Une version en cours de developpemnt devra contenir dans son `.csproj` un package version sous la forme suivante:
    + `<PackageVersion>Major.Minor.Patch-beta</PackageVersion>` où Major, Minor et Patch sont des entiers.
    + Le `-beta` indiquera à nuget que la version de ce package est en pre-release.
- Une version livrée en prod devra utiliser la même nomenclature sans le `-beta`.
- lors de la phase de developement:
    + le digit Major ne doit bouger qu'en cas de breaking change majeur
    + le digit Mineur doit bouger en cas de nouvelle fonctionnalité ou de breaking change mineur.
    + le digit Patch doit être incrémenté a chaque push afin que le package puisse être livré dans le nuget feed de VSTS

### .csproj example

```

<Project Sdk="Microsoft.NET.Sdk" ToolsVersion="15.0">
  <PropertyGroup>
    <TargetFramework>netstandard1.6</TargetFramework>
      <PackageId>Zags.Utility</PackageId>
      <PackageVersion>1.0.3-beta</PackageVersion>
      <Authors>Zags</Authors>
      <Description>Functionnal and error classes for Zags foundation</Description>
      <PackageRequireLicenseAcceptance>false</PackageRequireLicenseAcceptance>
      <PackageReleaseNotes>First release</PackageReleaseNotes>
      <Copyright>Copyright 2016 (c) Zags Corporation. All rights reserved.</Copyright>
      <PackageTags>functionnal errors</PackageTags>
  </PropertyGroup>
  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
    <OutputType>library</OutputType>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="**\*.cs" />
    <EmbeddedResource Include="**\*.resx" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="NETStandard.Library" Version="[1.6.1]"/>
  </ItemGroup>
</Project>

```
## branches

[Branching strategies sur GIT](./branchingStrategies.md)

## Builds

[création d'un build aspnet core](./aspnetcorebuild.md)

[création d'un build CI livrant un package nuget](./NugetCIBuild.md)

[création d'un agent de build linux](./linuxBuildAgent.md)

## Docker

[création d'une image docker aspnet core](./aspnetCoreDocker.md)
[création d'une image docker ngnix monter un RP devant Kestrel](./buildPipelineKPI.md)