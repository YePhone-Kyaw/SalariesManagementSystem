# Salary Management System

## Overview
The Salary Management System is a web application designed to efficiently manage employee salaries. The application offers functionalities to add, edit, and remove employees, as well as to manage their salary details comprehensively.

## Features

### Login Page
- **Extensions Required**: Ensure that the following extensions and packages are installed for the system to function correctly:
  - `Newtonsoft.Json`
  - `IdentityModel.Token`
  
  The login page requires users to authenticate themselves to access the system. This ensures that only authorized users can make changes to the salary details.

### Database Connection
- Create a new database
- Connect the database with the application

### Add Employee
- Allows the addition of a new employee to the system.
- Inputs required:
  - Employee ID (ID has to be unique)
  - Employee name
  - Career
  - Pay rate per hour

### Edit Employee
- Adjacent to each employee's details
- An update section allows the users to modify their information by using their unique employee IDs.

### Remove Employee
- Permits the deletion of employee details from the system.

### Clear All Employees
- A "Clear" button to remove all employee records from the system at once.

### Sort/Filter
- Employees can be sorted or filtered by:
  - Alphabetical order of names
  - Salary amount

## Setup
1. Install required extensions and packages.
2. Ensure proper configuration of the login system.
3. Start the application and log in to manage employee salaries.

## Login Features
- Secure authentication to ensure data protection.
- Session management to keep the user logged in during activity.
- Option to recover or reset user passwords.
