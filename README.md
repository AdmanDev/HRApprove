# HRApprove

## Description

HRApprove est une application de gestion des demandes de congé pour une entreprise. Elle permet aux employés de soumettre des demandes de congé et aux gestionnaires RH de les approuver ou de les rejeter.

## Architecture

L'application suit les principes de l'**architecture hexagonale** et du **Domain Driven Design (DDD)**. Le code est divisé en trois couches principales (Domain, Application et Infrastructure).

## Accès à l'API

Si vous souhaitez tester sans l'installer en local, l'API est temporairement disponible via [ce lien](https://hrapprove.admandev.fr/swagger/index.html).

Si vous souhaitez l'installer en local, suivez les instructions ci-dessous.

## Prérequis

Avant de commencer, assurez-vous d'avoir installé les éléments suivants :

- [.NET 8.0](https://dotnet.microsoft.com/fr-fr/download/dotnet/8.0)
- [MySQL](https://dev.mysql.com/downloads/installer/)

## Installation

1. Clonez le dépôt du projet :

   ```bash
   git clone https://github.com/AdmanDev/HRApprove.git
   cd HRApprove
   ```

2. Restaurez les dépendances :

   ```bash
   dotnet restore
   ```

3. Configurez la base de données :
   - Assurez-vous que MySQL est en cours d'exécution.
   - Exécutez le fichier [hrapprove_bdd.sql](./hrapprove_bdd.sql) pour creer la base de données.
   - Mettez à jour la chaîne de connexion dans le fichier [appsettings.json](./HRApprove.API/appsettings.json) :

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=yoursqlserver;Database=HRApprove;Uid=yoursqluser;Password=yourpassword;"
     }
     ```

## Construction et exécution

### Exécution de l'API

1. Compilez et lancez le projet API :
  
   ```bash
   cd HRApprove.API
   dotnet run
   ```

2. L'API sera disponible sur [http://localhost:5148/api](http://localhost:5148/api)

3. Accédez à la documentation Swagger à l'adresse :

   [http://localhost:5148/swagger/index.html](http://localhost:5148/swagger/index.html)

**_NOTE:_**  Si les liens ne fonctionnent pas, vérifiez le port d'exécution de l'API.

### Exécution des Tests

1. Exécutez les tests unitaires :

   ```bash
   cd HRApprove.Tests
   dotnet test
   ```

## Fonctionnalités

### User Story 1 : Soumettre une demande de congé

- Un employé peut soumettre une demande de congé avec des dates, un type et un commentaire facultatif.
- Validation des dates et des champs obligatoires.

### User Story 2 : Approuver ou rejeter une demande de congé

- Un gestionnaire RH peut approuver ou rejeter une demande en ajoutant un commentaire explicatif pour le statut de la demande.
- Mise à jour du statut dans le système.

## Technologies utilisées

- **.NET 8** - Framework
- **MySQL** - Base de données
- **Swagger** - Documentation API
