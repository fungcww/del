# How to setup the pipeline configurations for my applications?

### Step-1 Prerequisite

The App team should onboard the FWD DevOps first before trigger the pipeline execution.  

[More Detail]()

 

### Step-2 Clone the source code of the project

1.  Create a directory for Azure DevOps (Suggest path: C:\Source\Azure_DevOps\).

2.  Go to the directory

3.  Download PowerShell file from get_all_repo.ps1 - Repos (azure.com) and put it to above directory.

    - make sure your machine installed both azure cli and azure cli devops plugin before going to next step

4.  Execute the PowerShell file by “.\{name_of_your_powershell}.ps1 {Azure_DevOps_Project}” in PowerShell command prompt to get all repo of an Azure DevOps Project, e.g. “.\get_all_repo.ps1 HK_LCP”.

    - add the option "-ExecutionPolicy Bypass", e.g. ".\get_all_repo.ps1 HK_LCP -ExecutionPolicy Bypass", at the end of he command if you encounter error of execution policy.

 

### Step-3 Download the latest version of “az_pipeline_config”

1.  App team download/clone the latest “az_ pipeline_config” from the git “az_pipeline_config” in the HK_DevOps project. (git clone https://dev.azure.com/FWDGODevOps/az_pipeline_config)

2.  Place the downloaded “az_ pipeline_config” to the root directory of the application project code

 

### Step-4 Configure the JSON paramters in the “az_pipeline_config”

Configure the parameter regarding to the Configuration Parameters as below

 

### Step-5 Commit/Push the git request along with the az_config_pipeline config files.

```

Cd <Your application root_directory>

Git remote add origin https://dev.azure.com/FWDGODevOps/az_pipeline_config

Git branch <feature_branch>;

Git add .;

Git commit -m “<Comments>”

Git push -u origin <feature_branch>

```  

 

 

# Configuration Parameters

## Nodejs_Config.json
| Property Name| Description | Expect Value |  
|-----------|:-----------:|-----------:|  
| applicationType | The framework type of the nodejs application.        |node <br />angular       |
| build.poolVmImage   | The OS environment to be use for the application build        | windows-latest<br />windows-2019<br />ubuntu-latest |
| build.applicationEnv | The environment of the application | <Defined by the application team\><br />e.g. uat13 |
| build.vstsFeedPackagePublish | The name of the artifact after build | {azure_project}.{azure_repo}  |
| angular.basehref | The base_href in the ng build command | <Defined by the application team\><br />e.g. /web13/ |

### Sample:

```

{

    "applicationType": "angular",

    "build": {

        "poolVmImage": "windows-latest",

        "applicationEnv": "uat13",

        "builderVersion": "12.14.1",

        "vstsFeedPackagePublish": "eservice_web_UAT13_HKU301"

    },

    "angular":{

        "baseHref": "/web13/"

    }

}

```