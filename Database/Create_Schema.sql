CREATE TABLE dbo.FileMap
(
    Id       int         NOT NULL IDENTITY(1,1),
    FilePath varchar(50) NOT NULL,
    CONSTRAINT pk_FileMap PRIMARY KEY (Id)
);
GO

CREATE TABLE dbo.IdMap
(
    ReferenceId uniqueidentifier NOT NULL,
    Id          int              NOT NULL,
    IdType      int              NOT NULL,
    CONSTRAINT pk_IdMap PRIMARY KEY (ReferenceId)
);
GO

CREATE TABLE dbo.PhotoTheme
(
    Id          int IDENTITY(1,1) NOT NULL,
    Theme       varchar(50)            NOT NULL,
    ContestDate date DEFAULT getdate() NOT NULL,
    CONSTRAINT pk_PhotoTheme PRIMARY KEY (Id)
);
GO

CREATE TABLE dbo.Photographer
(
    Id           int IDENTITY(1,1) NOT NULL,
    UploaderName varchar(100) NOT NULL,
    CONSTRAINT pk_Photographer PRIMARY KEY (Id)
);
GO

CREATE TABLE dbo.PhotoEntry
(
    Id             int                            NOT NULL IDENTITY(1,1),
    ThemeId        int                            NOT NULL,
    PhotographerId int                            NOT NULL,
    FileId         int                            NOT NULL,
    Caption        varchar(100)                   NOT NULL,
    UploadedOn     datetime DEFAULT sysdatetime() NOT NULL,
    CONSTRAINT pk_PhotoEntry PRIMARY KEY (Id)
);
GO

CREATE TABLE dbo.PhotographerVoteDetails
(
    Id             int NOT NULL IDENTITY(1,1),
    ThemeId        int NOT NULL,
    PhotographerId int NOT NULL,
    FirstId        int NOT NULL,
    SecondId       int NULL,
    ThirdId        int NULL,
    CONSTRAINT pk_PhotographerVoteDetails PRIMARY KEY (Id)
);
GO

CREATE TABLE dbo.ScoreDetail
(
    Id      int           NOT NULL IDENTITY(1,1),
    EntryId int           NOT NULL,
    Score   int DEFAULT 0 NOT NULL,
    CONSTRAINT pk_ScoreDetail PRIMARY KEY (Id)
);
GO

CREATE TABLE AspNetRoles
(
    Id               nvarchar(450) NOT NULL,
    Name             nvarchar(256) NULL,
    NormalizedName   nvarchar(256) NULL,
    ConcurrencyStamp nvarchar( max) NULL,
    CONSTRAINT PK_AspNetRoles PRIMARY KEY (Id),
    INDEX            RoleNameIndex (NormalizedName)
);
GO

CREATE TABLE AspNetUsers
(
    Id                   nvarchar(450) NOT NULL,
    UserName             nvarchar(256) NULL,
    NormalizedUserName   nvarchar(256) NULL,
    Email                nvarchar(256) NULL,
    NormalizedEmail      nvarchar(256) NULL,
    EmailConfirmed       bit NOT NULL,
    PasswordHash         nvarchar( max) NULL,
    SecurityStamp        nvarchar( max) NULL,
    ConcurrencyStamp     nvarchar( max) NULL,
    PhoneNumber          nvarchar( max) NULL,
    PhoneNumberConfirmed bit NOT NULL,
    TwoFactorEnabled     bit NOT NULL,
    LockoutEnd           datetimeoffset(7) NULL,
    LockoutEnabled       bit NOT NULL,
    AccessFailedCount    int NOT NULL,
    CONSTRAINT PK_AspNetUsers PRIMARY KEY (Id),
    INDEX                UserNameIndex (NormalizedUserName)
);
GO

CREATE TABLE AspNetRoleClaims
(
    Id         int IDENTITY NOT NULL,
    RoleId     nvarchar(450) NOT NULL,
    ClaimType  nvarchar( max) NULL,
    ClaimValue nvarchar( max) NULL,
    CONSTRAINT PK_AspNetRoleClaims PRIMARY KEY (Id)
);
GO

CREATE TABLE AspNetUserClaims
(
    Id         int IDENTITY NOT NULL,
    UserId     nvarchar(450) NOT NULL,
    ClaimType  nvarchar( max) NULL,
    ClaimValue nvarchar( max) NULL,
    CONSTRAINT PK_AspNetUserClaims PRIMARY KEY (Id)
);
GO

CREATE TABLE AspNetUserLogins
(
    LoginProvider       nvarchar(450) NOT NULL,
    ProviderKey         nvarchar(450) NOT NULL,
    ProviderDisplayName nvarchar( max) NULL,
    UserId              nvarchar(450) NOT NULL,
    CONSTRAINT PK_AspNetUserLogins PRIMARY KEY (LoginProvider, ProviderKey)
);
GO

CREATE TABLE AspNetUserRoles
(
    UserId nvarchar(450) NOT NULL,
    RoleId nvarchar(450) NOT NULL,
    CONSTRAINT PK_AspNetUserRoles PRIMARY KEY (UserId, RoleId)
);
GO

CREATE TABLE AspNetUserTokens
(
    UserId        nvarchar(450) NOT NULL,
    LoginProvider nvarchar(450) NOT NULL,
    Name          nvarchar(450) NOT NULL,
    Value         nvarchar( max) NULL,
    CONSTRAINT PK_AspNetUserTokens PRIMARY KEY (UserId, LoginProvider, Name)
);
GO

ALTER TABLE dbo.PhotoEntry
    ADD CONSTRAINT fk_photoentry_photographer FOREIGN KEY (PhotographerId) REFERENCES dbo.Photographer (Id);
GO

ALTER TABLE dbo.PhotoEntry
    ADD CONSTRAINT fk_photoentry_phototheme FOREIGN KEY (ThemeId) REFERENCES dbo.PhotoTheme (Id);
GO

ALTER TABLE dbo.PhotoEntry
    ADD CONSTRAINT fk_photoentry_filemap FOREIGN KEY (FileId) REFERENCES dbo.FileMap (Id);
GO

ALTER TABLE dbo.PhotographerVoteDetails
    ADD CONSTRAINT fk_photographervotedetails_phototheme FOREIGN KEY (ThemeId) REFERENCES dbo.PhotoTheme (Id);
GO

ALTER TABLE dbo.PhotographerVoteDetails
    ADD CONSTRAINT fk_photographervotedetails_photographer FOREIGN KEY (PhotographerId) REFERENCES dbo.Photographer (Id);
GO

ALTER TABLE dbo.PhotographerVoteDetails
    ADD CONSTRAINT fk_photographervotedetails_firstvote FOREIGN KEY (FirstId) REFERENCES dbo.PhotoEntry (Id);
GO

ALTER TABLE dbo.PhotographerVoteDetails
    ADD CONSTRAINT fk_photographervotedetails_secondvote FOREIGN KEY (SecondId) REFERENCES dbo.PhotoEntry (Id);
GO

ALTER TABLE dbo.PhotographerVoteDetails
    ADD CONSTRAINT fk_photographervotedetails_thirdvote FOREIGN KEY (ThirdId) REFERENCES dbo.PhotoEntry (Id);
GO

ALTER TABLE dbo.ScoreDetail
    ADD CONSTRAINT fk_scoredetail_photoentry FOREIGN KEY (EntryId) REFERENCES dbo.PhotoEntry (Id);
GO

CREATE INDEX EmailIndex ON AspNetUsers (NormalizedEmail);
GO

CREATE INDEX IX_AspNetRoleClaims_RoleId ON AspNetRoleClaims (RoleId);
GO

CREATE INDEX IX_AspNetUserClaims_UserId ON AspNetUserClaims (UserId);
GO

CREATE INDEX IX_AspNetUserLogins_UserId ON AspNetUserLogins (UserId);
GO

CREATE INDEX IX_AspNetUserRoles_RoleId ON AspNetUserRoles (RoleId);
GO

ALTER TABLE AspNetRoleClaims
    ADD CONSTRAINT FK_AspNetRoleClaims_AspNetRoles_RoleId FOREIGN KEY (RoleId) REFERENCES AspNetRoles (Id) ON DELETE CASCADE;
GO

ALTER TABLE AspNetUserClaims
    ADD CONSTRAINT FK_AspNetUserClaims_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;
GO

ALTER TABLE AspNetUserLogins
    ADD CONSTRAINT FK_AspNetUserLogins_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;
GO

ALTER TABLE AspNetUserRoles
    ADD CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId FOREIGN KEY (RoleId) REFERENCES AspNetRoles (Id) ON DELETE CASCADE;
GO

ALTER TABLE AspNetUserRoles
    ADD CONSTRAINT FK_AspNetUserRoles_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;
GO

ALTER TABLE AspNetUserTokens
    ADD CONSTRAINT FK_AspNetUserTokens_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;
GO