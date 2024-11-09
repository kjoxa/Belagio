-- Tabela główna finansów
DROP TABLE [Finances];
CREATE TABLE [Finances]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [DocumentId] INT,
   [Name] NVARCHAR(4000),
   [Description] NTEXT,
   [DateFrom] DATETIME,
   [DateTo] DATETIME,
   [Cathegory] NVARCHAR(4000),    
   [Amount] DECIMAL(7,2),
   [CurrencyName] NVARCHAR(4000),
   [CreateDate] DATETIME,
   [ModifiedDate] DATETIME,
   [IsExpense] BIT DEFAULT 1,
   [IncludeFinance] BIT DEFAULT 1,
   [Visibility] BIT DEFAULT 1
);
ALTER TABLE [Finances] ADD CONSTRAINT [PK_dbo.Finances] PRIMARY KEY ([Id]);
CREATE INDEX [Idx_Id] ON [Finances] ([Id] ASC);
CREATE INDEX [Idx_Name] ON [FinanceCathegoryNames] ([Name] ASC);
CREATE INDEX [Idx_DocumentId] ON [Finances] ([DocumentId] ASC);
CREATE INDEX [Idx_DateFrom] ON [Finances] ([DateFrom] ASC);
CREATE INDEX [Idx_DateTo] ON [Finances] ([DateTo] ASC);
CREATE INDEX [Idx_Cathegory] ON [Finances] ([Cathegory] ASC);

-- Tabela kategorii
DROP TABLE [FinanceCathegoryNames];

CREATE TABLE [FinanceCathegoryNames]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [Name] NVARCHAR(4000),
   [Description] NTEXT,
   [IsExpense] BIT DEFAULT 1,
   [CreateDate] DATETIME,
   [ModifiedDate] DATETIME,
   [Visibility] BIT DEFAULT 1
);

ALTER TABLE [FinanceCathegoryNames] ADD CONSTRAINT [PK_dbo.FinanceCathegoryNames] PRIMARY KEY ([Id]);
CREATE INDEX [Idx_Id] ON [FinanceCathegoryNames] ([Id] ASC);
CREATE INDEX [Idx_Name] ON [FinanceCathegoryNames] ([Name] ASC);


-- Tabela używanych wydatków
DROP TABLE [FinanceFovouritesNames];
CREATE TABLE [FinanceFovouritesNames]
(
   [Id] INT NOT NULL IDENTITY (1,1),
   [Name] NVARCHAR(4000),
   [Description] NTEXT,
   [IsExpense] BIT DEFAULT 1,
   [CreateDate] DATETIME,
   [ModifiedDate] DATETIME,
   [Visibility] BIT DEFAULT 1
);

ALTER TABLE [FinanceFovouritesNames] ADD CONSTRAINT [PK_dbo.FinanceFovouritesNames] PRIMARY KEY ([Id]);
CREATE INDEX [Idx_Id] ON [FinanceFovouritesNames] ([Id] ASC);
CREATE INDEX [Idx_Name] ON [FinanceFovouritesNames] ([Name] ASC);


-- DANE

-- Script Date: 24.11.2020 05:36  - ErikEJ.SqlCeScripting version 3.5.2.86
SET IDENTITY_INSERT [Finances] ON;

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
2,1,N'Smycz od producenta www.psiakarma.pl',N'Smycz od producenta www.psiakarma.pl',{ts '2020-11-07 07:50:26.953'},{ts '2020-11-07 07:50:26.953'},N'Ubranka',69.90,N'PLN',{ts '2020-11-07 07:50:26.953'},{ts '2020-11-07 07:50:26.953'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
3,1,N'Szczepienie miotu L',N'Szczepienie',{ts '2020-11-09 07:29:39.370'},{ts '2020-11-09 07:29:39.370'},N'Zdrowie',169.90,N'PLN',{ts '2020-11-09 07:29:39.370'},{ts '2020-11-09 07:29:39.370'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
4,1,N'Chip x 6',N'Chipy dla miotu L',{ts '2020-11-09 07:29:39.370'},{ts '2020-11-09 07:29:39.370'},N'Zdrowie',269.90,N'PLN',{ts '2020-11-09 07:29:39.813'},{ts '2020-11-09 07:29:39.813'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
6,1,N'Tabletki musujące na bekanie',N'Tabletki przeciw wzdęciom',{ts '2020-11-09 07:29:39.370'},{ts '2020-11-09 07:29:39.370'},N'Zdrowie',29.50,N'PLN',{ts '2020-11-09 07:29:39.847'},{ts '2020-11-09 07:29:39.847'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
7,1,N'Sprzedaż - Lefelello Maestro Latte',N'Wydanie psiaka do Gdańska na ul. Psią 8',{ts '2020-11-09 07:29:39.370'},{ts '2020-11-09 07:29:39.370'},N'Sprzedaż',5000.00,N'PLN',{ts '2020-11-17 07:29:39.867'},{ts '2020-11-17 07:29:39.867'},0,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
8,1,N'Quechua Base Seconds',N'Zakup spodu do namiotu',{ts '2020-11-09 07:29:39.883'},{ts '2020-11-09 07:29:39.883'},N'Hodowla',369.90,N'PLN',{ts '2020-11-16 07:29:39.883'},{ts '2020-11-16 07:29:39.883'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
9,1,N'Zapis psów na wystawę x 4',N'XIX Międzynarodowa Wystawa Psów Rasowych w Bydgoszczy',{ts '2020-11-09 07:29:39.900'},{ts '2020-11-09 07:29:39.900'},N'Wystawy',370.00,N'PLN',{ts '2020-11-09 07:29:39.900'},{ts '2020-11-09 07:29:39.900'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
10,1,N'Krycie Mateuszkiem',N'Krycie Mateuszkiem dla Bursztynowej Bony',{ts '2020-11-09 07:29:39.900'},{ts '2020-11-09 07:29:39.900'},N'Rozmnażanie',4000.00,N'PLN',{ts '2020-12-04 07:29:39.917'},{ts '2020-12-04 07:29:39.917'},0,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
11,1,N'Lea - krycie w Szczecinie',N'Wyjazd na krycie do Słowenii',{ts '2020-11-09 07:29:39.900'},{ts '2020-11-09 07:29:39.900'},N'Rozmnażanie',69.90,N'PLN',{ts '2020-11-22 07:29:39.933'},{ts '2020-11-25 07:29:39.933'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
12,1,N'Karma PUFFI',N'W czasach kryzysu - karma PUFFI metabolizm RUFFI!',{ts '2020-11-09 07:29:39.900'},{ts '2020-11-09 07:29:39.900'},N'Odżywianie',69.90,N'PLN',{ts '2020-11-14 07:29:39.950'},{ts '2020-11-19 07:29:39.950'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
13,1,N'Szczepienie miotu L',N'Szczepienie',{ts '2020-11-09 08:47:53.943'},{ts '2020-11-09 08:47:53.943'},N'Zdrowie',169.90,N'PLN',{ts '2020-11-09 08:47:53.943'},{ts '2020-11-09 08:47:53.943'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
14,1,N'Chip x 6',N'Chipy dla miotu L',{ts '2020-11-09 08:47:53.943'},{ts '2020-11-09 08:47:53.943'},N'Zdrowie',269.90,N'PLN',{ts '2020-11-09 08:47:53.960'},{ts '2020-11-09 08:47:53.960'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
15,1,N'Sprzedaż - Pruptello Maestro Cappuccino',N'Wydanie psiaka do Warszawy',{ts '2020-11-09 08:47:53.943'},{ts '2020-11-09 08:47:53.943'},N'Sprzedaż',6500.00,N'PLN',{ts '2020-11-09 08:47:53.980'},{ts '2020-11-09 08:47:53.980'},0,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
16,1,N'Tabletki musujące na bekanie',N'Tabletki przeciw wzdęciom',{ts '2020-11-09 08:47:53.943'},{ts '2020-11-09 08:47:53.943'},N'Zdrowie',29.50,N'PLN',{ts '2020-11-09 08:47:53.997'},{ts '2020-11-09 08:47:53.997'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
17,1,N'Sprzedaż - Lefelello Maestro Latte',N'Wydanie psiaka do Gdańska na ul. Psią 8',{ts '2020-11-09 08:47:53.943'},{ts '2020-11-09 08:47:53.943'},N'Sprzedaż',5000.00,N'PLN',{ts '2020-11-17 08:47:54.017'},{ts '2020-11-17 08:47:54.017'},0,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
18,1,N'Spód do namiotu',N'Zakup spodu do namiotu',{ts '2020-11-09 08:47:54.033'},{ts '2020-11-09 08:47:54.033'},N'Hodowla',369.90,N'PLN',{ts '2020-11-16 08:47:54.033'},{ts '2020-11-16 08:47:54.033'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
19,1,N'Zapis psów na wystawę x 4',N'XIX Międzynarodowa Wystawa Psów Rasowych w Bydgoszczy',{ts '2020-11-09 08:47:54.050'},{ts '2020-11-09 08:47:54.050'},N'Wystawy',370.00,N'PLN',{ts '2020-11-09 08:47:54.050'},{ts '2020-11-09 08:47:54.050'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
20,1,N'Krycie Mateuszkiem',N'Krycie Mateuszkiem dla Bursztynowej Bony',{ts '2020-11-09 08:47:54.050'},{ts '2020-11-09 08:47:54.050'},N'Rozmnażanie',4000.00,N'PLN',{ts '2020-12-04 08:47:54.070'},{ts '2020-12-04 08:47:54.070'},0,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
21,1,N'Skazka - Wyjazd na krycie do Słowenii',N'Wyjazd na krycie do Słowenii',{ts '2020-11-09 08:47:54.050'},{ts '2020-11-09 08:47:54.050'},N'Rozmnażanie',69.90,N'PLN',{ts '2020-11-22 08:47:54.087'},{ts '2020-11-25 08:47:54.087'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
22,1,N'Karma PUFFI',N'W czasach kryzysu - karma PUFFI metabolizm RUFFI!',{ts '2020-11-09 08:47:54.050'},{ts '2020-11-09 08:47:54.050'},N'Odżywianie',69.90,N'PLN',{ts '2020-11-14 08:47:54.103'},{ts '2020-11-19 08:47:54.103'},1,1,1);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
23,0,N'Gryzaki',N'Gryzaki dla Nanuszki',{ts '2020-11-17 16:01:00.000'},{ts '2020-11-10 16:01:00.000'},N'Odżywianie',59.00,N'PLN',{ts '2020-11-17 16:25:26.400'},{ts '2020-11-17 16:25:26.400'},1,1,0);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
24,0,N'Ubranko',N'Ubranko dla Nanuszki',{ts '2020-11-17 19:32:00.000'},{ts '2020-11-17 19:32:00.000'},N'Hodowla',30.00,N'PLN',{ts '2020-11-17 19:32:52.707'},{ts '2020-11-17 19:32:52.707'},1,1,0);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
31,0,N'werwe',N'werwer',{ts '2020-11-04 19:47:00.000'},{ts '2020-11-11 19:47:00.000'},N'Hodowla',45.00,N'EUR',{ts '2020-11-17 19:48:07.737'},{ts '2020-11-17 19:48:07.737'},1,1,0);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
32,0,N'werwe',N'werwer',{ts '2020-11-04 19:47:00.000'},{ts '2020-11-11 19:47:00.000'},N'Hodowla',45.00,N'EUR',{ts '2020-11-17 19:48:16.537'},{ts '2020-11-17 19:48:16.537'},1,1,0);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
35,0,N'Poduszka dla Nanuszka',N'Poduszka',{ts '2020-11-18 19:59:00.000'},{ts '2020-11-16 19:59:00.000'},N'Hodowla',333.00,N'PLN',{ts '2020-11-17 20:00:01.500'},{ts '2020-11-17 20:00:01.500'},1,1,0);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
36,0,N'Kocyk dla Mateuszka',N'Kocyk dla Matiego',{ts '2020-11-10 20:05:00.000'},{ts '2020-11-11 20:05:00.000'},N'Hodowla',400.00,N'PLN',{ts '2020-11-17 20:05:38.297'},{ts '2020-11-17 20:05:38.297'},1,1,0);

INSERT INTO [Finances] ([Id],[DocumentId],[Name],[Description],[DateFrom],[DateTo],[Cathegory],[Amount],[CurrencyName],[CreateDate],[ModifiedDate],[IsExpense],[IncludeFinance],[Visibility]) VALUES (
37,0,N'Karma PEDIGRIPAŁ',N'Pedigripał dla PSÓW!',{ts '2020-11-11 20:08:00.000'},{ts '2020-11-11 20:08:00.000'},N'Odżywianie',560.00,N'PLN',{ts '2020-11-17 20:09:14.580'},{ts '2020-11-17 20:09:14.580'},1,1,0);

SET IDENTITY_INSERT [Finances] OFF;

ALTER TABLE [Finances] ALTER COLUMN [Id] IDENTITY (38,1);

