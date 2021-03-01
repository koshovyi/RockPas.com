using RockPaperScissors.Database;
using SqlBuilder.Attributes;
using System;

namespace RockPaperScissors.Services.Support.Models
{

	[TableName(Constants.TABLE_SUPPORT_ARTICLES)]
	public class Article
	{

		[IgnoreUpdate]
		[InsertDefault("@uuid")]
		public Guid ID { get; set; }

		[IgnoreUpdate]
		public DateTime CreatedAt { get; set; }

		public string Link { get; set; }

		public string Title { get; set; }

		public string Memo { get; set; }

		public int Position { get; set; }

		public bool IsDeleted { get; set; }

		public Article()
		{
			this.CreatedAt = DateTime.UtcNow;
		}

		public Article(string link, string title, string memo, int position = 0)
		{
			this.Link = link;
			this.Title = title;
			this.Memo = memo;
			this.Position = position;
			this.CreatedAt = DateTime.UtcNow;
		}

		public static string GetArticleLink(ArticleType type)
		{
			switch (type)
			{
				case ArticleType.ABOUT:
					return "about";
				case ArticleType.PRIVACY:
					return "privacy";
				case ArticleType.TEAM:
					return "team";
				case ArticleType.TERMSCONDITIONS:
					return "terms-and-conditions";
				case ArticleType.OFFER:
					return "offer";
				default:
				case ArticleType.UNKNOWN:
					return string.Empty;
			}
		}

	}

}
