using System;
using Swipe.Xamarin.Forms.Controls.Views;

namespace Swipe.Xamarin.Forms.Controls.ViewModels
{
	public class BasePageViewModel : BaseViewModel, IViewActionsHandler
	{
		#region -- IViewActionsHandler implementation --

		public virtual void OnAppearing()
		{
		}

		public virtual bool OnBackButtonPressed()
		{
			return false;
		}

		public virtual void OnDisappearing()
		{

		}

		#endregion
	}
}
