IF EXISTS (SELECT name 
           FROM   sys.databases 
           WHERE  name = N'HanHomework') 
  DROP DATABASE hanhomework 
CREATE DATABASE hanhomework 
GO 

USE hanhomework 
CREATE TABLE data 
  ( 
    [Id] int PRIMARY KEY IDENTITY,
    [name] VARCHAR(50), 
    [book] VARCHAR(1000), 
    [page] VARCHAR(1000), 
    [date] DATE
  ) 
GO 

USE [master]
GO

EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2
GO

USE [master]
GO

CREATE LOGIN [handb] WITH PASSWORD=N'123456', DEFAULT_DATABASE=[hanhomework], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [hanhomework]
GO

CREATE USER [handb] FOR LOGIN [handb]
GO

USE [hanhomework]
GO

EXEC sp_addrolemember N'db_owner', N'handb'
GO
