CREATE TABLE [Enrollment].[Files] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [MemberId]  INT            NOT NULL,
    [Path]      NVARCHAR (300) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [Name]      NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [Extension] NVARCHAR (5)   COLLATE Modern_Spanish_CI_AS NOT NULL,
    [CreatedBy] NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [CreatedOn] DATETIME       NOT NULL,
    [UpdatedBy] NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn] DATETIME       NULL,
    [Enabled]   BIT            NOT NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Files_Members] FOREIGN KEY ([MemberId]) REFERENCES [Enrollment].[Members] ([Id])
);


