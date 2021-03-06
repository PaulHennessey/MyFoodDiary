USE [MyFoodDiary]
GO
/****** Object:  Table [dbo].[FoodItems]    Script Date: 02/25/2015 10:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](100) NOT NULL,
	[Quantity] [int] NULL,
	[Date] [datetime] NOT NULL,
	[UserId] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Favourites]    Script Date: 02/25/2015 10:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Favourites](
	[UserId] [int] NOT NULL,
	[Code] [varchar](100) NOT NULL,
	[Quantity] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 02/25/2015 10:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 02/25/2015 10:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 02/25/2015 10:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02/25/2015 10:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[UpdateFoodItem]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateFoodItem](@Id int, @Quantity int)	
AS

UPDATE 
	FoodItems
SET 
	Quantity = @Quantity
WHERE 
	Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[MergeFavourite]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MergeFavourite](@userId int, @id int, @quantity int)
AS

DECLARE @code VARCHAR(100)
SET @code = (SELECT fi.Code FROM FoodItems fi WHERE fi.Id = @id)

IF EXISTS (SELECT * FROM Favourites WHERE UserId = @userId AND Code = @code)
	UPDATE 
		Favourites
	SET 
		Quantity = @quantity
	WHERE 
		UserId = @userId
	AND
		Code = @code
ELSE
    INSERT INTO 
		Favourites (UserId, Code, Quantity)
	VALUES
		(@userId, @code, @quantity)
GO
/****** Object:  StoredProcedure [dbo].[InsertFoodItem]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertFoodItem](@Code varchar(255), @quantity int, @dt datetime, @userId int)	
AS
	
INSERT INTO 
	FoodItems
	(Code, Quantity, Date, UserId)
VALUES
	(@Code, @quantity, @dt, @userId)
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUser](@Email nvarchar(50))	
AS

SELECT 
	UserId, Email, FirstName, LastName
FROM 
	Users
WHERE
	Email = @Email
GO
/****** Object:  StoredProcedure [dbo].[GetFoodItems]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetFoodItems](@dt datetime, @userId int)	
AS

SELECT 
	f.Id, f.Code, l.[Food Name] AS Name, f.Quantity, f.Date, l.ServingSize
FROM 
	FoodItems f
INNER JOIN
	CoFIDS.dbo.Labelling l
ON
	f.Code = l.[Food Code]
WHERE
	f.Date = @dt
AND 
	f.UserId = @userId
GO
/****** Object:  StoredProcedure [dbo].[GetFoodItem]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetFoodItem](@Code nvarchar(255))	
AS

SELECT 
	f.Id, f.Code, l.[Food Name] AS Name, f.Quantity, f.Date, l.ServingSize
FROM 
	FoodItems f
INNER JOIN
	CoFIDS.dbo.Labelling l
ON
	f.Code = l.[Food Code]
WHERE
	f.Code = @Code
GO
/****** Object:  StoredProcedure [dbo].[GetFavourites]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetFavourites](@userId int)	
AS

SELECT 
	f.Code, f.Quantity, l.[Food Name] AS Name
FROM 
	Favourites f
INNER JOIN
	CoFIDS.dbo.Labelling l
ON
	f.Code = l.[Food Code]
WHERE
	f.UserId = @userId
GO
/****** Object:  StoredProcedure [dbo].[DeleteFoodItem]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteFoodItem](@Id int)	
AS

DELETE FROM
	FoodItems
WHERE 
	Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[DeleteFavourite]    Script Date: 02/25/2015 10:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Auhor:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteFavourite](@userId int, @code varchar(100))	
AS

DELETE FROM
	Favourites
WHERE 
	UserId = @userId
AND
	Code = @code
GO
/****** Object:  Default [DF__webpages___IsCon__20C1E124]    Script Date: 02/25/2015 10:58:23 ******/
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
/****** Object:  Default [DF__webpages___Passw__21B6055D]    Script Date: 02/25/2015 10:58:23 ******/
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO
