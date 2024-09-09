# Product Feature Management System

This project consists of an Angular frontend and a .NET Core backend API with MSSQL integration.

## Prerequisites

- Node.js (v14 or later)
- .NET Core SDK 7.0
- Angular CLI (v15)
- MSSQL Server 2019 or later

## Setup Instructions

1. Clone the repository:
 ```
git clone https://github.com/andymahajan/ProductFeatureManagement.git
cd --YourFolderName--
 ```

2. Set up the Database:
 ```
cd Database
Execute the Scripts from the folder in below order
Schema 
Tables
 ```

3. Set up the backend:
 ```
cd ProductFeatureManagementWebApi
dotnet restore
dotnet build
 ```

4. Set up the frontend:
 ```
cd ../ProductFeatureManagementAngular
npm install
```

### Running the Application

After completing the setup, you have option to run the application:

**Using Visual Studio**:

You can run the application directly through Visual Studio. Open the solution file in Visual Studio, and start the application by pressing F5 or using the "Start Debugging" option.

- Start the backend:
  ```
  cd ../ProductFeatureManagementWebApi
  dotnet run
  ```
- In a new terminal, start the frontend:
  ```
  cd ../ProductFeatureManagementAngular
  ng serve
  ```
Open a web browser and navigate to http://localhost:4200

## Ports

- The backend API will be available at `https://localhost:5089`
- The frontend will be served at `http://localhost:4200`

## Project Structure

- `ProductFeatureManagementWebApi/`: .NET Core 7 backend with MSSQL integration
- `ProductFeatureManagementAngular/`: Angular 15 frontend application

## Backend (AddressAPI)

- Built with .NET Core 7
- Uses MSSQL for data persistence
- Implements RESTful API endpoints for product feature management

## Troubleshooting

- If you encounter CORS issues, ensure that the backend is properly configured to allow requests from the frontend origin.
- Verify that Database is setup in MSSQL server and connection string is accurate according to the server you connected.
- Verify connection string in appsettings.json file.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

For backend issues, refer to the .NET Core and MSSQL documentation.
Troubleshooting
If you encounter CORS issues, ensure that the backend is properly configured to allow requests from the frontend origin.
