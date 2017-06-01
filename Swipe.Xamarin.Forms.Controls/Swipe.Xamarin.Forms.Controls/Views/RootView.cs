using System;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Views
{
	public class RootView : TabbedPage
	{
		private Page ShoppingListPage = new ShoppingListView();
		private Page CookiePage = new CookieView();

		public RootView()
		{
			Children.Add(ShoppingListPage);
			Children.Add(CookiePage);

			ActiveIconColor = (Color)Application.Current.Resources["activeIcon"];

			NavigationPage.SetHasNavigationBar(this, false);

			this.Padding = new Thickness(0, 0, 0, Device.OnPlatform(0, 55, 0));

		}

		#region -- Public properties --

		public static readonly BindableProperty ActiveIconColorProperty =
			BindableProperty.Create(nameof(ActiveIconColor), typeof(Color), typeof(RootView), default(Color));

		public Color ActiveIconColor
		{
			get { return (Color)GetValue(ActiveIconColorProperty); }
			set { SetValue(ActiveIconColorProperty, value); }
		}

		#endregion

		#region -- Overrides --

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var actionsHandler = BindingContext as IViewActionsHandler;
			if (actionsHandler != null)
				actionsHandler.OnAppearing();
		}

		protected override bool OnBackButtonPressed()
		{
			bool isHandled = false;
			var actionsHandler = BindingContext as IViewActionsHandler;
			if (actionsHandler != null)
				isHandled = actionsHandler.OnBackButtonPressed();
			return isHandled || base.OnBackButtonPressed();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			var actionsHandler = BindingContext as IViewActionsHandler;
			if (actionsHandler != null)
				actionsHandler.OnAppearing();
		}

		#endregion
	}
}
