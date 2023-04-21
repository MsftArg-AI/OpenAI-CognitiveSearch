#OpenAI# ChatGPT + Enterprise data with Azure OpenAI and Cognitive Search

[![Open in GitHub Codespaces](https://img.shields.io/static/v1?style=for-the-badge&label=GitHub+Codespaces&message=Open&color=brightgreen&logo=github)](https://github.com/codespaces/new?hide_repo_select=true&ref=main&repo=599293758&machine=standardLinux32gb&devcontainer_path=.devcontainer%2Fdevcontainer.json&location=WestUs2)
[![Open in Remote - Containers](https://img.shields.io/static/v1?style=for-the-badge&label=Remote%20-%20Containers&message=Open&color=blue&logo=visualstudiocode)](https://vscode.dev/redirect?url=vscode://ms-vscode-remote.remote-containers/cloneInVolume?url=https://github.com/azure-samples/azure-search-openai-demo)

Este ejemplo demuestra algunas formas de crear experiencias similares a ChatGPT utilizando sus propios datos mediante el patrón de generación con recuperación mejorada. Utiliza Azure OpenAI Service para acceder al modelo ChatGPT (gpt-35-turbo) y Azure Cognitive Search para la indexación y recuperación de datos..

El repositorio no incluye ningun tipo de informacion, es necesario subir los archivos a la Storage Account y luego ejecutar ./scripts/prepdocs.ps1 para poder procesar los documentos subidos a la storage account.

![RAG Architecture](docs/appcomponents.png)

## Características

* Interfaces de comunicación mediante chat y preguntas y respuestas
* Explora diversas alternativas para permitir a los usuarios evaluar la fiabilidad de las respuestas, a través de la inclusión de citas y el seguimiento de las fuentes de información.
* Presenta posibles enfoques para la preparación de datos, la construcción de comandos y la orquestación de la interacción entre el modelo ChatGPT y el recuperador Cognitive Search.
* Muestra posibles enfoques para indexar el contenido de sitios web , con el fin de integrar el contenido web con ChatGPT.
* Muestra posibles enfoques para la indexación del contenido de fuentes de SharePoint y la interacción con dicho contenido.

![Chat screen](docs/chatscreen.png)

## Comenzando

> **IMPORTANTE**: Para la correcta ejecución y operación de esta ilustración, es necesario contar con una suscripción de Azure autorizada para utilizar el servicio Azure OpenAI. Las solicitudes de acceso a este servicio se pueden realizar a través de este enlace: https://aka.ms/oaiapply. Además, para aquellos que sean nuevos en Azure, puede resultar valioso explorar la oportunidad de adquirir créditos de Azure gratuitos para facilitar la configuración inicial, lo cual puede realizarse a través de este sitio web: https://azure.microsoft.com/free/cognitive-search/.

> **COSTOS DE RECURSOS DE AZURE**: Por defecto, este ejemplo creará recursos de Azure App Service y Azure Cognitive Search que tienen un costo mensual, así como un recurso Form Recognizer que tiene un costo por página de documento. Si desea evitar estos costos, puede cambiarlos a sus versiones gratuitas modificando el archivo de parámetros en la carpeta "infra" (aunque hay algunas limitaciones a considerar; por ejemplo, solo se puede tener un recurso gratuito de Cognitive Search por suscripción, y el recurso Form Recognizer gratuito solo analiza las primeras 2 páginas de cada documento).


### Prerequisitos

#### Ejecución Local (Instalar los siguientes componentes)
- [Azure Developer CLI](https://aka.ms/azure-dev/install)
- [Python 3+](https://www.python.org/downloads/)
    - **Importante: Para que los scripts de configuración funcionen en Windows, es necesario que Python y el administrador de paquetes pip estén en la ruta de acceso.
    - **Importante: Asegúrese de que puede ejecutar python --version desde la consola. En Ubuntu, puede ser necesario ejecutar sudo apt install python-is-python3 para enlazar python con python3.
- [Node.js](https://nodejs.org/en/download/)
- [Git](https://git-scm.com/downloads)
- [Powershell 7+ (pwsh)](https://github.com/powershell/powershell) - For Windows users only.
   - **Importante**: Asegúrese de que pueda ejecutar pwsh.exe desde un comando PowerShell. Si esto falla, probablemente necesite actualizar PowerShell.

>NOTE: NOTA: Su cuenta de Azure debe tener permisos de escritura Microsoft.Authorization/roleAssignments/write, como Administrador de acceso de usuario o Propietario/Owner.  

#### To Run in GitHub Codespaces

You can run this repo virtually by using GitHub Codespaces or VS Code Remote Containers.  Click on one of the buttons below to open this repo in one of those options.

[![Open in GitHub Codespaces](https://img.shields.io/static/v1?style=for-the-badge&label=GitHub+Codespaces&message=Open&color=brightgreen&logo=github)](https://github.com/codespaces/new?hide_repo_select=true&ref=main&repo=599293758&machine=standardLinux32gb&devcontainer_path=.devcontainer%2Fdevcontainer.json&location=WestUs2)


### Installation

#### Project Initialization

1. Create a new folder and switch to it in the terminal
1. Run `azd login`
1. Run `azd init -t OpenAI-CognitiveSearch`
    * For the target location, the regions that currently support the models used in this sample are **East US** or **South Central US**. For an up-to-date list of regions and models, check [here](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/concepts/models)

#### Starting from scratch:

Execute the following command, if you don't have any pre-existing Azure services and want to start from a fresh deployment.

1. Run `azd up` - This will provision Azure resources and deploy this sample to those resources, including building the search index based on the PDF files found in the the storage account.
1. After the application has been successfully deployed you will see a URL printed to the console.  Click that URL to interact with the application in your browser.  

It will look like the following:

!['Output from running azd up'](assets/endpoint.png)
    

> NOTE: It may take a minute for the application to be fully deployed. If you see a "Python Developer" welcome screen, then wait a minute and refresh the page.

#### Use existing resources:

1. Run `azd env set AZURE_OPENAI_SERVICE {Name of existing OpenAI service}`
1. Run `azd env set AZURE_OPENAI_RESOURCE_GROUP {Name of existing resource group that OpenAI service is provisioned to}`
1. Run `azd env set AZURE_OPENAI_CHATGPT_DEPLOYMENT {Name of existing ChatGPT deployment}`. Only needed if your ChatGPT deployment is not the default 'chat'.
1. Run `azd env set AZURE_OPENAI_GPT_DEPLOYMENT {Name of existing GPT deployment}`. Only needed if your ChatGPT deployment is not the default 'davinci'.
1. Run `azd up`

> NOTE: You can also use existing Search and Storage Accounts.  See `./infra/main.parameters.json` for list of environment variables to pass to `azd env set` to configure those existing resources.

#### Deploying or re-deploying a local clone of the repo:
* Simply run `azd up`

#### Running locally:
1. Run `azd login`
2. Change dir to `app`
3. Run `./start.ps1` or `./start.sh` or run the "VS Code Task: Start App" to start the project locally.

#### Sharing Environments

Run the following if you want to give someone else access to completely deployed and existing environment.

1. Install the [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli)
1. Run `azd init -t OpenAI-CognitiveSearch`
1. Run `azd env refresh -e {environment name}` - Note that they will need the azd environment name, subscription Id, and location to run this command - you can find those values in your `./azure/{env name}/.env` file.  This will populate their azd environment's .env file with all the settings needed to run the app locally.
1. Run `pwsh ./scripts/roles.ps1` - This will assign all of the necessary roles to the user so they can run the app locally.  If they do not have the necessary permission to create roles in the subscription, then you may need to run this script for them. Just be sure to set the `AZURE_PRINCIPAL_ID` environment variable in the azd .env file or in the active shell to their Azure Id, which they can get with `az account show`.

### Quickstart

* In Azure: navigate to the Azure WebApp deployed by azd. The URL is printed out when azd completes (as "Endpoint"), or you can find it in the Azure portal.
* Running locally: navigate to 127.0.0.1:5000

Once in the web app:
* Try different topics in chat or Q&A context. For chat, try follow up questions, clarifications, ask to simplify or elaborate on answer, etc.
* Explore citations and sources
* Click on "settings" to try different options, tweak prompts, etc.

## Resources

* [Revolutionize your Enterprise Data with ChatGPT: Next-gen Apps w/ Azure OpenAI and Cognitive Search](https://aka.ms/entgptsearchblog)
* [Azure Cognitive Search](https://learn.microsoft.com/azure/search/search-what-is-azure-search)
* [Azure OpenAI Service](https://learn.microsoft.com/azure/cognitive-services/openai/overview)

### Note
>Note: The PDF documents used in this demo contain information generated using a language model (Azure OpenAI Service). The information contained in these documents is only for demonstration purposes and does not reflect the opinions or beliefs of Microsoft. Microsoft makes no representations or warranties of any kind, express or implied, about the completeness, accuracy, reliability, suitability or availability with respect to the information contained in this document. All rights reserved to Microsoft.

### FAQ

***Question***: Why do we need to break up the PDFs into chunks when Azure Cognitive Search supports searching large documents?

***Answer***: Chunking allows us to limit the amount of information we send to OpenAI due to token limits. By breaking up the content, it allows us to easily find potential chunks of text that we can inject into OpenAI. The method of chunking we use leverages a sliding window of text such that sentences that end one chunk will start the next. This allows us to reduce the chance of losing the context of the text.

### Troubleshooting

If you see this error while running `azd deploy`: `read /tmp/azd1992237260/backend_env/lib64: is a directory`, then delete the `./app/backend/backend_env folder` and re-run the `azd deploy` command.  This issue is being tracked here: https://github.com/Azure/azure-dev/issues/1237
