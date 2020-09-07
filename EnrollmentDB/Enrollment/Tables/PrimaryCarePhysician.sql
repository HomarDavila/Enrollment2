CREATE TABLE [Enrollment].[PrimaryCarePhysician] (
    [Id]                           INT            IDENTITY (1, 1) NOT NULL,
    [PersonPrimaryCarePhysicianId] INT            NULL,
    [SpecialityId]                 INT            NULL,
    [CreatedBy]                    NVARCHAR (100) NULL,
    [CreatedOn]                    DATETIME       NOT NULL,
    [UpdatedBy]                    NVARCHAR (100) NULL,
    [UpdatedOn]                    DATETIME       NULL,
    [Enabled]                      BIT            NOT NULL,
    CONSTRAINT [PK_PrimaryCarePhysician] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrimaryCarePhysician_PersonPrimaryCarePhysician1] FOREIGN KEY ([PersonPrimaryCarePhysicianId]) REFERENCES [Enrollment].[PersonPrimaryCarePhysician] ([Id]),
    CONSTRAINT [FK_PrimaryCarePhysician_Specialities] FOREIGN KEY ([SpecialityId]) REFERENCES [Enrollment].[Specialities] ([Id])
);


















GO
CREATE NONCLUSTERED INDEX [I_PrimaryCarePhysician_SpecialityId]
    ON [Enrollment].[PrimaryCarePhysician]([SpecialityId] ASC);


GO
CREATE NONCLUSTERED INDEX [I_PrimaryCarePhysician_PersonPrimaryCarePhysicianId]
    ON [Enrollment].[PrimaryCarePhysician]([PersonPrimaryCarePhysicianId] ASC);

