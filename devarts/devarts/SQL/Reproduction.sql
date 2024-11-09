DROP TABLE [Reproduction];
CREATE TABLE [Reproduction]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DogId] INT,

   [DogName] NVARCHAR(4000),
   [EstrusStartDate] DATETIME,   
   [EstrusEndDate] DATETIME,   
   [RutStartDate] DATETIME,
   [RutEndDate] DATETIME,

   [ProgessteronBestDay] NVARCHAR(4000),
   [ProgessteronBestVal] NVARCHAR(4000),
   
   [FatherName] NVARCHAR(4000),   
   [FatherBreederName] NVARCHAR(4000),
   [FatherFromMyHome] BIT DEFAULT 0,

   [MatingDate_First] DATETIME,   
   [MatingDate_Second] DATETIME,   
   [MatingDate_Third] DATETIME,
   
   [EstimationBornDate] DATETIME,   
   [NextEstrusDate] DATETIME,
   [DateOfBorn] DATETIME,   

   [WeightBeforeBorn] INT DEFAULT 0,
   [CountOfBornPuppies] INT DEFAULT 0,
   [CorrectDaysToEstrus] INT NOT NULL,
   [CorrectDaysToRut] INT NOT NULL,
   [CorrectDaysToPregnancy] INT NOT NULL,

   [Comment] NTEXT,
   [CommentDate] NTEXT,
   [CommentForBorn] NTEXT,
   [Caesarean] BIT DEFAULT 0,
   [IsSuccess] BIT DEFAULT 0,
   [CalculateDone] BIT DEFAULT 0
);
ALTER TABLE [Reproduction] ADD CONSTRAINT [PK_dbo.Reproduction] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Reproduction] ([Id] ASC);
CREATE INDEX [Idx_DogId] ON [Reproduction] ([DogId] ASC);
CREATE INDEX [Idx_DogName] ON [Reproduction] ([DogName] ASC);

-- ALTERY

--ALTER TABLE [Reproduction] ADD COLUMN [CalculateDone] BIT DEFAULT 0;
--ALTER TABLE [Reproduction] ADD COLUMN [CorrectDaysToEstrus] INT NOT NULL;
--ALTER TABLE [Reproduction] ADD COLUMN [CorrectDaysToRut] INT NOT NULL;
--ALTER TABLE [Reproduction] ADD COLUMN [CorrectDaysToPregnancy] INT NOT NULL;

--ALTER TABLE [Reproduction] DROP COLUMN [CalculateDone];
--ALTER TABLE [Reproduction] DROP COLUMN [CorrectDaysToEstrus];
--ALTER TABLE [Reproduction] DROP COLUMN [CorrectDaysToRut];
--ALTER TABLE [Reproduction] DROP COLUMN [CorrectDaysToPregnancy];