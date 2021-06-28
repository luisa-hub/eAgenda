CREATE TABLE [dbo].[TBTarefas] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [titulo]        VARCHAR (50) NULL,
    [percentual]    INT          NULL,
    [prioridade]    VARCHAR (50) NULL,
    [dataInicio]    DATETIME     NULL,
    [dataConclusao] DATETIME     NULL,
    CONSTRAINT [PK_TBTarefas] PRIMARY KEY CLUSTERED ([Id] ASC)
);

