-- PRZYK£ADOWE POLECENIA SQL

   ALTER TABLE [Statistics] ADD Continent NVARCHAR(4000);
   ALTER TABLE [Statistics] ADD Country NVARCHAR(4000);
   ALTER TABLE [Statistics] ADD GeoLong NVARCHAR(4000);
   ALTER TABLE [Statistics] ADD GeoLat NVARCHAR(4000); 

-- TABELA STATYSTYK
CREATE TABLE [Statistics]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DateTime] NVARCHAR(4000),
   [IpAddress] NVARCHAR(4000),
   [Name] NVARCHAR(4000),
   [DeviceType] INT NOT NULL DEFAULT (0),
   [Continent] NVARCHAR(4000),
   [Country] NVARCHAR(4000),
   [GeoLong] NVARCHAR(4000),
   [GeoLat] NVARCHAR(4000)
);

ALTER TABLE [Statistics] ADD CONSTRAINT [PK_dbo.Statistics] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Statistics] ([Id] ASC);
CREATE INDEX [Idx_DeviceType] ON [Statistics] ([DeviceType] ASC);
CREATE INDEX [Idx_Continent] ON [Statistics] ([Continent] ASC);
CREATE INDEX [Idx_Country] ON [Statistics] ([Country] ASC);

-- ŒLEDZENIE TRAS U¯YTKOWNIKÓW
CREATE TABLE [Tracing]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [Url] NVARCHAR(4000),
   [HostAndIp] NVARCHAR(4000),
   [DateTime] NVARCHAR(4000),
   [DeviceType] INT NOT NULL DEFAULT (0)
);

ALTER TABLE [Tracing] ADD CONSTRAINT [PK_dbo.Tracing] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Tracing] ([Id] ASC);
CREATE INDEX [Idx_DeviceType] ON [Tracing] ([DeviceType] ASC);