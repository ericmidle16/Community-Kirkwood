/* drop the database if it exists */
print '' print '** dropping database communityDb.sql'
GO
IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'communityDb')
BEGIN
    DROP DATABASE [communityDb]
END
GO

print '' print '** creating database communityDb'
GO
CREATE DATABASE [communityDb]
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO





