CREATE TABLE [dbo].[Shortlist]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [ShortlistUserId] UNIQUEIDENTIFIER NOT NULL,
    [UkPrn] INT NOT NULL,
    [StandardId] INT NOT NULL,
    [LocationDescription] VARCHAR(1000) NULL,
    [Lat] FLOAT NULL,
    [Long] FLOAT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_Shortlist PRIMARY KEY ([Id]) 
)
GO

CREATE NONCLUSTERED INDEX [IDX_Shortlist_UserItems] ON [dbo].[Shortlist] (ShortlistUserId) WITH (ONLINE=ON)
GO
