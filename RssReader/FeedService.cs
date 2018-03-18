using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace RssReader
{
	internal static class FeedService
	{
		internal static List<FeedItem> GetFeedItems(string url)
		{
			List<FeedItem> feedItemsList = new List<FeedItem>();
			
			try
			{
				WebRequest webRequest = WebRequest.Create(url);
				WebResponse webResponse = webRequest.GetResponse();

				Stream stream = webResponse.GetResponseStream();
				XmlDocument xmlDocument = new XmlDocument();

				xmlDocument.Load(stream);

				XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
				nsmgr.AddNamespace("dc", xmlDocument.DocumentElement.GetNamespaceOfPrefix("dc"));
				nsmgr.AddNamespace("", xmlDocument.NamespaceURI);
				nsmgr.AddNamespace("rdf", xmlDocument.DocumentElement.GetNamespaceOfPrefix("rdf"));
				nsmgr.AddNamespace("enc", xmlDocument.DocumentElement.GetNamespaceOfPrefix("enc"));
				nsmgr.AddNamespace("media", xmlDocument.DocumentElement.GetNamespaceOfPrefix("media"));

				XmlNodeList itemNodes = xmlDocument.SelectNodes("*")[0].ChildNodes;

				for (int i = 1; i < itemNodes.Count; i++)
				{
					FeedItem feedItem = new FeedItem();


//					if (itemNodes[i].SelectSingleNode("title") != null)
					if (itemNodes[i].ChildNodes[0] != null)
					{
//						feedItem.Title = itemNodes[i].SelectSingleNode("title").InnerText;
						feedItem.Title = itemNodes[i].ChildNodes[0].InnerText;
					}

					if (itemNodes[i].ChildNodes[1] != null)
					{
						feedItem.Description = itemNodes[i].ChildNodes[1].InnerText;
					}

					if (itemNodes[i].ChildNodes[2] != null)
					{
						feedItem.Link = itemNodes[i].ChildNodes[2].InnerText;
					}

					if (itemNodes[i].SelectSingleNode("dc:creator", nsmgr) != null)
					{
						feedItem.Creator = itemNodes[i].SelectSingleNode("dc:creator", nsmgr).InnerText;
					}

					if (itemNodes[i].SelectSingleNode("dc:date", nsmgr) != null)
					{
						feedItem.PubDate = Convert.ToDateTime(itemNodes[i].SelectSingleNode("dc:date", nsmgr).InnerText);
					}

					else
					{
						feedItem.Content = feedItem.Description;
					}

					feedItemsList.Add(feedItem);
				}
			}
			catch (Exception)
			{
				throw;
			}

			return feedItemsList;
		}
	}
}