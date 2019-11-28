Create table [dbo].[group]
(
	[id] INT NOT NULL PRIMARY KEY Identity(1, 1),
	[name] nvarchar(10) not null
)

CREATE TABLE [dbo].[User]
(
	[id] INT NOT NULL PRIMARY KEY Identity(1, 1),
	[name] nvarchar(20) not null,
	[pass] nvarchar(20),
	[group] int not null
)

INSERT INTO [dbo].[group] ([name]) VALUES (N"admin")

INSERT INTO [dbo].[User] ([name], [pass], [group]) VALUES (N"root", N"root", 1)