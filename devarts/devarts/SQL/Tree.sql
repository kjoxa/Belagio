DROP TABLE [Trees];
CREATE TABLE [Trees]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DogId] INT,
   [LitterId] INT,
   [DogLink] NVARCHAR(4000),
   [LitterLink] NVARCHAR(4000),
   [CreateDate] DATETIME,
   [EditDate] DATETIME,
   
   [DogTreeBox] NVARCHAR(4000),
   [DogTreeTooltip] NVARCHAR(4000),
   [DogTreeTooltip_Image] NVARCHAR(4000),

   [A_DogTreeBox_Father_1] NVARCHAR(4000),
   [A_DogTreeTooltip_Father_1] NVARCHAR(4000),
   [A_DogTreeImage_Father_1] NVARCHAR(4000),
   [A_DogTreeBox_Mother_2] NVARCHAR(4000),
   [A_DogTreeTooltip_Mother_2] NVARCHAR(4000),
   [A_DogTreeImage_Mother_2] NVARCHAR(4000),
   
   [B_DogTreeBox_Father_3] NVARCHAR(4000),
   [B_DogTreeTooltip_Father_3] NVARCHAR(4000),
   [B_DogTreeImage_Father_3] NVARCHAR(4000),
   [B_DogTreeBox_Mother_4] NVARCHAR(4000),
   [B_DogTreeTooltip_Mother_4] NVARCHAR(4000),
   [B_DogTreeImage_Mother_4] NVARCHAR(4000),
   [B_DogTreeBox_Father_5] NVARCHAR(4000),
   [B_DogTreeTooltip_Father_5] NVARCHAR(4000),
   [B_DogTreeImage_Father_5] NVARCHAR(4000),
   [B_DogTreeBox_Mother_6] NVARCHAR(4000),
   [B_DogTreeTooltip_Mother_6] NVARCHAR(4000),
   [B_DogTreeImage_Mother_6] NVARCHAR(4000),

   [C_DogTreeBox_Father_7] NVARCHAR(4000),
   [C_DogTreeTooltip_Father_7] NVARCHAR(4000),
   [C_DogTreeImage_Father_7] NVARCHAR(4000),
   [C_DogTreeBox_Mother_8] NVARCHAR(4000),
   [C_DogTreeTooltip_Mother_8] NVARCHAR(4000),
   [C_DogTreeImage_Mother_8] NVARCHAR(4000),
   [C_DogTreeBox_Father_9] NVARCHAR(4000),
   [C_DogTreeTooltip_Father_9] NVARCHAR(4000),
   [C_DogTreeImage_Father_9] NVARCHAR(4000),
   [C_DogTreeBox_Mother_10] NVARCHAR(4000),
   [C_DogTreeTooltip_Mother_10] NVARCHAR(4000),
   [C_DogTreeImage_Mother_10] NVARCHAR(4000),
   [C_DogTreeBox_Father_11] NVARCHAR(4000),
   [C_DogTreeTooltip_Father_11] NVARCHAR(4000),
   [C_DogTreeImage_Father_11] NVARCHAR(4000),
   [C_DogTreeBox_Mother_12] NVARCHAR(4000),
   [C_DogTreeTooltip_Mother_12] NVARCHAR(4000),
   [C_DogTreeImage_Mother_12] NVARCHAR(4000),
   [C_DogTreeBox_Father_13] NVARCHAR(4000),
   [C_DogTreeTooltip_Father_13] NVARCHAR(4000),
   [C_DogTreeImage_Father_13] NVARCHAR(4000),
   [C_DogTreeBox_Mother_14] NVARCHAR(4000),
   [C_DogTreeTooltip_Mother_14] NVARCHAR(4000),
   [C_DogTreeImage_Mother_14] NVARCHAR(4000),

   [GenerationsCount] INT DEFAULT 3,
   [Visible] BIT DEFAULT 0,
   [IsLitter] BIT DEFAULT 0
);
ALTER TABLE [Trees] ADD CONSTRAINT [PK_dbo.Trees] PRIMARY KEY ([Id]);

CREATE INDEX [Idx_Id] ON [Trees] ([Id] ASC);
CREATE INDEX [Idx_DogLink] ON [Trees] ([DogLink] ASC);

---
ALTER TABLE [Trees] ADD COLUMN [DogTreeTooltip_Image] NVARCHAR(4000);
ALTER TABLE [Trees] ADD COLUMN [IsLitter] BIT DEFAULT 0;
ALTER TABLE [Trees] ADD COLUMN [LitterLink] NVARCHAR(4000);
ALTER TABLE [Trees] ADD COLUMN [LitterId] INT;