SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Companies](
    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[WebName] nvarchar(50) NOT NULL,
    [DefaultReservationSettings] nvarchar(max)
)
GO

CREATE UNIQUE INDEX UI_Company_WebName
   ON [dbo].[Companies] (WebName);
GO