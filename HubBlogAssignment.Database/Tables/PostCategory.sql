CREATE TABLE [dbo].[PostCategory]
(
	[PostId] INT NOT NULL FOREIGN KEY REFERENCES [Post](Id),
	[CategoryId] INT NOT NULL FOREIGN KEY REFERENCES [Category](Id),
	PRIMARY KEY ([PostId], [CategoryId])
)
