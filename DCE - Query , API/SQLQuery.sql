-- CREATE DB
CREATE DATABASE DCE;

-- CREATE CUSTOMER TABLE
CREATE TABLE Customer (
    UserId UNIQUEIDENTIFIER PRIMARY KEY,
    Username NVARCHAR(30),
    Email NVARCHAR(20),
    FirstName NVARCHAR(20),
    LastName NVARCHAR(20),
    CreatedOn DATETIME,
    IsActive BIT
);

-- CREATE SUPPLIER TABLE
CREATE TABLE Supplier (
    SupplierId UNIQUEIDENTIFIER PRIMARY KEY,
    SupplierName NVARCHAR(50),
    CreatedOn DATETIME,
    IsActive BIT
);

-- CREATE PRODUCT TABLE
CREATE TABLE Product (
    ProductId UNIQUEIDENTIFIER PRIMARY KEY,
    ProductName NVARCHAR(50),
    UnitPrice DECIMAL,
    SupplierId UNIQUEIDENTIFIER,
    CreatedOn DATETIME,
    IsActive BIT,
    FOREIGN KEY (SupplierId) REFERENCES Supplier(SupplierId)
);

-- CREATE ORDERS TABLE
CREATE TABLE Orders (
    OrderId UNIQUEIDENTIFIER PRIMARY KEY,
    ProductId UNIQUEIDENTIFIER,
    OrderStatus INT,
    OrderType INT,
    OrderBy UNIQUEIDENTIFIER,
    OrderedOn DATETIME,
    ShippedOn DATETIME,
    IsActive BIT,
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (OrderBy) REFERENCES Customer(UserId)
);

-- INSERT INTO CUSTOMER
INSERT INTO Customer (UserId, Username, Email, FirstName, LastName, CreatedOn, IsActive)
VALUES (NEWID(), 'john_doe', 'john.doe@email.com', 'John', 'Doe', GETDATE(), 1);

-- INSERT INTO SUPPLIER
INSERT INTO Supplier (SupplierId, SupplierName, CreatedOn, IsActive)
VALUES (NEWID(), 'SupplierX', GETDATE(), 1);


-- INSERT INTO PRODUCT
INSERT INTO Product (ProductId, ProductName, UnitPrice, SupplierId, CreatedOn, IsActive)
VALUES (NEWID(), 'ProductA', 19.99, (SELECT SupplierId FROM Supplier WHERE SupplierId = '423E346F-260A-4DFF-AA15-9962C368478C'), GETDATE(), 1);

-- INSERT INTO ORDERS
INSERT INTO Orders (OrderId, ProductId, OrderStatus, OrderType, OrderBy, OrderedOn, ShippedOn, IsActive)
VALUES (NEWID(), (SELECT ProductId FROM Product WHERE ProductId = '7796E92D-979C-499B-9D5F-5DB9F9A1E4C3'), 1, 1, (SELECT UserId FROM Customer WHERE UserId = '521994B7-F4DC-412B-BEAB-0269AA779EF3'), GETDATE(), GETDATE(), 1);

-- STORED PROCEDURE FOR GET ACTIVE ORDERS
CREATE PROCEDURE GetActiveOrdersByCustomer
    @UserId NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM Orders
    WHERE UserId = @UserId
      AND IsActive = 1; 
END


