CREATE TABLE [dbo].[Tweets] (
    [Id]                    INT                IDENTITY (1, 1) NOT NULL,
    [TweetId]               NVARCHAR (50)      NOT NULL,
    [Text]                  NVARCHAR (350)     NOT NULL,
    [CreatedAt]             DATETIMEOFFSET (7) NOT NULL,
    [EventProcessedUtcTime] DATETIME2 (7)      NULL,
    [PartitionId]           BIGINT             NULL,
    [EventEnqueuedUtcTime]  DATETIME2 (7)      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UC_Tweets] UNIQUE NONCLUSTERED ([TweetId] ASC)
);

