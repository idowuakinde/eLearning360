using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RssReader
{
	[Activity (Label = "e-Learning360 - powered by SmartSystems", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
	public class StartupActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view
			SetContentView (Resource.Layout.StartupLayout);
						
			var menu = FindViewById<FlyOutContainer> (Resource.Id.FlyOutContainer);
			var menuButton = FindViewById (Resource.Id.MenuButton);
			menuButton.Click += (sender, e) => {
			menu.AnimatedOpened = !menu.AnimatedOpened;

			var listView = FindViewById<ExpandableListView> (Resource.Id.expandableListViewUni);
				if (listView == null)
				{
					ViewGroup parent = menu;
					//listView = new ExpandableListView(this);
					parent.AddView(listView);
				}
				else
				{
					listView.SetAdapter (new ExpandableDataAdapter (this, Data.SampleData ()));
				}
			
				if (listView.Parent != null)
				{
					ViewGroup parent = (ViewGroup)listView.Parent;
					parent.RemoveView(listView);
				}
				else
				{
					menu.AddView(listView);
				}
			};
		}
	}
}
