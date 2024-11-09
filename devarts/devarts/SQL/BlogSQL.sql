
-- WPISY NA BLOGU/PORTFOLIO

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


--ALTER TABLE [Posts] ADD COLUMN [BlogCathegory] NVARCHAR(4000);
--ALTER TABLE [Posts] ADD COLUMN [PortfolioCathegory] NVARCHAR(4000);
--ALTER TABLE [Posts] ADD COLUMN [MainPicture] NVARCHAR(4000);

  -- KOMENTARZE DO WPISÓW

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

-- ZDJĘCIA DO WPISÓW

--ALTER TABLE [PostImages] ADD COLUMN [ImageIndex] INT NOT NULL DEFAULT (0);
--ALTER TABLE [PostImages] ADD COLUMN [ImageAlt] NVARCHAR(4000);

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

-- NEWSLETTER

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

-- SUBSCRIBER

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































