AssistAI

AssistAI is a modern AI-powered web application built using .NET 8 and Microsoft Azure services. It integrates with Azure Active Directory (Azure AD) for authentication and Azure Key Vault for secure secret management.

Features

Authentication: Secure access using Azure AD with support for OAuth 2.0 Client Credentials flow.

Secret Management: Securely retrieve sensitive data from Azure Key Vault.

Minimal API: Lightweight API endpoints with authentication.

Logging: Detailed logging using ASP.NET Core's built-in logging mechanisms.

Deployment-Ready: Configured for deployment on Azure App Services.

Tech Stack

Backend: .NET 8 (C#)

Authentication: Azure Active Directory

Security: Azure Key Vault

Hosting: Azure App Services

Logging: Built-in ASP.NET Core logging

Getting Started

Prerequisites

Ensure you have the following installed:

.NET 8 SDK

Azure CLI

Visual Studio Code or Visual Studio

Git (for version control)

Installation

Clone the repository:

git clone https://github.com/your-username/AssistAI.git
cd AssistAI

Set up environment variables:
Create an .env file and add the following:

AZURE_TENANT_ID=your-tenant-id
AZURE_CLIENT_ID=your-client-id
AZURE_CLIENT_SECRET=your-client-secret
AZURE_KEY_VAULT_URI=https://your-keyvault-name.vault.azure.net/

Restore dependencies:

dotnet restore

Run the application:

dotnet run

API Endpoints

Public Endpoint:

GET http://localhost:7122/

Response:

"AssistAI application is running!"

Secure Endpoint (Requires Token):

GET http://localhost:7122/secure-data
Authorization: Bearer {your-access-token}

Response:

{ "secret": "This is secure data!" }

Obtaining an Access Token

Use the following command to request an access token from Azure AD:

curl -X POST https://login.microsoftonline.com/{tenant-id}/oauth2/v2.0/token \
     -d 'grant_type=client_credentials' \
     -d 'client_id={client-id}' \
     -d 'client_secret={client-secret}' \
     -d 'scope=api://{client-id}/.default'

Deployment

Login to Azure:

az login

Deploy to Azure App Service:

az webapp up --name AssistAIApp --resource-group AssistAI-RG --runtime "DOTNET:8.0"

CI/CD (GitHub Actions)

A GitHub Actions workflow can be configured to automate deployment by adding the following file in .github/workflows/deploy.yml:

name: Deploy to Azure
on: push

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '8.0'
      - name: Build
        run: dotnet build --configuration Release
      - name: Deploy
        run: az webapp up --name AssistAIApp --resource-group AssistAI-RG

Contributing

We welcome contributions! To contribute:

Fork the repository.

Create a new feature branch.

Commit your changes.

Push to your fork and submit a pull request.

License

This project is licensed under the MIT License. See the LICENSE file for more details.
