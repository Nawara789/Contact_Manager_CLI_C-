# Contact Manager CLI 
## Goal
Build a CLI contact manager that is fast and efficient using modern indexing

## Problems it solves: 
The outdated system relies on phone-book-style indexing approach and had several issues:
- Searching for contacts is slow and inefficient. 
- Data storage is rigid and difficult to extend. 
- The system blocks operations while loading data. 

## Setup & Run

1. Ensure you have [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download) installed.
2. Clone this repository:
```bash
git clone https://github.com/Nawara789/Contact_Manager_CLI_C-.git    
```

Navigate to the project folder:

```bash
cd ContactManagerCLI
```
Build and run:

```bash
dotnet run
```
##File Structure
ContactManagerCLI/  
├─ ContactManagerCLI.csproj  
├─ Program.cs              # Main CLI menu  
├─ Contact.cs              # Contact model  
├─ ContactManager.cs       # Handles CRUD, search, and filter logic  
├─ IContactRepository.cs   # Repository interface  
├─ JSONContactRepository.cs# JSON persistence implementation  
├─ Contacts.json           # Data storage file (created automatically)  
## Design Rationale

- The legacy phone-book–style lookup was replaced with hash-based indexing using in-memory dictionaries to provide O(1) average-time retrieval by contact ID, eliminating linear scans for common operations.

- The application loads contact data once at startup and operates on in-memory structures to avoid blocking the user interface during repeated disk access.

- Data persistence is abstracted behind a repository interface, allowing the storage mechanism (JSON today, database in the future) to be changed without modifying business logic.

- The core contact management logic is separated from the CLI user interface to follow Single Responsibility and improve testability and maintainability.

- The design supports future indexing strategies (e.g., by email or phone) by allowing additional in-memory indexes to be introduced without changing the persistence layer or UI flow.

- The system uses simple, readable object-oriented structures rather than custom data structures to favor maintainability and real-world engineering practices.

## Usage Notes
- Only search by ID is currently optimized; other fields are filtered sequentially.

- Contacts are saved to JSON only when user selects 'Save'.

- it can be extended later to search by name, email, or phone efficiently using additional indexing.