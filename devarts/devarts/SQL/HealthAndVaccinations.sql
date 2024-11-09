DROP TABLE [HealthAndVaccinations];
CREATE TABLE [HealthAndVaccinations]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DogId] INT NOT NULL,
   [DogName] NVARCHAR(4000),   
   [Name] NVARCHAR(4000),
   [Type] NVARCHAR(4000),
   [Cathegory] NVARCHAR(4000),
   [Comment] NVARCHAR(4000),
   [Description] NVARCHAR(4000),
   [ProgressBar] NVARCHAR(4000),
   [IsRepeatable] BIT DEFAULT 0,
   [DaysToRepeat] INT NOT NULL,
   [NextDate] DATETIME,
   [CreateDate] DATETIME,
   [ModifiedDate] DATETIME   
);
ALTER TABLE [HealthAndVaccinations] ADD CONSTRAINT [PK_dbo.HealthAndVaccinations] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [HealthAndVaccinations] ([Id] ASC);
CREATE INDEX [Idx_DogName] ON [HealthAndVaccinations] ([DogName] ASC);
CREATE INDEX [Idx_Name] ON [HealthAndVaccinations] ([Name] ASC);