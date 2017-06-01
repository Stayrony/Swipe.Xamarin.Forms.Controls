using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Views
{
	public class BaseContentPage : ContentPage
	{
		public BaseContentPage()
		{
			this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
			ViewModelLocator.SetAutowireViewModel(this, true);
			NavigationPage.SetHasNavigationBar(this, false);
		}

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
				actionsHandler.OnDisappearing();
		}

		#endregion
	}
}
