CREATE TABLE [dbo].[TBContatos] (
    [id]       INT          IDENTITY (1, 1) NOT NULL,
    [nome]     VARCHAR (50) NULL,
    [email]    VARCHAR (50) NULL,
    [empresa]  VARCHAR (50) NULL,
    [telefone] VARCHAR (50) NULL,
    [cargo]    VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TBContatos] PRIMARY KEY CLUSTERED ([id] ASC)
);

