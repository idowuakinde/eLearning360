using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RssReader
{
	[Activity (Label = "e-Learning360 - powered by SmartSystems")]
	public class Activity1 : Activity
	{
		private List<FeedItem> lista;
		private ListView feedItemsListView;
		private ProgressDialog progressDialog;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			this.feedItemsListView = this.FindViewById<ListView>(Resource.Id.feedItemsListView);

			this.progressDialog = new ProgressDialog(this);
			this.progressDialog.SetMessage("Please wait...");

			this.GetFeedItemsList();
		}

		private void GetFeedItemsList()
		{
			this.progressDialog.Show();

			Task<List<FeedItem>> task1 = Task.Factory.StartNew(() =>
			{
				return FeedService.GetFeedItems("http://ocw.mit.edu/rss/new/mit-newcourses-16.xml");
			}
			);

			Task task2 = task1.ContinueWith((antecedent) =>
			    {
				try
				{
					this.progressDialog.Dismiss();
					this.lista = antecedent.Result;
					this.PopulateListView(this.lista);
				}
				catch (AggregateException aex)
				{
					Toast.MakeText (this, aex.InnerException.Message, ToastLength.Short).Show ();
				}
			}, TaskScheduler.FromCurrentSynchronizationContext()
			);
		}

		private void PopulateListView(List<FeedItem> feedItemsList)
		{
			var adapter = new FeedItemsListAdapter(this, feedItemsList);
			this.feedItemsListView.Adapter = adapter;
			this.feedItemsListView.ItemClick += OnListViewItemClick;
		}

		protected void OnListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			// Here the logic to start another screen with details....
			var t = lista[e.Position];
			Android.Widget.Toast.MakeText(this, t.Link, Android.Widget.ToastLength.Short).Show();
		}
	}
}