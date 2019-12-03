if not exists (select * from sys.tables where name='group')
begin
create table [dbo].[group]
(
	[id] INT NOT NULL PRIMARY KEY Identity(1, 1),
	[name] nvarchar(10) not null
)
end
if not exists (select * from sys.tables where name='User')
begin
create table [dbo].[User]
(
	[id] INT NOT NULL PRIMARY KEY Identity(1, 1),
	[name] nvarchar(20) not null,
	[pass] nvarchar(20),
	[group] int not null
)
end

if not exists (select * from [group]) insert into [group]([name]) values (N'admin')

if not exists (select * from [User]) insert into [dbo].[User] ([name], [pass], [group]) VALUES (N'root', N'root', 1)