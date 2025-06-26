# PharmacyApp

This is a simple web API for managing a pharmacy's data, including patients, doctors, prescriptions, and medicaments.

## API Endpoints

### Patients

- **GET /api/patients/{id}**

  Retrieves details for a specific patient, including their prescriptions sorted by `DueDate`.

  - **Parameters:**
    - `id` (integer, required): The ID of the patient.
  - **Responses:**
    - `200 OK`: Returns the patient's details.
    - `400 Bad Request`: If the provided ID is invalid.
    - `404 Not Found`: If the patient with the given ID does not exist.

### Prescriptions

- **POST /api/prescriptions**

  Adds a new prescription.

  - **Request Body:**
    ```json
    {
      "patient": {
        "firstName": "string",
        "lastName": "string",
        "birthDate": "2024-06-04"
      },
      "medicaments": [
        {
          "id": 0,
          "dose": 0,
          "details": "string"
        }
      ],
      "doctor": {
        "firstName": "string",
        "lastName": "string"
      },
      "dueDate": "2024-06-04"
    }
    ```
  - **Responses:**
    - `200 OK`: Returns the newly created prescription.
    - `400 Bad Request`: If the request is invalid (e.g., `DueDate` is earlier than `Date`, more than 10 medicaments).
    - `404 Not Found`: If a medicament or doctor does not exist.

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core

## How to Run

1.  **Clone the repository:**

    ```bash
    git clone <repository-url>
    cd PharmacyApp/PharmacyApp
    ```

2.  **Restore dependencies:**

    ```bash
    dotnet restore
    ```

3.  **Update the database:**

    Ensure you have `dotnet-ef` tools installed.

    ```bash
    dotnet tool install --global dotnet-ef
    ```

    Apply migrations to create the database schema:

    ```bash
    dotnet ef database update -p PharmacyApp -s PharmacyApp
    ```

4.  **Run the application:**
    ```bash
    dotnet run --project PharmacyApp
    ```

The API will be available at `http://localhost:5000` or `https://localhost:5001`.
