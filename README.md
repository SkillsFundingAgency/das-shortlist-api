## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# _Project Name_

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

_Update these badges with the correct information for this project. These give the status of the project at a glance and also sign-post developers to the appropriate resources they will need to get up and running_

[![Build Status](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status%2Fdas-shortlist-api?repoName=SkillsFundingAgency%2Fdas-shortlist-api&branchName=main)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=2956&repoName=SkillsFundingAgency%2Fdas-shortlist-api&branchName=main)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-shortlist-api&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=SkillsFundingAgency_das-shortlist-api)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

## 🚀 Installation

In the SFA.DAS.Shortlist.Api project, if it does not exist already, add appSettings.Development.json file with following content:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true;",
  "ConfigNames": "SFA.DAS.Shortlist.Api",
  "Environment": "LOCAL",
  "Version": "1.0",
  "APPINSIGHTS_INSTRUMENTATIONKEY": "APPLICATIONINSIGHTS_CONNECTION_STRING"
}
```

The repo has a database project which should be published locally to a database named 'SFA.DAS.Shortlist.Database'

### Pre-Requisites
* A clone of this repository
* A storage emulator like Azurite
* The DAS Shortlist Database is published
  
### Config

You can find the latest config file in [das-employer-config](https://github.com/SkillsFundingAgency/das-employer-config/blob/master/das-shortlist-api/SFA.DAS.Shortlist.Api.json) repository.

In the API project, if not exist already, add appSettings.Development.json file with following content:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true;",
  "ConfigNames": "SFA.DAS.Shortlist.Api",
  "Environment": "LOCAL",
  "Version": "1.0",
  "APPINSIGHTS_INSTRUMENTATIONKEY": ""
} 
```

## Technologies
* .Net 8.0
* SQL Server
* Azure Table Storage
* NUnit
* Moq
* FluentAssertions