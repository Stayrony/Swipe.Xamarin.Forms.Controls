using System;
using Swipe.Xamarin.Forms.Controls.Controls;
using Swipe.Xamarin.Forms.Controls.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ControllableScrollListView), typeof(ControllableScrollListViewRenderer))]
namespace Swipe.Xamarin.Forms.Controls.iOS.Renderers
{
	public class ControllableScrollListViewRenderer : ListViewRenderer
	{
		private ControllableScrollListView _extendedListView;

		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
				return;

			_extendedListView = (ControllableScrollListView)Element;

			if (_extendedListView != null)
			{
				UpdateState(_extendedListView);
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == "IsScrollEnabled")
				UpdateState(_extendedListView);
		}

		#region -- Private helpers --

		private void UpdateState(ControllableScrollListView view)
		{
			if (Control != null)
			{
				Control.ScrollEnabled = view.IsScrollEnabled;
			}
		}

		#endregion
	}
}
