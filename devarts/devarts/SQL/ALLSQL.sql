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
   [ModifiedDate] NVARCHAR(4000)
);
ALTER TABLE [Dogs] ADD CONSTRAINT [PK_dbo.Dogs] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Dogs] ([Id] ASC);
CREATE INDEX [Idx_SpeciesId] ON [Dogs] ([SpeciesId] ASC);
CREATE INDEX [Idx_ImageId] ON [Dogs] ([ImageId] ASC);
CREATE INDEX [Idx_MainPicture] ON [Dogs] ([MainPicture] ASC);
CREATE INDEX [Idx_BreedLinkDogs] ON [Dogs] ([BreedLink] ASC);
CREATE INDEX [Idx_DogLink] ON [Dogs] ([DogLink] ASC);


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
   [NavbarVisibility] BIT DEFAULT 0         
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
   [NavbarVisibility] BIT DEFAULT 0         
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

---------------------------------------------------------------------------------
---------------------------------------------------------------------------------
---------------------------------------------------------------------------------
---------------------------------------------------------------------------------
---------------------------------------------------------------------------------
-- ----------------------------SITEDB.SDF----------------------------------------

DROP TABLE [Posts];
CREATE TABLE [Posts]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [ImageId] INT,
   [MainPicture] NVARCHAR(4000),
   [AuthorName] NVARCHAR(4000),
   [PostName] NVARCHAR(4000),   
   [PostNameEng] NVARCHAR(4000),   
   [CategoryName] NVARCHAR(4000),
   [Type] NVARCHAR(4000),
   [PostContent] NTEXT,
   [PostContentShort] NTEXT,
   [PostContentEng] NTEXT,
   [PostContentShortEng] NTEXT,
   [PostLink] NVARCHAR(4000),
   [Tags] NVARCHAR(4000),
   [PostDate] NVARCHAR(4000),
   [PostDateOkay] DATETIME,
   [ModifiedDate] NVARCHAR(4000),
   [PostRate] INT NOT NULL DEFAULT (0),
   [PostShow] INT NOT NULL DEFAULT (0),
   [PostCommentsCount] INT NOT NULL DEFAULT (0),
   [PostLikes] INT NOT NULL DEFAULT (0),
   [AllowComments] BIT DEFAULT 1,
   [IsPublished] BIT DEFAULT 0,
   [BlogCathegory] NVARCHAR(4000),
   [PortfolioCathegory] NVARCHAR(4000),
   [InterviewCathegory] NVARCHAR(4000)
);
ALTER TABLE [Posts] ADD CONSTRAINT [PK_dbo.Posts] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Posts] ([Id] ASC);
CREATE INDEX [Idx_PostLink] ON [Posts] ([PostLink] ASC);
CREATE INDEX [Idx_CategoryName] ON [Posts] ([CategoryName] ASC);
CREATE INDEX [Idx_Type] ON [Posts] ([Type] ASC);


CREATE INDEX [Idx_BlogCathegory] ON [Posts] ([BlogCathegory] ASC);
CREATE INDEX [Idx_PortfolioCathegory] ON [Posts] ([PortfolioCathegory] ASC);
CREATE INDEX [Idx_InterviewCathegory] ON [Posts] ([InterviewCathegory] ASC);
CREATE INDEX [Idx_MainPicture] ON [Posts] ([MainPicture] ASC);


DROP TABLE [Comments];
CREATE TABLE [Comments]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [PostId] INT NOT NULL DEFAULT (0),
   [ParentCommentId] INT NOT NULL DEFAULT (0),
   [PostLink] NVARCHAR(4000),
   [Nick] NVARCHAR(4000),
   [Email] NVARCHAR(4000),
   [CommentDate] NVARCHAR(4000),
   [Notify] BIT DEFAULT 0, -- poinformuj mnie o odpowiedzi
   [IsPublished] BIT DEFAULT 0
);
ALTER TABLE [Comments] ADD CONSTRAINT [PK_dbo.Comments] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Comments] ([Id] ASC);
CREATE INDEX [Idx_PostId] ON [Comments] ([PostId] ASC);
CREATE INDEX [Idx_PostLink] ON [Comments] ([PostLink] ASC);


DROP TABLE [PostImages];
CREATE TABLE [PostImages]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [PostId] INT NOT NULL DEFAULT (0),   
   [PostLink] NVARCHAR(4000),
   [ImageFileName] NVARCHAR(4000),
   [ImageFileSize] NVARCHAR(4000),
   [ImageDate] NVARCHAR(4000),   
   [IsPublished] BIT DEFAULT 0,
   [ImageIndex] INT NOT NULL DEFAULT (0),   
   [ImageAlt] NVARCHAR(4000)  
);
ALTER TABLE [PostImages] ADD CONSTRAINT [PK_dbo.PostImages] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [PostImages] ([Id] ASC);
CREATE INDEX [Idx_PostId] ON [PostImages] ([PostId] ASC);
CREATE INDEX [Idx_PostLink] ON [PostImages] ([PostLink] ASC);


DROP TABLE [Newsletter];
CREATE TABLE [Newsletter]
(
   [Id] INT NOT NULL IDENTITY (1,1),   
   [Email] NVARCHAR(4000),
   [Subject] NVARCHAR(4000),
   [Content] NVARCHAR(4000),
   [CreateDate] NVARCHAR(4000),  
   [SendDate] NVARCHAR(4000),
   [IsActive] BIT DEFAULT 1
);
ALTER TABLE [Newsletter] ADD CONSTRAINT [PK_dbo.Newsletter] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Newsletter] ([Id] ASC);
CREATE INDEX [Idx_Email] ON [Newsletter] ([Email] ASC);
CREATE INDEX [Idx_CreateDate] ON [Newsletter] ([CreateDate] ASC);
CREATE INDEX [Idx_SendDate] ON [Newsletter] ([SendDate] ASC);


DROP TABLE [Subscriber];
CREATE TABLE [Subscriber]
(
   [Id] INT NOT NULL IDENTITY (1,1),   
   [Email] NVARCHAR(4000),
   [CreateDate] NVARCHAR(4000),     
   [IsActive] BIT DEFAULT 1
);
ALTER TABLE [Subscriber] ADD CONSTRAINT [PK_dbo.Subscriber] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Subscriber] ([Id] ASC);
CREATE INDEX [Idx_Email] ON [Subscriber] ([Email] ASC);
CREATE INDEX [Idx_CreateDate] ON [Subscriber] ([CreateDate] ASC);