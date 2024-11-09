
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 10/31/2018 00:01:05
-- Generated from EDMX file: C:\Biznesowe\SangueAzurro\sangueAzurro\Notatki\Baza danych.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    ALTER TABLE [webpages_UsersInRoles] DROP CONSTRAINT [fk_RoleId];
GO
    ALTER TABLE [webpages_UsersInRoles] DROP CONSTRAINT [fk_UserId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [__MigrationHistory];
GO
    DROP TABLE [Comments];
GO
    DROP TABLE [Entries];
GO
    DROP TABLE [Statistics];
GO
    DROP TABLE [Tracing];
GO
    DROP TABLE [UserProfile];
GO
    DROP TABLE [webpages_Membership];
GO
    DROP TABLE [webpages_OAuthMembership];
GO
    DROP TABLE [webpages_Roles];
GO
    DROP TABLE [webpages_UsersInRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] image  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CommentOwnerId] int  NOT NULL,
    [NickName] nvarchar(200)  NOT NULL,
    [CommentContent] ntext  NOT NULL,
    [Email] nvarchar(100)  NULL,
    [CommentDate] datetime  NOT NULL,
    [CommentRate] int  NOT NULL,
    [ShowComment] bit  NOT NULL,
    [HostIP] nvarchar(4000)  NULL
);
GO

-- Creating table 'Entries'
CREATE TABLE [Entries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuthorName] nvarchar(4000)  NOT NULL,
    [EntryName] nvarchar(4000)  NOT NULL,
    [CategoryName] nvarchar(4000)  NOT NULL,
    [EntryContent] ntext  NOT NULL,
    [EntryLink] nvarchar(4000)  NOT NULL,
    [Tags] nvarchar(4000)  NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryRate] int  NOT NULL,
    [HostIP] nvarchar(4000)  NULL,
    [ImageData] varbinary(4000)  NULL,
    [ImageMimeType] nvarchar(4000)  NULL,
    [haha] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'Statistics'
CREATE TABLE [Statistics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] nvarchar(4000)  NULL,
    [IpAddress] nvarchar(4000)  NULL,
    [Name] nvarchar(4000)  NULL,
    [DeviceType] int  NOT NULL,
    [Continent] nvarchar(4000)  NULL,
    [Country] nvarchar(4000)  NULL,
    [GeoLong] nvarchar(4000)  NULL,
    [GeoLat] nvarchar(4000)  NULL
);
GO

-- Creating table 'Tracing'
CREATE TABLE [Tracing] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Url] nvarchar(4000)  NULL,
    [HostAndIp] nvarchar(4000)  NULL,
    [DateTime] nvarchar(4000)  NULL,
    [DeviceType] int  NOT NULL
);
GO

-- Creating table 'UserProfile'
CREATE TABLE [UserProfile] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(56)  NOT NULL
);
GO

-- Creating table 'webpages_Membership'
CREATE TABLE [webpages_Membership] (
    [UserId] int  NOT NULL,
    [CreateDate] datetime  NULL,
    [ConfirmationToken] nvarchar(128)  NULL,
    [IsConfirmed] bit  NULL,
    [LastPasswordFailureDate] datetime  NULL,
    [PasswordFailuresSinceLastSuccess] int  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [PasswordChangedDate] datetime  NULL,
    [PasswordSalt] nvarchar(128)  NOT NULL,
    [PasswordVerificationToken] nvarchar(128)  NULL,
    [PasswordVerificationTokenExpirationDate] datetime  NULL
);
GO

-- Creating table 'webpages_OAuthMembership'
CREATE TABLE [webpages_OAuthMembership] (
    [Provider] nvarchar(30)  NOT NULL,
    [ProviderUserId] nvarchar(100)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'webpages_Roles'
CREATE TABLE [webpages_Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'webpages_UsersInRoles'
CREATE TABLE [webpages_UsersInRoles] (
    [webpages_Roles_RoleId] int  NOT NULL,
    [UserProfile_UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY ([MigrationId], [ContextKey] );
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Entries'
ALTER TABLE [Entries]
ADD CONSTRAINT [PK_Entries]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Statistics'
ALTER TABLE [Statistics]
ADD CONSTRAINT [PK_Statistics]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Tracing'
ALTER TABLE [Tracing]
ADD CONSTRAINT [PK_Tracing]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [UserId] in table 'UserProfile'
ALTER TABLE [UserProfile]
ADD CONSTRAINT [PK_UserProfile]
    PRIMARY KEY ([UserId] );
GO

-- Creating primary key on [UserId] in table 'webpages_Membership'
ALTER TABLE [webpages_Membership]
ADD CONSTRAINT [PK_webpages_Membership]
    PRIMARY KEY ([UserId] );
GO

-- Creating primary key on [Provider], [ProviderUserId] in table 'webpages_OAuthMembership'
ALTER TABLE [webpages_OAuthMembership]
ADD CONSTRAINT [PK_webpages_OAuthMembership]
    PRIMARY KEY ([Provider], [ProviderUserId] );
GO

-- Creating primary key on [RoleId] in table 'webpages_Roles'
ALTER TABLE [webpages_Roles]
ADD CONSTRAINT [PK_webpages_Roles]
    PRIMARY KEY ([RoleId] );
GO

-- Creating primary key on [webpages_Roles_RoleId], [UserProfile_UserId] in table 'webpages_UsersInRoles'
ALTER TABLE [webpages_UsersInRoles]
ADD CONSTRAINT [PK_webpages_UsersInRoles]
    PRIMARY KEY ([webpages_Roles_RoleId], [UserProfile_UserId] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [webpages_Roles_RoleId] in table 'webpages_UsersInRoles'
ALTER TABLE [webpages_UsersInRoles]
ADD CONSTRAINT [FK_webpages_UsersInRoles_webpages_Roles]
    FOREIGN KEY ([webpages_Roles_RoleId])
    REFERENCES [webpages_Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserProfile_UserId] in table 'webpages_UsersInRoles'
ALTER TABLE [webpages_UsersInRoles]
ADD CONSTRAINT [FK_webpages_UsersInRoles_UserProfile]
    FOREIGN KEY ([UserProfile_UserId])
    REFERENCES [UserProfile]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_webpages_UsersInRoles_UserProfile'
CREATE INDEX [IX_FK_webpages_UsersInRoles_UserProfile]
ON [webpages_UsersInRoles]
    ([UserProfile_UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------