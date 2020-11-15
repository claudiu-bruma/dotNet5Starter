/*
   Saturday, April 13, 201910:54:35 AM
   User: 
   Server: DESKTOP-12S6IUV
   Database: GlassLewisCompanies
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Companies
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(500) NOT NULL,
	Exchange nvarchar(100) NOT NULL,
	StockTicker nvarchar(10) NOT NULL,
	ISIN nvarchar(50) NOT NULL,
	Website nvarchar(100) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Companies ADD CONSTRAINT
	PK_Companies PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Companies SET (LOCK_ESCALATION = TABLE)
GO
COMMIT