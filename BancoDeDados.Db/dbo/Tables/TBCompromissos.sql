CREATE TABLE [dbo].[TBCompromissos] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [assunto]         VARCHAR (50) NULL,
    [local]           VARCHAR (50) NULL,
    [dataCompromisso] DATE         NULL,
    [horaInicio]      DATETIME     NULL,
    [horaFim]         DATETIME     NULL,
    [Id_Contato]      INT          NULL,
    CONSTRAINT [PK_TBCompromissos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBCompromissos_TBContatos] FOREIGN KEY ([Id_Contato]) REFERENCES [dbo].[TBContatos] ([id])
);

