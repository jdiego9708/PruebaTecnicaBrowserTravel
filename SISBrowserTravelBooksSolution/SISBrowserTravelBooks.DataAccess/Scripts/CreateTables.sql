CREATE TABLE [dbo].[Authors](
	[Id_author] [int] IDENTITY(1,1) NOT NULL,
	[Name_author] [varchar](5) NOT NULL,
	[Last_name_author] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Id_author] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authors_books]    Script Date: 26/02/2023 5:23:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors_books](
	[Id_author] [int] NOT NULL,
	[Id_book] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 26/02/2023 5:23:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id_book] [int] IDENTITY(1,1) NOT NULL,
	[Id_editorial] [int] NOT NULL,
	[Tittle_book] [varchar](50) NOT NULL,
	[Synopsis_book] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id_book] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Editorials]    Script Date: 26/02/2023 5:23:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Editorials](
	[Id_editorial] [int] IDENTITY(1,1) NOT NULL,
	[Name_editorial] [varchar](50) NULL,
	[Campus_editorial] [varchar](50) NULL,
 CONSTRAINT [PK_Editorials] PRIMARY KEY CLUSTERED 
(
	[Id_editorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Authors_books]  WITH CHECK ADD  CONSTRAINT [FK_Authors_books_Authors] FOREIGN KEY([Id_author])
REFERENCES [dbo].[Authors] ([Id_author])
GO
ALTER TABLE [dbo].[Authors_books] CHECK CONSTRAINT [FK_Authors_books_Authors]
GO
ALTER TABLE [dbo].[Authors_books]  WITH CHECK ADD  CONSTRAINT [FK_Authors_books_Books] FOREIGN KEY([Id_book])
REFERENCES [dbo].[Books] ([Id_book])
GO
ALTER TABLE [dbo].[Authors_books] CHECK CONSTRAINT [FK_Authors_books_Books]
GO
