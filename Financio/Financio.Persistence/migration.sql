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

CREATE TABLE [Accounts] (
    [Number] nchar(4) NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(500) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([Number])
);
GO

CREATE TABLE [References] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [Value] float NOT NULL,
    [Side] nvarchar(max) NOT NULL,
    [AccountNumber] nchar(4) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_References] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_References_Accounts_AccountNumber] FOREIGN KEY ([AccountNumber]) REFERENCES [Accounts] ([Number]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AccountAccountReference] (
    [CounterAccountReferencesId] int NOT NULL,
    [CounterAccountsNumber] nchar(4) NOT NULL,
    CONSTRAINT [PK_AccountAccountReference] PRIMARY KEY ([CounterAccountReferencesId], [CounterAccountsNumber]),
    CONSTRAINT [FK_AccountAccountReference_Accounts_CounterAccountsNumber] FOREIGN KEY ([CounterAccountsNumber]) REFERENCES [Accounts] ([Number]),
    CONSTRAINT [FK_AccountAccountReference_References_CounterAccountReferencesId] FOREIGN KEY ([CounterAccountReferencesId]) REFERENCES [References] ([Id])
);
GO

CREATE INDEX [IX_AccountAccountReference_CounterAccountsNumber] ON [AccountAccountReference] ([CounterAccountsNumber]);
GO

CREATE INDEX [IX_References_AccountNumber] ON [References] ([AccountNumber]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230806164640_Second', N'6.0.9');
GO

COMMIT;
GO

