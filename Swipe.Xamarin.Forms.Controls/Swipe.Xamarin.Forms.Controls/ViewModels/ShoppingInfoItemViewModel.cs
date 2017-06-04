using System;
using System.Windows.Input;
using Swipe.Xamarin.Forms.Controls.Models;

namespace Swipe.Xamarin.Forms.Controls.ViewModels
{
	public class ShoppingInfoItemViewModel : ObservableObject
	{
		public ShoppingInfoItemViewModel()
		{
		}

		public ShoppingInfoItemViewModel(ShoppingInfoItem model)
		{
			Id = model.Id;
			Name = model.Name;
			IconUrl = model.IconUrl;
		}

		#region -- Public properties --

		public Guid Id { get;  set; }

		private string _IconUrl;
		public string IconUrl
		{
			get { return _IconUrl; }
			set
			{
				SetProperty(ref _IconUrl, value);
			}
		}

		private string _Name;
		public string Name
		{
			get { return _Name; }
			set
			{
				SetProperty(ref _Name, value);
			}
		}

		private ICommand _ShoppingInfoItemEditCommand;
		public ICommand ShoppingInfoEditCommand
		{
			get { return _ShoppingInfoItemEditCommand; }
			set { SetProperty(ref _ShoppingInfoItemEditCommand, value); }
		}

		private ICommand _ShoppingInfoItemDeleteCommand;
		public ICommand ShoppingInfoItemDeleteCommand
		{
			get { return _ShoppingInfoItemDeleteCommand; }
			set { SetProperty(ref _ShoppingInfoItemDeleteCommand, value); }
		}

		private ICommand _ShoppingInfoItemClickedCommand;
		public ICommand ShoppingInfoItemClickedCommand
		{
			get { return _ShoppingInfoItemClickedCommand; }
			set { SetProperty(ref _ShoppingInfoItemClickedCommand, value); }
		}

		#endregion
	}
}
