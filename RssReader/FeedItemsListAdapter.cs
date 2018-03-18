using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace RssReader
{
	public class FeedItemsListAdapter : BaseAdapter<FeedItem>
	{
		protected Activity context = null;
		protected List<FeedItem> feedsList = new List<FeedItem>();

		public FeedItemsListAdapter(Activity context, List<FeedItem> feedsList)
			: base()
		{
			this.context = context;
			this.feedsList = feedsList;
		}

		public override FeedItem this[int position]
		{
			get { return this.feedsList[position]; }
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override int Count
		{
			get { return this.feedsList.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var feedItem = this.feedsList[position];

			var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.FeedItemListItemLayout, parent, false)) as LinearLayout;

			view.FindViewById<TextView>(Resource.Id.title).Text = feedItem.Title.Length < 51 ? feedItem.Title : feedItem.Title.Substring(0, 50) + "...";
			view.FindViewById<TextView>(Resource.Id.creator).Text = "Faculty: " + feedItem.Creator;
			view.FindViewById<TextView>(Resource.Id.pubDate).Text = "Date: " + feedItem.PubDate.ToString("MMM dd, yyyy hh:mm tt");
			return view;
		}
	}
}