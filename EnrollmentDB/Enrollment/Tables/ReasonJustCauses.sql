CREATE TABLE [Enrollment].[ReasonJustCauses] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Reason]      NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [Description] NVARCHAR (250) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [CreatedBy]   NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [CreatedOn]   DATETIME       NOT NULL,
    [UpdatedBy]   NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn]   DATETIME       NULL,
    [Enabled]     BIT            NOT NULL,
    [Razon]       NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [Descripcion] NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ReasonJustCause] PRIMARY KEY CLUSTERED ([Id] ASC)
);



