using System;
namespace Swipe.Xamarin.Forms.Controls.Views
{
	public interface IViewActionsHandler
	{
		bool OnBackButtonPressed();
		void OnAppearing();
		void OnDisappearing();
	}
}
