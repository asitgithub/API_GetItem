USE [CategoryDb]
GO

INSERT INTO [dbo].[Category]   ([Name])
     VALUES ('Electronics'), ('Sports'), ('Food')
GO

INSERT INTO [dbo].[SubCategory]
           ([Name]
           ,[CategoryId])
     VALUES
			('Mobiles',1),
			('Refrigerator',1),
			('Cricket',2),
			('Tennis',2),
			('BreakFast',3),
			('Dairy', 3)
GO

INSERT INTO [dbo].[Item]
           ([Name]
           ,[Description]
           ,[SubCategoryId])
     VALUES
		('OnePlus8', 'One Plus 8 Mobile', 1),
		('iPhone10', 'AppleMobile', 1),
		('SonyXperia', 'Sony Smartphone', 1),
		('Whirlpool323 L', 'Double Door Refrigerator',2),
		('CricketBall', 'Cricket Ball',3),
		('TennisRaquet', 'Lawn tennis RacQuet',4),
		('Oats', 'BreakFast',5),
		('CornFlakes', 'BreakFast',5),
		('Milk', 'BreakFast',6),
		('Curd', 'BreakFast',6)
GO

Select * from  [dbo].[Category]
Select * from  [dbo].[SubCategory]
Select *from [dbo].[Item]


