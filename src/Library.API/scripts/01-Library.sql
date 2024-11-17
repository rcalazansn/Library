USE LibraryCompose;
GO
    
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
    WHERE [MigrationId] = N'20240625220152_Initial'
)
BEGIN
    CREATE TABLE [books] (
        [id] int NOT NULL IDENTITY,
        [title] varchar(30) NOT NULL,
        [author] varchar(30) NOT NULL,
        [isbn] varchar(13) NOT NULL,
        [year_of_publication] int NOT NULL,
        [status] int NOT NULL,
        CONSTRAINT [pk_books] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240625220152_Initial'
)
BEGIN
    CREATE TABLE [users] (
        [id] int NOT NULL IDENTITY,
        [name] varchar(30) NOT NULL,
        [email] varchar(50) NOT NULL,
        [user_type_enum] int NOT NULL,
        CONSTRAINT [pk_users] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240625220152_Initial'
)
BEGIN
    CREATE TABLE [loans] (
        [id] int NOT NULL IDENTITY,
        [user_id] int NOT NULL,
        [book_id] int NOT NULL,
        [loan_date] datetime2 NOT NULL,
        [deadline_return_date] datetime2 NOT NULL,
        [return_date] datetime2 NOT NULL,
        CONSTRAINT [pk_loans] PRIMARY KEY ([id]),
        CONSTRAINT [fk_loans_books_book_id] FOREIGN KEY ([book_id]) REFERENCES [books] ([id]) ON DELETE NO ACTION,
        CONSTRAINT [fk_loans_users_user_id] FOREIGN KEY ([user_id]) REFERENCES [users] ([id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240625220152_Initial'
)
BEGIN
    CREATE INDEX [IX_loans_book_id] ON [loans] ([book_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240625220152_Initial'
)
BEGIN
    CREATE INDEX [IX_loans_user_id] ON [loans] ([user_id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240625220152_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240625220152_Initial', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627195917_AddDateTime'
)
BEGIN
    ALTER TABLE [users] ADD [createdd_at] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627195917_AddDateTime'
)
BEGIN
    ALTER TABLE [users] ADD [modified_at] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627195917_AddDateTime'
)
BEGIN
    ALTER TABLE [loans] ADD [createdd_at] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627195917_AddDateTime'
)
BEGIN
    ALTER TABLE [loans] ADD [modified_at] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627195917_AddDateTime'
)
BEGIN
    ALTER TABLE [books] ADD [createdd_at] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627195917_AddDateTime'
)
BEGIN
    ALTER TABLE [books] ADD [modified_at] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240627195917_AddDateTime'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240627195917_AddDateTime', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240911184943_soft-delete'
)
BEGIN
    ALTER TABLE [books] ADD [deleted_at] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240911184943_soft-delete'
)
BEGIN
    ALTER TABLE [books] ADD [is_deleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240911184943_soft-delete'
)
BEGIN
    CREATE INDEX [IX_books_is_deleted] ON [books] ([is_deleted]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240911184943_soft-delete'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240911184943_soft-delete', N'8.0.6');
END;
GO

COMMIT;
GO

