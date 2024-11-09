-- ZMIANA TYPU KOLUMNY
-- ALTER TABLE [Dogs] ALTER COLUMN [DogDescription] [NTEXT];
-- ALTER TABLE [Dogs] ALTER COLUMN [DogMedicalDescription] [NTEXT];
-- ALTER TABLE [Dogs] ALTER COLUMN [DegreeDescription] [NTEXT];
-- ALTER TABLE [Dogs] ALTER COLUMN [AchievementsDescription] [NTEXT];

-- ZDJĘCIA PSÓW W HODOWLI

-- ALTER TABLE [DogImages] ADD COLUMN [ImageIndex] INT NOT NULL DEFAULT (0);
-- ALTER TABLE [DogImages] ADD COLUMN [ImageAlt] NVARCHAR(4000);

-- PIES W HODOWLI

DROP TABLE [Litters];
CREATE TABLE [Litters]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [BreedId] INT NOT NULL,
   [DogId] INT NOT NULL,   
   [BreedLink] NVARCHAR(4000),
   [DogLink] NVARCHAR(4000),
   [LitterLink] NVARCHAR(4000),
   [Letter] NVARCHAR(4000),
   [MainPicture] NVARCHAR(4000),
   [LitterPresentationName] NVARCHAR(4000),
   [MaleCount] INT NOT NULL,   
   [FemaleCount] INT NOT NULL,   
   [DogFather] NVARCHAR(4000),
   [DogFatherDegree] NVARCHAR(4000),
   [DogFatherLink] NVARCHAR(4000),
   [Description] NTEXT,
   [BornDate] NVARCHAR(4000),
   [BornDateOkay] DATETIME,
   [Tags] NVARCHAR(4000),   
   [BootstrapColumn] NVARCHAR(4000),
   [CreateDate] NVARCHAR(4000),
   [ModifiedDate] NVARCHAR(4000),
   [ShowsCount] INT NOT NULL,   
   [Visibility] BIT DEFAULT 0,
   [NavbarLitterName] NVARCHAR(4000),
   [NavbarLitterNameEng] NVARCHAR(4000),
   [NavbarEnabled] BIT DEFAULT 0,
   [NavbarVisibility] BIT DEFAULT 0,
   [MotherLitterDescription] NVARCHAR(4000),
   [FatherLitterDescription] NVARCHAR(4000),
   [DogFatherKennelNameFirst] BIT DEFAULT 0,
   [DogMotherKennelNameFirst] BIT DEFAULT 0,
   [FatherPictureName] NVARCHAR(4000)
);

ALTER TABLE [Litters] ADD CONSTRAINT [PK_dbo.Litters] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Litters] ([Id] ASC);
CREATE INDEX [Idx_BreedId] ON [Litters] ([BreedId] ASC);
CREATE INDEX [Idx_DogId] ON [Litters] ([DogId] ASC);
CREATE INDEX [Idx_BreedLink] ON [Litters] ([BreedLink] ASC);
CREATE INDEX [Idx_DogLink] ON [Litters] ([DogLink] ASC);
CREATE INDEX [Idx_LitterLink] ON [Litters] ([LitterLink] ASC);
CREATE INDEX [Idx_Letter] ON [Litters] ([Letter] ASC);
CREATE INDEX [Idx_Tags] ON [Litters] ([Tags] ASC);

-- 09.11.2022
ALTER TABLE [Litters] ADD COLUMN [MotherLitterDescription] NVARCHAR(4000);
ALTER TABLE [Litters] ADD COLUMN [FatherLitterDescription] NVARCHAR(4000);
ALTER TABLE [Litters] ADD COLUMN [DogFatherKennelNameFirst] BIT DEFAULT 0;
ALTER TABLE [Litters] ADD COLUMN [DogMotherKennelNameFirst] BIT DEFAULT 0;
ALTER TABLE [Litters] ADD COLUMN [FatherPictureName] NVARCHAR(4000);
-- ZDJĘCIA ZAPOWIEDZI W MIOTACH

DROP TABLE [LitterImages];
CREATE TABLE [LitterImages]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [BreedId] INT NOT NULL DEFAULT (0),
   [DogId] INT NOT NULL DEFAULT (0), 
   [LitterId] INT NOT NULL DEFAULT (0),   
   [BreedLink] NVARCHAR(4000),
   [DogLink] NVARCHAR(4000),
   [LitterLink] NVARCHAR(4000),
   [ImageFileName] NVARCHAR(4000),
   [ImageFileSize] NVARCHAR(4000),
   [ImageDate] NVARCHAR(4000),   
   [IsPublished] BIT DEFAULT 0,
   [ImageIndex] INT NOT NULL DEFAULT (0),   
   [ImageAlt] NVARCHAR(4000)   
);
ALTER TABLE [LitterImages] ADD CONSTRAINT [PK_dbo.LitterImages] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [LitterImages] ([Id] ASC);
CREATE INDEX [Idx_BreedId] ON [LitterImages] ([BreedId] ASC);
CREATE INDEX [Idx_DogId] ON [LitterImages] ([DogId] ASC);
CREATE INDEX [Idx_LitterId] ON [LitterImages] ([LitterId] ASC);
CREATE INDEX [Idx_BreedLink] ON [LitterImages] ([BreedLink] ASC);
CREATE INDEX [Idx_DogLink] ON [LitterImages] ([DogLink] ASC);
CREATE INDEX [Idx_LitterLink] ON [LitterImages] ([LitterLink] ASC);

-- SZCZENIĘTA DOPISANE DO MIOTÓW

DROP TABLE [Puppies];
CREATE TABLE [Puppies]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [LitterId] INT NOT NULL,
   [BreedId] INT NOT NULL,
   [DogId] INT NOT NULL,   
   [BreedLink] NVARCHAR(4000),
   [DogLink] NVARCHAR(4000),
   [LitterLink] NVARCHAR(4000),
   [PuppyLink] NVARCHAR(4000),   
   [MainPicture] NVARCHAR(4000),
   [PuppyName] NVARCHAR(4000),
   [PuppyAchievements] NVARCHAR(4000),
   [PuppyColour] NVARCHAR(4000),
   [PuppySex] BIT DEFAULT 0,
   [IsForBreeding] BIT DEFAULT 0,
   [IsForSale] BIT DEFAULT 0,
   [IsReproductor] BIT DEFAULT 0,
   [Defects] NVARCHAR(4000),
   [SpecialSigns] NVARCHAR(4000),
   [BornDate] NVARCHAR(4000),
   [BornWeight] INT NOT NULL,
   [DeathDate] NVARCHAR(4000),
   [CauseOfDeath] NVARCHAR(4000),
   [AdultWeight] INT NOT NULL,
   [AdultHeight] INT NOT NULL,
   [Country] NVARCHAR(4000),
   [City] NVARCHAR(4000),
   [Address] NVARCHAR(4000),   
   [FirstName] NVARCHAR(4000),
   [SurName] NVARCHAR(4000),
   [Phone] NVARCHAR(4000),
   [Email] NVARCHAR(4000),
   [Description] NTEXT,
   [DescriptionEng] NTEXT,
   [Url] NVARCHAR(4000),
   [BootstrapColumn] NVARCHAR(4000),
   [Tags] NVARCHAR(4000),      
   [CreateDate] NVARCHAR(4000),
   [ModifiedDate] NVARCHAR(4000),
   [ShowsCount] INT NOT NULL,   
   [Visibility] BIT DEFAULT 0,
   [NavbarPuppyName] NVARCHAR(4000),
   [NavbarPuppyNameEng] NVARCHAR(4000),
   [NavbarEnabled] BIT DEFAULT 0,
   [NavbarVisibility] BIT DEFAULT 0,
   [AvailableStatus] NVARCHAR(4000)
);

ALTER TABLE [Puppies] ADD CONSTRAINT [PK_dbo.Puppies] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Puppies] ([Id] ASC);
CREATE INDEX [Idx_LitterId] ON [Puppies] ([LitterId] ASC);
CREATE INDEX [Idx_BreedId] ON [Puppies] ([BreedId] ASC);
CREATE INDEX [Idx_DogId] ON [Puppies] ([DogId] ASC);
CREATE INDEX [Idx_BreedLink] ON [Puppies] ([BreedLink] ASC);
CREATE INDEX [Idx_DogLink] ON [Puppies] ([DogLink] ASC);
CREATE INDEX [Idx_LitterLink] ON [Puppies] ([LitterLink] ASC);
CREATE INDEX [Idx_PuppyLink] ON [Puppies] ([PuppyLink] ASC);
CREATE INDEX [Idx_Tags] ON [Puppies] ([Tags] ASC);

ALTER TABLE [Puppies] ADD COLUMN [FirstName] NVARCHAR(4000);
ALTER TABLE [Puppies] ADD COLUMN [SurName] NVARCHAR(4000);
ALTER TABLE [Puppies] ADD COLUMN [Phone] NVARCHAR(4000);
ALTER TABLE [Puppies] ADD COLUMN [Email] NVARCHAR(4000);

-- ZDJĘCIA SZCZENIĄT

DROP TABLE [PuppyImages];
CREATE TABLE [PuppyImages]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [BreedId] INT NOT NULL DEFAULT (0),   
   [DogId] INT NOT NULL DEFAULT (0),
   [LitterId] INT NOT NULL DEFAULT (0),
   [BreedLink] NVARCHAR(4000),
   [DogLink] NVARCHAR(4000),
   [LitterLink] NVARCHAR(4000),
   [PuppyLink] NVARCHAR(4000),
   [ImageFileName] NVARCHAR(4000),
   [ImageFileSize] NVARCHAR(4000),
   [ImageDate] NVARCHAR(4000),   
   [IsPublished] BIT DEFAULT 0,
   [ImageIndex] INT NOT NULL DEFAULT (0),   
   [ImageAlt] NVARCHAR(4000)   
);
ALTER TABLE [PuppyImages] ADD CONSTRAINT [PK_dbo.PuppyImages] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [PuppyImages] ([Id] ASC);
CREATE INDEX [Idx_BreedId] ON [PuppyImages] ([BreedId] ASC);
CREATE INDEX [Idx_DogId] ON [PuppyImages] ([DogId] ASC);
CREATE INDEX [Idx_LitterId] ON [PuppyImages] ([LitterId] ASC);
CREATE INDEX [Idx_BreedLink] ON [PuppyImages] ([BreedLink] ASC);
CREATE INDEX [Idx_DogLink] ON [PuppyImages] ([DogLink] ASC);
CREATE INDEX [Idx_LitterLink] ON [PuppyImages] ([LitterLink] ASC);
CREATE INDEX [Idx_PuppyLink] ON [PuppyImages] ([PuppyLink] ASC);