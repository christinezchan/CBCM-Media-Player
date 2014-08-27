USE [MediaLibrary]
GO


/****** Object:  Table [cbcmgro_medialibrary].[FOLDER_MEDIA]    Script Date: 7/21/2014 11:32:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FOLDER_MEDIA](
	[FOLDER_ID] [int] NOT NULL,
	[MEDIA_ID] [varchar](30) NOT NULL,
 CONSTRAINT [PK_FOLDER_MEDIA] PRIMARY KEY CLUSTERED 
(
	[FOLDER_ID] ASC,
	[MEDIA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


