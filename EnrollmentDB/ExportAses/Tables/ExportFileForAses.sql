﻿CREATE TABLE [ExportAses].[ExportFileForAses] (
    [RECORD_TYPE]                 NCHAR (1)     NOT NULL,
    [TRAN_ID]                     NCHAR (1)     NOT NULL,
    [PROCESS_DATE]                NCHAR (8)     NOT NULL,
    [REGION]                      NCHAR (1)     NOT NULL,
    [CARRIER]                     NCHAR (2)     NOT NULL,
    [MEMBER_PRIMARY_CENTER]       NCHAR (4)     NOT NULL,
    [ODSI_FAMILY_ID]              NCHAR (11)    NOT NULL,
    [MEMBER_SSN]                  NCHAR (9)     NOT NULL,
    [MEMBER_SUFFIX]               NCHAR (2)     NOT NULL,
    [EFFECTIVE_DATE]              NCHAR (8)     NOT NULL,
    [PLAN_TYPE]                   NCHAR (2)     NOT NULL,
    [PLAN_VERSION]                NCHAR (3)     NOT NULL,
    [MPI]                         NCHAR (13)    NOT NULL,
    [PCP1]                        NCHAR (15)    NOT NULL,
    [PCP1_EFFECTIVE_DATE]         NCHAR (8)     NOT NULL,
    [PCP2]                        NVARCHAR (15) NULL,
    [PCP2_EFFECTIVE_DATE]         NVARCHAR (8)  NULL,
    [FAMILY_PRIMARY_CENTER]       NCHAR (4)     NOT NULL,
    [PMG_TAX_ID_EFF_DT]           NCHAR (8)     NOT NULL,
    [IPA_PCP_CHANGE_REASON]       NVARCHAR (2)  NULL,
    [MEDICARE_INDICATOR]          NVARCHAR (1)  NULL,
    [HIC_NUMBER]                  NVARCHAR (12) NULL,
    [REJECT_IDENTIFIER]           NVARCHAR (1)  NULL,
    [RECORD_KEY]                  NVARCHAR (14) NULL,
    [ERROR_CODE_1]                NVARCHAR (3)  NULL,
    [ERROR_CODE_2]                NVARCHAR (3)  NULL,
    [ERROR_CODE_3]                NVARCHAR (3)  NULL,
    [ERROR_CODE_4]                NVARCHAR (3)  NULL,
    [ERROR_CODE_5]                NVARCHAR (3)  NULL,
    [ERROR_CODE_6]                NVARCHAR (3)  NULL,
    [ERROR_CODE_7]                NVARCHAR (3)  NULL,
    [ERROR_CODE_8]                NVARCHAR (3)  NULL,
    [ERROR_CODE_9]                NVARCHAR (3)  NULL,
    [ERROR_CODE_10]               NVARCHAR (3)  NULL,
    [UPDATE_DATE]                 NVARCHAR (8)  NULL,
    [UPDATE_USER]                 NVARCHAR (8)  NULL,
    [IPA_ESPECIAL]                NVARCHAR (1)  NULL,
    [CONTRACT_NUMBER]             NCHAR (13)    NOT NULL,
    [SPECIAL_ENROLL]              NVARCHAR (1)  NULL,
    [PMG_TAX_ID]                  NCHAR (9)     NOT NULL,
    [DATA_SOURCE]                 NCHAR (2)     NOT NULL,
    [FILLER]                      NCHAR (4)     NULL,
    [BLOCK_NUMBER]                NCHAR (10)    NULL,
    [PROCESS_HEADER_ID]           INT           NOT NULL,
    [GENERATED_FOR_ASES]          BIT           NULL,
    [GENERATED_FOR_ASES_DATE]     DATE          NULL,
    [REJECTED_FROM_ASES]          BIT           NULL,
    [REJECTED_FROM_ASES_DATE]     DATE          NULL,
    [MEMBER_ID_ENROLLMENTHISTORY] INT           NOT NULL,
    [STATUSID]                    INT           NULL,
    CONSTRAINT [FK__ExportFil__STATU__324172E1] FOREIGN KEY ([STATUSID]) REFERENCES [Enrollment].[Status] ([Id])
);


