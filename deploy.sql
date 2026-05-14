IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [ClientItems] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(150) NOT NULL,
        [LogoUrl] nvarchar(300) NOT NULL,
        [WebsiteUrl] nvarchar(300) NULL,
        [DisplayOrder] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ClientItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [SiteContents] (
        [Id] int NOT NULL IDENTITY,
        [LogoUrl] nvarchar(300) NULL,
        [Phone] nvarchar(40) NULL,
        [WhatsApp] nvarchar(40) NULL,
        [Email] nvarchar(200) NULL,
        [FooterTextEn] nvarchar(500) NULL,
        [FooterTextAr] nvarchar(500) NULL,
        [LinkedInUrl] nvarchar(300) NULL,
        [TwitterUrl] nvarchar(300) NULL,
        [GitHubUrl] nvarchar(300) NULL,
        [HeroTitleEn] nvarchar(300) NULL,
        [HeroTitleAr] nvarchar(300) NULL,
        [HeroSubtitleEn] nvarchar(800) NULL,
        [HeroSubtitleAr] nvarchar(800) NULL,
        [HeroImageUrl] nvarchar(300) NULL,
        [HeroPrimaryCtaTextEn] nvarchar(80) NULL,
        [HeroPrimaryCtaTextAr] nvarchar(80) NULL,
        [HeroPrimaryCtaUrl] nvarchar(300) NULL,
        [AboutTitleEn] nvarchar(200) NULL,
        [AboutTitleAr] nvarchar(200) NULL,
        [AboutTextEn] nvarchar(2000) NULL,
        [AboutTextAr] nvarchar(2000) NULL,
        [AboutImageUrl] nvarchar(300) NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_SiteContents] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ClientItems_DisplayOrder] ON [ClientItems] ([DisplayOrder]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513130414_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260513130414_InitialCreate', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteContents]') AND [c].[name] = N'LogoUrl');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SiteContents] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [SiteContents] ALTER COLUMN [LogoUrl] nvarchar(400) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteContents]') AND [c].[name] = N'HeroImageUrl');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SiteContents] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [SiteContents] ALTER COLUMN [HeroImageUrl] nvarchar(400) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteContents]') AND [c].[name] = N'AboutImageUrl');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [SiteContents] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [SiteContents] ALTER COLUMN [AboutImageUrl] nvarchar(400) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [Address] nvarchar(300) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [CtaBannerButtonTextAr] nvarchar(80) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [CtaBannerButtonTextEn] nvarchar(80) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [CtaBannerButtonUrl] nvarchar(300) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [CtaBannerImageUrl] nvarchar(400) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [CtaBannerSubtitleAr] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [CtaBannerSubtitleEn] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [CtaBannerTitleAr] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [CtaBannerTitleEn] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [FacebookUrl] nvarchar(300) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [FaviconUrl] nvarchar(400) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [HeroEyebrowAr] nvarchar(300) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [HeroEyebrowEn] nvarchar(300) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [HeroSecondaryCtaTextAr] nvarchar(80) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [HeroSecondaryCtaTextEn] nvarchar(80) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [HeroSecondaryCtaUrl] nvarchar(300) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [InstagramUrl] nvarchar(300) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [LogoDarkUrl] nvarchar(400) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [PortfolioLeadAr] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [PortfolioLeadEn] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [PortfolioTitleAr] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [PortfolioTitleEn] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [ServicesLeadAr] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [ServicesLeadEn] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [ServicesTitleAr] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    ALTER TABLE [SiteContents] ADD [ServicesTitleEn] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [FaqItems] (
        [Id] int NOT NULL IDENTITY,
        [QuestionEn] nvarchar(300) NOT NULL,
        [QuestionAr] nvarchar(300) NOT NULL,
        [AnswerEn] nvarchar(2000) NOT NULL,
        [AnswerAr] nvarchar(2000) NOT NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_FaqItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [MediaItems] (
        [Id] int NOT NULL IDENTITY,
        [FileName] nvarchar(260) NOT NULL,
        [FileUrl] nvarchar(400) NOT NULL,
        [FileType] nvarchar(20) NOT NULL,
        [Category] nvarchar(40) NOT NULL,
        [AltText] nvarchar(200) NULL,
        [SizeBytes] bigint NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_MediaItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [PortfolioItems] (
        [Id] int NOT NULL IDENTITY,
        [TitleEn] nvarchar(150) NOT NULL,
        [TitleAr] nvarchar(150) NOT NULL,
        [DescriptionEn] nvarchar(1000) NULL,
        [DescriptionAr] nvarchar(1000) NULL,
        [ThumbnailUrl] nvarchar(400) NULL,
        [Technologies] nvarchar(400) NULL,
        [ProjectUrl] nvarchar(400) NULL,
        [DisplayOrder] int NOT NULL,
        [IsFeatured] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_PortfolioItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [ProcessSteps] (
        [Id] int NOT NULL IDENTITY,
        [TitleEn] nvarchar(120) NOT NULL,
        [TitleAr] nvarchar(120) NOT NULL,
        [DescriptionEn] nvarchar(500) NULL,
        [DescriptionAr] nvarchar(500) NULL,
        [IconClass] nvarchar(80) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ProcessSteps] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [ServiceItems] (
        [Id] int NOT NULL IDENTITY,
        [TitleEn] nvarchar(150) NOT NULL,
        [TitleAr] nvarchar(150) NOT NULL,
        [DescriptionEn] nvarchar(800) NULL,
        [DescriptionAr] nvarchar(800) NULL,
        [IconUrl] nvarchar(400) NULL,
        [IconClass] nvarchar(80) NULL,
        [ImageUrl] nvarchar(400) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ServiceItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [SiteSettings] (
        [Id] int NOT NULL IDENTITY,
        [MetaTitleEn] nvarchar(160) NULL,
        [MetaTitleAr] nvarchar(160) NULL,
        [MetaDescriptionEn] nvarchar(320) NULL,
        [MetaDescriptionAr] nvarchar(320) NULL,
        [MetaKeywordsEn] nvarchar(320) NULL,
        [MetaKeywordsAr] nvarchar(320) NULL,
        [OpenGraphImage] nvarchar(400) NULL,
        [FaviconUrl] nvarchar(400) NULL,
        [GoogleAnalyticsCode] nvarchar(60) NULL,
        [PrimaryColor] nvarchar(20) NULL,
        [SecondaryColor] nvarchar(20) NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_SiteSettings] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [StatItems] (
        [Id] int NOT NULL IDENTITY,
        [LabelEn] nvarchar(80) NOT NULL,
        [LabelAr] nvarchar(80) NOT NULL,
        [Value] nvarchar(20) NOT NULL,
        [Suffix] nvarchar(10) NULL,
        [IconClass] nvarchar(80) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_StatItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [TechStackItems] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(80) NOT NULL,
        [IconUrl] nvarchar(400) NULL,
        [IconClass] nvarchar(80) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_TechStackItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE TABLE [Testimonials] (
        [Id] int NOT NULL IDENTITY,
        [NameEn] nvarchar(120) NOT NULL,
        [NameAr] nvarchar(120) NOT NULL,
        [RoleEn] nvarchar(150) NULL,
        [RoleAr] nvarchar(150) NULL,
        [CompanyEn] nvarchar(150) NULL,
        [CompanyAr] nvarchar(150) NULL,
        [AvatarUrl] nvarchar(400) NULL,
        [QuoteEn] nvarchar(1200) NOT NULL,
        [QuoteAr] nvarchar(1200) NOT NULL,
        [Rating] int NOT NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Testimonials] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_FaqItems_DisplayOrder] ON [FaqItems] ([DisplayOrder]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_MediaItems_Category] ON [MediaItems] ([Category]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_MediaItems_CreatedAt] ON [MediaItems] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_PortfolioItems_DisplayOrder] ON [PortfolioItems] ([DisplayOrder]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_ProcessSteps_DisplayOrder] ON [ProcessSteps] ([DisplayOrder]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_ServiceItems_DisplayOrder] ON [ServiceItems] ([DisplayOrder]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_StatItems_DisplayOrder] ON [StatItems] ([DisplayOrder]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_TechStackItems_DisplayOrder] ON [TechStackItems] ([DisplayOrder]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    CREATE INDEX [IX_Testimonials_DisplayOrder] ON [Testimonials] ([DisplayOrder]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513143614_AddCmsTables'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260513143614_AddCmsTables', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513144954_AddLeadRequests'
)
BEGIN
    CREATE TABLE [LeadRequests] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(150) NOT NULL,
        [Email] nvarchar(200) NOT NULL,
        [PhoneNumber] nvarchar(40) NULL,
        [WhatsAppNumber] nvarchar(40) NULL,
        [CompanyName] nvarchar(150) NULL,
        [ServiceType] nvarchar(80) NULL,
        [Budget] nvarchar(80) NULL,
        [Message] nvarchar(4000) NOT NULL,
        [AttachmentUrl] nvarchar(400) NULL,
        [AttachmentName] nvarchar(200) NULL,
        [Status] nvarchar(20) NOT NULL,
        [IsRead] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [IpAddress] nvarchar(45) NULL,
        CONSTRAINT [PK_LeadRequests] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513144954_AddLeadRequests'
)
BEGIN
    CREATE INDEX [IX_LeadRequests_CreatedAt] ON [LeadRequests] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513144954_AddLeadRequests'
)
BEGIN
    CREATE INDEX [IX_LeadRequests_Status] ON [LeadRequests] ([Status]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260513144954_AddLeadRequests'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260513144954_AddLeadRequests', N'8.0.10');
END;
GO

COMMIT;
GO

