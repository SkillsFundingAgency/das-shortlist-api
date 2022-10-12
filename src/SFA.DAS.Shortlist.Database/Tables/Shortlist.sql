CREATE TABLE [dbo].[Shortlist]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [ShortlistUserId] UNIQUEIDENTIFIER NOT NULL,
    [UkPrn] INT NOT NULL,
    [Larscode] INT NOT NULL,
    [LocationDescription] VARCHAR(1000) NULL,
    [Latitude] FLOAT NULL,
    [Longitude] FLOAT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_Shortlist PRIMARY KEY ([Id]) 
)
GO

CREATE NONCLUSTERED INDEX [IDX_Shortlist_UserItems] ON [dbo].[Shortlist] (ShortlistUserId) WITH (ONLINE=ON)
GO
