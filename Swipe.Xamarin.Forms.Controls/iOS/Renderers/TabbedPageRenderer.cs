using System;
using System.ComponentModel;
using Swipe.Xamarin.Forms.Controls.iOS.Renderers;
using Swipe.Xamarin.Forms.Controls.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRenderer))]
namespace Swipe.Xamarin.Forms.Controls.iOS.Renderers
{
	public class TabbedPageRenderer : TabbedRenderer
	{
		#region -- Overrides --

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			if (e.OldElement != null)
				e.OldElement.PropertyChanged -= OnElementPropertyChanged;
			if (e.NewElement != null)
				e.NewElement.PropertyChanged += OnElementPropertyChanged;
			TabBar.Translucent = false;
			TabBar.Opaque = true;
			UpdateActiveColor();
		}

		#endregion

		#region -- Private helpers --

		private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == RootView.ActiveIconColorProperty.PropertyName)
				UpdateActiveColor();
		}

		private void UpdateActiveColor()
		{
			var mp = Element as RootView;
			if (mp == null)
				return;
			TabBar.TintColor = mp.ActiveIconColor.ToUIColor();
		}

		#endregion
	}
}
