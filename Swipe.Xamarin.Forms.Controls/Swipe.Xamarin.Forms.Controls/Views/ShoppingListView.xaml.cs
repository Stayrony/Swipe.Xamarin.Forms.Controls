using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Swipe.Xamarin.Forms.Controls.Cell;
using Swipe.Xamarin.Forms.Controls.Controls;

namespace Swipe.Xamarin.Forms.Controls.Views
{
	public partial class ShoppingListView : BaseContentPage
	{
		public ShoppingListView()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<SwipeView, bool>(this, "IsScrollListEnabled", (sender, isScrollEnabled) =>
			{
				listView.IsScrollEnabled = isScrollEnabled;
			});
		}
	}
}
