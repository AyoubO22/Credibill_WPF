
CrediBill WPF

CrediBill is a desktop application built with WPF (Windows Presentation Foundation) and Entity Framework Core. It is designed to manage customers, invoices, and payments with basic CRUD (Create, Read, Update, Delete) functionality.

This project is part of my schoolwork, and I am a beginner in WPF and C#. My goal is to learn and apply the fundamentals of building a desktop application with database integration.

Features

	•	Customer Management:
Add, view, edit, and delete customer information (name, email, phone number, etc.).
	•	Invoice Management:
Track invoices associated with customers, including amounts and issue dates.
	•	Payment Tracking:
Manage payments linked to invoices with details like amount and payment date.
	•	Soft Deletion:
Data is never permanently deleted; records are hidden using a Deleted property for better data retention.
	•	Database Seeding:
Pre-loaded sample data for testing purposes.

Technology Stack

	•	C#: Primary programming language.
	•	WPF: For the user interface.
	•	Entity Framework Core: For database management.
	•	SQLite: Lightweight database for local data storage.
	•	MVVM Architecture: Applied with views and a clean separation of concerns.
	•	LINQ: For querying data.

Application Structure

CrediBill_WPF  
├── CrediBill_WPF.sln       // Solution file  
├── Data                    // Database context and seeders  
│   ├── AppDbContext.cs  
│   ├── DbSeeder.cs  
├── Models                  // Data models  
│   ├── Customer.cs  
│   ├── Invoice.cs  
│   ├── Payment.cs  
├── Views                   // WPF views (XAML and code-behind)  
│   ├── MainWindow.xaml  
│   ├── CustomerView.xaml  
│   ├── InvoiceView.xaml  
│   ├── PaymentView.xaml  
├── App.xaml                // Application entry point  
└── README.md               // Project documentation  

Prerequisites

Before running the application, ensure you have the following installed:
	•	.NET 6 SDK or later
	•	A code editor like Visual Studio (preferred) or Visual Studio Code.

Getting Started

	1.	Clone the repository:

git clone https://github.com/AyoubO22/Credibill_WPF.git  


	2.	Navigate to the project folder:

cd Credibill_WPF  


	3.	Open the solution in Visual Studio:
Double-click the CrediBill_WPF.sln file.
	4.	Run the application:
	•	Set MainWindow.xaml as the startup window.
	•	Press F5 or click Start to run the application.

Usage

	1.	Customers:
	•	Add, edit, or delete customers from the main customer management view.
	2.	Invoices:
	•	Navigate to the invoice view to create, view, or update invoices associated with specific customers.
	3.	Payments:
	•	Manage payments under the corresponding invoice view.



Notes

This project is part of my learning journey. As a beginner, I welcome any feedback or suggestions for improvement!

License

This project is open-source and available under the MIT License.
