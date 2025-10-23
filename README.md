# IBAS Support System - CosmosDB WebApp

En Blazor WebAssembly applikation til håndtering af kundesupport henvendelser for IBAS cykler, powered by Azure CosmosDB.

## Projektbeskrivelse

Dette projekt er en cloud-baseret webapp der giver IBAS mulighed for at modtage, kategorisere og administrere kundesupport henvendelser. Systemet er bygget med .NET Blazor WebAssembly og Azure CosmosDB for global skalerbarhed.

### Funktioner
- Opret supporthenvendelser med validering
- Kategorisering af henvendelser (teknisk, reservedele, forslag, etc.)
- Status tracking (Ny, I gang, Løst, Lukket)
- Azure CosmosDB integration
- Responsiv Blazor WebAssembly UI

## Azure Opsætning - Kommandoer

### Opret Resource Group

az group create --name ibas-support-rg --location northeurope

**Opret CosmosDB Konto**

az cosmosdb create --name <fint navn> --resource-group ibas-support-rg --locations regionName='North Europe'

**Opret Database**

az cosmosdb sql database create --account-name <fint navn> --database-name SupportDatabase --resource-group ibas-support-rg

**Opret Container**

az cosmosdb sql container create --account-name <fint navn> --database-name <databsename> --name SupportMessages --partition-key-path "/id" --throughput 400 --resource-group ibas-support-rg

**Hent Conncetion string** 

az cosmosdb keys list --name <fint navn> --resource-group ibas-support-rg --type connection-strings

**Tilføj CORS Til localhost** 

az cosmosdb update --name <fint navn> --resource-group ibas-support-rg --cors-rules "http://localhost:5215"

**az cosmosdb keys list** --name <fint navn> --resource-group ibas-support-rg --type connection-strings

**az cosmosdb update** --name <fint navn> --resource-group ibas-support-rg --cors-rules "http://localhost:5215"
Roter Nøgle (Hvis kompromitteret, kan ske..)

**az cosmosdb keys regenerate** --name <fint navn> --resource-group ibas-support-rg --key-kind primary

# Projektstruktur


IBAS-Support-System/
├── Models/

│   └── SupportMessage.cs          # Datamodel med validering

├── Services/

│   ├── ICosmosDbService.cs        # Interface

│   ├── CosmosDbService.cs         # CosmosDB integration

│   └── MockCosmosDbService.cs     # Mock service til development

├── Pages/

│   ├── Home.razor                 # Velkomstside

│   ├── CreateSupport.razor        # Opret henvendelse

│   └── SupportList.razor          # Vis henvendelser

├── Shared/

│   └── NavMenu.razor              # Navigation

└── wwwroot/

 ├── appsettings.json           # Konfiguration (local)
    
 └── appsettings.example.json   # Eksempel konfig

# Valg af MockService til aflevering
- Pga. CORS issues med Azure CosmosDB under development, bruger den afleverede version MockCosmosDbService som:
- Under development to be fixed...

# Teknologier
Frontend: Blazor WebAssembly

Backend: .NET 9.0

Database: Azure CosmosDB (NoSQL)

Cloud: Azure Cloud Services

*Udviklet som del af Cloud Computing - M4.04 Afleveringsopgave*

**LØST af Benjamin Lorenzen og Lukas Bæk-Nielsen** 
