# DVLD (Driving License Management System)

## Project Overview
DVLD is a comprehensive Driving License Management System developed as part of Course 19 on the Programming Advices platform. The project implements essential driving license services with added custom features to streamline the management of driver records, license issuance, renewals, and related processes.

## Features

- **Three-Tier Architecture**: Designed with a clear separation of concerns into Data Access Layer (DAL), Business Logic Layer (BL), and Presentation Layer.
- **Database**: Built on SQL Server with over 14 normalized tables and established relationships, accessed via ADO.NET and more than 90 stored procedures (SPs) to ensure efficiency and security.
- **Rich User Interface**: Over 25 screens featuring reusable User Controls to avoid code duplication and maintain consistency.
- **Advanced Data Handling**:  
  - Use of database Views for professional and user-friendly data presentation via DataGridView controls.  
  - Full CRUD operations (Create, Read, Update, Delete) plus advanced search capabilities on all entities.
- **Modular Management**:  
  - User Management  
  - Person Records Management  
  - Driver Records Management  
  - License Services including:  
    - New Local Driving License (multi-stage process: eye exam, written test, and road test)  
    - License Renewal  
    - Replacement for Lost or Damaged Licenses  
    - Release of Detained Licenses  
    - New International License  
    - Retake Tests  
  Each service manages payment processing and all related CRUD operations.
- **Security Features**:  
  - Secure login with username and password, including option to store credentials in Windows Registry.  
  - Password hashing with salt for enhanced security.  
  - SQL Injection protection through advanced input validation and strict stored procedure access policies.
- **Event-Driven Communication**: Use of Delegates and Events for smooth communication between forms and components.

## Technologies Used

- C# .NET Framework  
- SQL Server with extensive use of **T-SQL** for writing stored procedures, triggers, and complex queries  
- ADO.NET  
- Three-Tier Architecture (DAL / BL / Presentation)  
- Stored Procedures (SP) for data operations

## System Requirements

- Visual Studio Community 2022 (or later)  
- SQL Server (compatible version)

## Installation & Setup

1. Clone or download the repository.  
2. Restore the provided SQL database (`DVLD DB`) from the GitHub files to your SQL Server instance.  
3. Update the connection string in the application to match your SQL Server credentials and database settings.

## Usage

- Launch the application.  
- Login credentials:  
  - Username: `RachidUser`  
  - Password: `000000`

## Project Structure

- Three-tier architecture separating the data access, business logic, and user interface layers.  
- Extensive use of Stored Procedures for all database interactions.

## Contact & Support

For questions or support, connect with me on LinkedIn:  
[Zakaria Sakalli Housaini](https://www.linkedin.com/in/zakaria-sakalli-housaini-1a782b289)

## Contributing

Contributions are welcome! Please feel free to fork the project and submit pull requests with improvements or bug fixes.

## Acknowledgements

Special thanks to the Programming Advices platform for the original course content and guidance.

---

*This README is intended to provide a clear, concise, and professional overview of the DVLD project to assist developers, users, and contributors.*
