CREATE TABLE [dbo].[Article] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Title]            NVARCHAR (MAX) NOT NULL,
    [Content]          NVARCHAR (MAX) NOT NULL,
    [ShortDescription] NVARCHAR (MAX) NULL,
    [Created]          DATETIME       NULL,
    [Modified]         DATETIME       NULL,
    [Published]        DATETIME       NULL,
    [Category]         INT            NULL,
    CONSTRAINT [PK_dbo.Article] PRIMARY KEY CLUSTERED ([ID] ASC)
);

