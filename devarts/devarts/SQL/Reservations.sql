DROP TABLE [Reservations];
CREATE TABLE [Reservations]
(
   [Id] INT NOT NULL IDENTITY (1,1),   
   [CreateByClient] BIT DEFAULT 1,
   [Email] NVARCHAR(400),
   [FirstName] NVARCHAR(400),
   [SurName] NVARCHAR(400),
   [Country] NVARCHAR(400),
   [City] NVARCHAR(400),
   [PhoneNumber] NVARCHAR(400),
   [Breed] NVARCHAR(400),
   [Sex] NVARCHAR(400),
   [Colour] NVARCHAR(400),
   [EnergyLevel] NVARCHAR(400),
   [DogSize] NVARCHAR(400),
   [DogForKennel] BIT DEFAULT 0,
   [DogForSport] BIT DEFAULT 0,
   [PreferredMother] NVARCHAR(400),
   [PreferredFather] NVARCHAR(400),
   [ReadyToDog] NVARCHAR(400),
   [AdditionalResidents] NVARCHAR(4000),
   [Alergies] NVARCHAR(4000),   
   [ClientComments] NVARCHAR(4000),      
   [Priority] NVARCHAR(4000), 
   [BackendComments] NVARCHAR(4000),
   [PaymentStatus] NVARCHAR(4000),
   [DeliveryMethod] NVARCHAR(4000),
   [CreateDate] DATETIME,
   [ModifiedDate] DATETIME,
   [Rodo] BIT DEFAULT 0,
   [IsReaded] BIT DEFAULT 0,
   [IsClosed] BIT DEFAULT 0,
   [Password] NVARCHAR(4000),
   [TailLength] NVARCHAR(4000)
);
ALTER TABLE [Reservations] ADD CONSTRAINT [PK_dbo.Reservations] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Reservations] ([Id] ASC);
CREATE INDEX [Idx_SurName] ON [Reservations] ([SurName] ASC);
CREATE INDEX [Idx_City] ON [Reservations] ([City] ASC);

-- ALTERY
ALTER TABLE [Reservations] DROP COLUMN [Sex]
GO
ALTER TABLE [Reservations] ADD COLUMN [Sex] NVARCHAR(400)
GO

ALTER TABLE [Reservations] ADD COLUMN [IsReaded] BIT DEFAULT 0
GO

ALTER TABLE [Reservations] ADD COLUMN [Password] NVARCHAR(400);
--ALTER TABLE [Reproduction] ADD COLUMN [CalculateDone] BIT DEFAULT 0;
--ALTER TABLE [Reproduction] ADD COLUMN [CorrectDaysToEstrus] INT NOT NULL;
--ALTER TABLE [Reproduction] ADD COLUMN [CorrectDaysToRut] INT NOT NULL;
--ALTER TABLE [Reproduction] ADD COLUMN [CorrectDaysToPregnancy] INT NOT NULL;

--ALTER TABLE [Reproduction] DROP COLUMN [CalculateDone];
--ALTER TABLE [Reproduction] DROP COLUMN [CorrectDaysToEstrus];
--ALTER TABLE [Reproduction] DROP COLUMN [CorrectDaysToRut];
--ALTER TABLE [Reproduction] DROP COLUMN [CorrectDaysToPregnancy];

ALTER TABLE [Reservations] ADD COLUMN [TailLength] NVARCHAR(400);