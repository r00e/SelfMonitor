IF EXISTS (SELECT name 
           FROM   sys.databases 
           WHERE  name = N'HanHomework') 
  DROP DATABASE hanhomework 

CREATE DATABASE hanhomework 

go 

USE hanhomework 

CREATE TABLE data 
  ( 
     [name] VARCHAR(50), 
     [book] VARCHAR(1000), 
     [page] VARCHAR(1000), 
     [date] DATE 
  ) 

go 