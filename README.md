
# 📚 SQL Employee Management System

This project contains SQL scripts for managing employee records across three tasks. Each task focuses on different aspects of database design including table creation, data insertion, and implementing relationships using foreign keys.

---

## ✅ Task 1: Basic Employee Table

### 🔧 Create Employee Table

```sql
CREATE TABLE Employee (
    Id INT NOT NULL PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50),
    LastName VARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    Address VARCHAR(100),
    MobileNumber VARCHAR(10) NOT NULL
);
```

### 🔄 Reset Identity (Optional)

```sql
DBCC CHECKIDENT ('Employee', RESEED, 0);
```

### 📥 Insert Records

```sql
INSERT INTO [Practical12].[dbo].[Employee] 
    ([FirstName], [MiddleName], [LastName], [DOB], [Address], [MobileNumber])
VALUES 
    ('John', 'A', 'Doe', '1990-05-21', 'New York', '9876543210'),
    ('Alice', NULL, 'Smith', '1985-08-15', 'Los Angeles', '9123456789'),
    ('Bob', 'C', 'Williams', '1992-02-10', 'Chicago', '8569741230'),
    ('David', 'M', 'Brown', '1988-11-25', 'Houston', '7854123690'),
    ('Emma', NULL, 'Johnson', '1995-07-19', 'San Francisco', '6958741230'),
    ('Michael', 'J', 'Miller', '1983-06-05', 'Boston', '7458963210'),
    ('Sophia', 'R', 'Davis', '1991-09-30', 'Seattle', '9638527410'),
    ('Liam', 'T', 'Wilson', '1994-04-10', 'Dallas', '8521479630'),
    ('Olivia', NULL, 'Anderson', '1989-03-14', 'Atlanta', '7896541230'),
    ('Ethan', 'K', 'Thomas', '1996-12-20', 'Denver', '9517538520');
```

---

## ✅ Task 2: Employee Table with Identity and Salary

### 🔧 Create Table `Employee1`

```sql
CREATE TABLE Employee1 (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50) NULL,
    LastName VARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    MobileNumber VARCHAR(10) NOT NULL,
    Address VARCHAR(100) NULL,
    Salary DECIMAL(10,2) NOT NULL
);
```

### 📥 Insert Records

```sql
INSERT INTO Employee1 (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary) VALUES 
('John', 'A', 'Doe', '1990-05-21', '9876543210', 'New York', 50000.00),
('Alice', NULL, 'Smith', '1985-08-15', '1234567890', 'Los Angeles', 60000.00),
('Bob', 'C', 'Williams', '1992-02-10', '5556667777', 'Chicago', 55000.00),
('David', 'M', 'Brown', '1988-11-25', '9988776655', 'Houston', 70000.00),
('Emma', NULL, 'Johnson', '1995-07-19', '7788994455', 'San Francisco', 62000.00),
('Michael', 'J', 'Miller', '1983-06-05', '6655443322', 'Boston', 80000.00),
('Sophia', 'R', 'Davis', '1991-09-30', '8899776655', 'Seattle', 48000.00),
('Liam', 'T', 'Wilson', '1994-04-10', '1122334455', 'Dallas', 53000.00),
('Olivia', NULL, 'Anderson', '1989-03-14', '2233445566', 'Atlanta', 75000.00),
('Ethan', 'K', 'Thomas', '1996-12-20', '3344556677', 'Denver', 67000.00);
```

---

## ✅ Task 3: Employee with Designation (Foreign Key Relationship)

### 🔧 Create `Designation` Table

```sql
CREATE TABLE Designation (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Designation VARCHAR(50) NOT NULL
);
```

### 📥 Insert Designations

```sql
INSERT INTO Designation (Designation) 
VALUES ('Software Engineer'), ('Project Manager'), ('HR');
```

### 🔧 Create `Employee2` Table with Foreign Key

```sql
CREATE TABLE Employee2 (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50) NULL,
    LastName VARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    MobileNumber VARCHAR(10) NOT NULL,
    Address VARCHAR(100) NULL,
    Salary DECIMAL(10,2) NOT NULL,
    DesignationId INT NULL,
    FOREIGN KEY (DesignationId) REFERENCES Designation(Id)
);
```

### 📥 Insert Records into `Employee2`

```sql
INSERT INTO Employee2 (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId) 
VALUES 
('John', 'A', 'Doe', '1990-05-10', '9876543210', '123 Street, NY', 75000, 1),
('Jane', NULL, 'Smith', '1985-08-15', '9123456789', '456 Road, LA', 90000, 2),
('Mike', 'B', 'Johnson', '1992-03-22', '9988776655', '789 Avenue, TX', 80000, 1),
('Sara', NULL, 'Wilson', '1995-07-18', '9776655443', NULL, 70000, 1);
```

