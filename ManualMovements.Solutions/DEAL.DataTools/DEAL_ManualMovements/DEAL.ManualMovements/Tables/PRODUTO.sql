﻿CREATE TABLE [dbo].[PRODUTO]
(
	[COD_PRODUTO] CHAR(4) NOT NULL, 
    [DES_PRODUTO] VARCHAR(30) NULL, 
    [STA_STATUS] CHAR NULL,
	CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
	(
		[COD_PRODUTO] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
