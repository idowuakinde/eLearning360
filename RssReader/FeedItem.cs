using System;

namespace RssReader
{
	public class FeedItem
	{
		public FeedItem()
		{

		}

		public string Title { get; set; }

		public string Description { get; set; }

		public string Link { get; set; }

		public string Creator { get; set; }

		public DateTime PubDate { get; set; }

		public string Subject { get; set; }

		public string Content { get; set; }
	}
}