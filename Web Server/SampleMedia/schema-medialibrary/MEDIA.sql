USE [MediaLibrary]
GO
/****** Object:  Table [cbcmgro_medialibrary].[MEDIA]    Script Date: 11/14/2007 23:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MEDIA](
	[MEDIA_ID] [varchar](30) NOT NULL,
	[ONLINE] [varchar](1) NULL,
	[YYYY] [varchar](4) NULL,
	[MM] [varchar](2) NULL,
	[DD] [varchar](2) NULL,
	[LANGUAGE] [varchar](10) NULL,
	[TITLE] [nvarchar](100) NULL,
	[SUBTITLE] [nvarchar](100) NULL,
	[TOPIC] [nvarchar](100) NULL,
	[VERSES] [nvarchar](100) NULL,
	[SPEAKER] [nvarchar](100) NULL,
	[HANDOUT] [varchar](1) NULL,
	[DURATION] [varchar](10) NULL,
	[FLASH] [varchar](20) NULL,
	[MDATE] [datetime] NULL,
	[LOCATION] [varchar](1) NULL,
 CONSTRAINT [PK_MEDIA] PRIMARY KEY CLUSTERED 
(
	[MEDIA_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

ALTER TABLE [dbo].[MEDIA] ADD  CONSTRAINT [DF_MEDIA_LOCATION]  DEFAULT ('I') FOR [LOCATION]
GO

