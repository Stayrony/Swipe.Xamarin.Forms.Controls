using System;
using System.Collections.ObjectModel;

namespace Swipe.Xamarin.Forms.Controls.ViewModels
{
	public class ShoppingListViewModel : BasePageViewModel
	{
		public ShoppingListViewModel()
		{
			ShoppingInfoItems = new ObservableCollection<ShoppingInfoItemViewModel>();
			ShoppingInfoItems.Add(
				new ShoppingInfoItemViewModel()
			{
				Id = Guid.NewGuid(),
				Name = "Banana",
			});
			ShoppingInfoItems.Add(
				new ShoppingInfoItemViewModel()
				{
					Id = Guid.NewGuid(),
					Name = "Apple",
				});
			ShoppingInfoItems.Add(
				new ShoppingInfoItemViewModel()
				{
					Id = Guid.NewGuid(),
					Name = "Banana",
				});
			ShoppingInfoItems.Add(
				new ShoppingInfoItemViewModel()
				{
					Id = Guid.NewGuid(),
					Name = "Apple",
				});
		}

		#region -- Public properties --

		private ObservableCollection<ShoppingInfoItemViewModel> _ShoppingInfoItems;
		public ObservableCollection<ShoppingInfoItemViewModel> ShoppingInfoItems
		{
			get { return _ShoppingInfoItems; }
			set { SetProperty(ref _ShoppingInfoItems, value); }
		}

		#endregion
	}
}
