CREATE TABLE [dbo].[Vote]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[UserId] INT NOT NULL FOREIGN KEY REFERENCES [User](Id),
	[PostId] INT NULL FOREIGN KEY REFERENCES [Post](Id),
	[CommentId] INT NULL FOREIGN KEY REFERENCES [Comment](Id),
	[CreatedDateTimeUtc] DATETIME2 NOT NULL,
	CONSTRAINT [OnlyOnePostOrComment]
        CHECK (        ([PostId] IS NULL OR [CommentId] IS NULL) 
               AND NOT ([PostId] IS NULL AND [CommentId] IS NULL)),
	UNIQUE ([UserId],[PostId],[CommentId])
)
