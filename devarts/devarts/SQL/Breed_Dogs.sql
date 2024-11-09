-- ZMIANA TYPU KOLUMNY
--ALTER TABLE [Dogs] ALTER COLUMN [DogDescription] [NTEXT];
--ALTER TABLE [Dogs] ALTER COLUMN [DogMedicalDescription] [NTEXT];
--ALTER TABLE [Dogs] ALTER COLUMN [DegreeDescription] [NTEXT];
--ALTER TABLE [Dogs] ALTER COLUMN [AchievementsDescription] [NTEXT];

-- ZDJĘCIA PSÓW W HODOWLI

-- ALTER TABLE [DogImages] ADD COLUMN [ImageIndex] INT NOT NULL DEFAULT (0);
-- ALTER TABLE [DogImages] ADD COLUMN [ImageAlt] NVARCHAR(4000);
-- ALTER TABLE [Dogs] ADD COLUMN [IsForBreeding] BIT DEFAULT 1;
-- ALTER TABLE [Dogs] ADD COLUMN [IsReproductor] BIT DEFAULT 1;
-- ALTER TABLE [Dogs] DROP COLUMN [IsBreeding];

-- WSZYSTKIE RASY PSÓW
DROP TABLE [AllBreeds];
CREATE TABLE [AllBreeds]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DogNameEng] NVARCHAR(4000),
   [DogNamePl] NVARCHAR(4000)
);
ALTER TABLE [AllBreeds] ADD CONSTRAINT [PK_dbo.AllBreeds] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [AllBreeds] ([Id] ASC);

-- RASA PSA

DROP TABLE [DogBreeds];
CREATE TABLE [DogBreeds]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [BreedSpeciesName] NVARCHAR(4000),

   [BreedSpeciesDisplayName] NVARCHAR(4000),
   [BreedSpeciesDisplayNameEng] NVARCHAR(4000),
   [BreedDescription] NVARCHAR(4000),
   
   [BreedLink] NVARCHAR(4000),   
   [CreateDate] NVARCHAR(4000),    
   [Visibility] BIT DEFAULT 1
);
ALTER TABLE [DogBreeds] ADD CONSTRAINT [PK_dbo.DogBreeds] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [DogBreeds] ([Id] ASC);
CREATE INDEX [Idx_BreedLinkBreeds] ON [DogBreeds] ([BreedLink] ASC);

-- PIES W HODOWLI

DROP TABLE [Dogs];
CREATE TABLE [Dogs]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [SpeciesId] INT NOT NULL,
   [ImageId] INT,
   [MainPicture] NVARCHAR(4000),
   [BreedLink] NVARCHAR(4000),
   [DogLink] NVARCHAR(4000),

   [DogName] NVARCHAR(4000),
   [DogPetName] NVARCHAR(4000),
   [BornDate] NVARCHAR(4000),
   [BornDateDateTime] DATETIME,
   [DeathDate] NVARCHAR(4000),
   [CauseOfDeath] NVARCHAR(4000),
   [DogColour] NVARCHAR(4000),   
   [DogSex] BIT DEFAULT 0, -- false:suka, true:pies
   [DogDescription] NTEXT,

   [Height] NVARCHAR(4000),
   [Weight] NVARCHAR(4000),
   [CoursingLicenceNumber] NVARCHAR(4000),
   [BloodlineNumber] NVARCHAR(4000),
   [DepartmentNumber] NVARCHAR(4000),
   [ChipNumber] NVARCHAR(4000),
   
   [BreedName] NVARCHAR(4000),
   [OwnerName] NVARCHAR(4000),
   [OwnerContact] NVARCHAR(4000),
   [BreedArchiveUrl] NVARCHAR(4000),
   [DogFatherName] NVARCHAR(4000),
   [DogMotherName] NVARCHAR(4000),
   [DogFatherBreedName] NVARCHAR(4000),
   [DogMotherBreedName] NVARCHAR(4000),
   [DogFatherUrl] NVARCHAR(4000),
   [DogMotherUrl] NVARCHAR(4000),
   [DogFatherCountry] NVARCHAR(4000),
   [DogMotherCountry] NVARCHAR(4000),
   [DogFatherCity] NVARCHAR(4000),
   [DogMotherCity] NVARCHAR(4000),   
   [DogMedicalDescription] NTEXT,
   [DegreeDescription] NTEXT, --- Zmienić na titles
   [AchievementsDescription] NTEXT,
   [Visibility] BIT DEFAULT 0,
   [IsRetired] BIT DEFAULT 0,
   [IsForBreeding] BIT DEFAULT 1,
   [IsReproductor] BIT DEFAULT 1,
   [CreateDate] NVARCHAR(4000),
   [ShowCount] INT,
   [Tags] NVARCHAR(4000),

   [NavbarDogName] NVARCHAR(4000),
   [NavbarDogNameEng] NVARCHAR(4000),
   [NavbarEnabled] BIT DEFAULT 1,
   [NavbarVisible] BIT DEFAULT 1,      
   [ModifiedDate] NVARCHAR(4000), 

   [Coowner] BIT DEFAULT 0,
   [CoownerFirstName] NVARCHAR(4000),
   [CoownerSurName] NVARCHAR(4000),
   [CoownerAddress] NVARCHAR(4000),
   [CoownerContact] NVARCHAR(4000),

   [DogKennelNameFirst] BIT DEFAULT 0,
   [DogFatherKennelNameFirst] BIT DEFAULT 0,
   [DogMotherKennelNameFirst] BIT DEFAULT 0
);

ALTER TABLE [Dogs] ADD CONSTRAINT [PK_dbo.Dogs] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Dogs] ([Id] ASC);
CREATE INDEX [Idx_SpeciesId] ON [Dogs] ([SpeciesId] ASC);
CREATE INDEX [Idx_ImageId] ON [Dogs] ([ImageId] ASC);
CREATE INDEX [Idx_MainPicture] ON [Dogs] ([MainPicture] ASC);
CREATE INDEX [Idx_BreedLinkDogs] ON [Dogs] ([BreedLink] ASC);
CREATE INDEX [Idx_DogLink] ON [Dogs] ([DogLink] ASC);

-- // dogs altery

ALTER TABLE [Dogs] ADD COLUMN [Coowner] BIT DEFAULT 0;
ALTER TABLE [Dogs] ADD COLUMN [CoownerFirstName] NVARCHAR(4000);
ALTER TABLE [Dogs] ADD COLUMN [CoownerSurName] NVARCHAR(4000);
ALTER TABLE [Dogs] ADD COLUMN [CoownerAddress] NVARCHAR(4000);
ALTER TABLE [Dogs] ADD COLUMN [CoownerContact] NVARCHAR(4000);
ALTER TABLE [Dogs] ADD COLUMN [BornDateDateTime] DATETIME;

-- 09.11.2022
ALTER TABLE [Dogs] ADD COLUMN [DogKennelNameFirst] BIT DEFAULT 0;
ALTER TABLE [Dogs] ADD COLUMN [DogFatherKennelNameFirst] BIT DEFAULT 0;
ALTER TABLE [Dogs] ADD COLUMN [DogMotherKennelNameFirst] BIT DEFAULT 0;

-- ZDJĘCIA PSÓW W HODOWLI

DROP TABLE [DogImages];
CREATE TABLE [DogImages]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DogId] INT NOT NULL DEFAULT (0),   
   [DogLink] NVARCHAR(4000),
   [ImageFileName] NVARCHAR(4000),
   [ImageFileSize] NVARCHAR(4000),
   [ImageDate] NVARCHAR(4000),   
   [IsPublished] BIT DEFAULT 0,
   [ImageIndex] INT NOT NULL DEFAULT (0),   
   [ImageAlt] NVARCHAR(4000)   
);
ALTER TABLE [DogImages] ADD CONSTRAINT [PK_dbo.DogImages] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [DogImages] ([Id] ASC);
CREATE INDEX [Idx_DogId] ON [DogImages] ([DogId] ASC);
CREATE INDEX [Idx_DogLink] ON [DogImages] ([DogLink] ASC);

-- DOKUMENTY HODOWLI

DROP TABLE [Documents];
CREATE TABLE [Documents]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DocumentName] NVARCHAR(4000),
   [Description] NVARCHAR(4000),
   [Type] NVARCHAR(4000),
   [Url] NVARCHAR(4000),
   [DocumentDate] NVARCHAR(4000),   
   [DocumentVersion] NVARCHAR(4000), 
   [UploadDate] NVARCHAR(4000),
   [IsActual] BIT DEFAULT 1
);
ALTER TABLE [Documents] ADD CONSTRAINT [PK_dbo.Documents] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Documents] ([Id] ASC);
CREATE INDEX [Idx_Type] ON [Documents] ([Type] ASC);

-- SUKI W MIOTACH

DROP TABLE [Bitch];
CREATE TABLE [Bitch]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DogId] INT,
   [DogName] NVARCHAR(4000),
   [EstrusStartDate] NVARCHAR(4000),   
   [EstrusEndDate] NVARCHAR(4000),   
   [RutStartDate] NVARCHAR(4000),
   [RutEndDate] NVARCHAR(4000),
   [ProgessteronBestDay] NVARCHAR(4000),
   [ProgessteronBestVal] NVARCHAR(4000),   
   [Comment] NTEXT,
   [IsSuccess] BIT DEFAULT 0
);
ALTER TABLE [Bitch] ADD CONSTRAINT [PK_dbo.Bitch] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Bitch] ([Id] ASC);
CREATE INDEX [Idx_DogId] ON [Bitch] ([DogId] ASC);
CREATE INDEX [Idx_DogName] ON [Bitch] ([DogName] ASC);

-- SZCZENIAKI DOPISANE DO SUKI

DROP TABLE [PuppieOfBitch];
CREATE TABLE [PuppieOfBitch]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [BitchId] INT,
   [BitchName] NVARCHAR(4000),
   [Comment] NVARCHAR(4000),   
   [MeasurementDate] NVARCHAR(4000),   
   [Weight] NVARCHAR(4000),
   [Height] NVARCHAR(4000),
   [Width] NVARCHAR(4000),
   [Behaviour] NVARCHAR(4000),   
   [Attributes] NVARCHAR(4000),
   [IsDeath] BIT DEFAULT 0
);
ALTER TABLE [PuppieOfBitch] ADD CONSTRAINT [PK_dbo.PuppieOfBitch] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [PuppieOfBitch] ([Id] ASC);
CREATE INDEX [Idx_BitchId] ON [PuppieOfBitch] ([BitchId] ASC);
CREATE INDEX [Idx_BitchName] ON [PuppieOfBitch] ([BitchName] ASC);