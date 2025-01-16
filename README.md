# Wallet Management API

## Overview
This project is a .NET API service designed to manage user wallets for the Hubtel app. It provides endpoints to create, retrieve, list, and delete wallets while enforcing specific business rules such as preventing duplicate wallets, limiting the number of wallets per user, and only storing the first six digits of card numbers. The project follows the repository pattern and is configured for validation using attributes on the model.

---

## Features

- **POST /api/wallets**: Add a wallet to a user's account.
  - Prevents duplicate wallet names.
  - Restricts each user to a maximum of 5 wallets.
  - Stores only the first 6 digits of the card number.

- **GET /api/wallets**: Retrieve a list of all wallets.

- **GET /api/wallets/{id}**: Retrieve a single wallet by its ID.

- **DELETE /api/wallets/{id}**: Delete a wallet by its ID.

---

## Technologies Used

- **Framework**: .NET 6
- **Pattern**: Repository Pattern
- **Storage**: In-Memory Database 
- **Documentation**: Swagger 
- **Validation**: Data Annotations

---

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Swagger](https://swagger.io/) for API testing

---

## Setup and Installation

1. **Clone the Repository**
   ```
   git clone <repository-url>
   cd WalletManagementApi
   ```

2. **Restore Dependencies**
   ```
   dotnet restore
   ```

3. **Run the Application**
   ```
   dotnet run
   ```

4. **Access Swagger Documentation**
   Open your browser and navigate to:
   ```
   http://localhost:<port>/swagger
   ```
   Replace `<port>` with the port number displayed in the console output.

---

## Endpoints

### 1. Add a Wallet
**POST** `/api/wallets`

- **Request Body**:
  ```json
  {
    "name": "My Wallet",
    "type": "card",
    "accountNumber": "1234567890123456",
    "accountScheme": "visa",
    "owner": "1234567890"
  }
  ```

- **Responses**:
  - `201 Created`: Wallet added successfully.
  - `400 Bad Request`: Validation errors or rule violations.

### 2. List All Wallets
**GET** `/api/wallets`

- **Responses**:
  - `200 OK`: Returns a list of wallets.

### 3. Retrieve a Single Wallet
**GET** `/api/wallets/{id}`

- **Path Parameter**:
  - `id` (string): ID of the wallet to retrieve.

- **Responses**:
  - `200 OK`: Wallet details.
  - `404 Not Found`: Wallet not found.

### 4. Delete a Wallet
**DELETE** `/api/wallets/{id}`

- **Path Parameter**:
  - `id` (string): ID of the wallet to delete.

- **Responses**:
  - `200 OK`: Wallet deleted successfully.
  - `404 Not Found`: Wallet not found.

---

## Business Rules

1. Prevent duplicate wallet names for the same user.
2. Limit each user to a maximum of 5 wallets.
3. Store only the first 6 digits of card numbers.

---


## Validation Attributes

The `Wallet` model uses data annotations for validation:

- **Name**: Required, minimum length 3, maximum length 50.
- **Type**: Must be either `momo` or `card`.
- **AccountNumber**: Limited to the first 6 digits.
- **AccountScheme**: Must match one of `visa`, `mastercard`, `mtn`, `vodafone`, or `airteltigo`.
- **Owner**: Must be a valid phone number.

---

## Testing the API

### Using Swagger
1. Run the application: `dotnet run`.
2. Open Swagger at `http://localhost:<port>/swagger`.
3. Use the interactive UI to test all endpoints.

---

