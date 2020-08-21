--Create Databse

--Create Database CategoryDb

--Ctaegory Table
Use CategoryDb
Create Table Category
(
	Id Int Identity(1,1) Primary Key,
	Name Varchar(100) Not Null
)
GO

--SubCategory Table

Create Table SubCategory
(
	Id Int Identity(1,1) Not null Primary Key,
	Name Varchar(30) Not null,
	CategoryId Int FOREIGN KEY REFERENCES Category(Id)
)
GO

Create Table Item
(
	Id Int Identity(1,1) Not null Primary Key,
	Name Varchar(30) Not null,
	Description Varchar(300) Not null,
	SubCategoryId Int FOREIGN KEY REFERENCES SubCategory(Id)
)
GO