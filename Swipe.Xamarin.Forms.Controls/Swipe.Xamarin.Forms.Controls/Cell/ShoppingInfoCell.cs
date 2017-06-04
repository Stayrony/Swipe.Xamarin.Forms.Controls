using System;
using System.Windows.Input;
using Swipe.Xamarin.Forms.Controls.Controls;
using Swipe.Xamarin.Forms.Controls.Helpers;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Cell
{
	public class ShoppingInfoCell : ViewCell
	{
		public ShoppingInfoCell()
		{
			CreateLayout();
		}

		#region -- Public properties --

		public static readonly BindableProperty RightCommandProperty =
			BindableProperty.Create(nameof(RightCommand), typeof(ICommand), typeof(ShoppingInfoCell), default(ICommand));

		public ICommand RightCommand
		{
			get { return (ICommand)GetValue(RightCommandProperty); }
			set { SetValue(RightCommandProperty, value); }
		}

		public static readonly BindableProperty LeftCommandProperty =
			BindableProperty.Create(nameof(LeftCommand), typeof(ICommand), typeof(ShoppingInfoCell), default(ICommand));

		public ICommand LeftCommand
		{
			get { return (ICommand)GetValue(LeftCommandProperty); }
			set { SetValue(LeftCommandProperty, value); }
		}

		public static readonly BindableProperty TouchCommandProperty =
			BindableProperty.Create(nameof(TouchCommand), typeof(ICommand), typeof(ShoppingInfoCell), default(ICommand));

		public ICommand TouchCommand
		{
			get { return (ICommand)GetValue(TouchCommandProperty); }
			set { SetValue(TouchCommandProperty, value); }
		}

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ShoppingInfoCell), default(object));

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		#endregion

		#region -- Private helpers --

		private void CreateLayout()
		{
			var _mainGrid = new Grid()
			{
				HeightRequest = 50,
				BackgroundColor = Color.Wheat,
				RowSpacing = 0,
				RowDefinitions = {
				new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					//new RowDefinition { Height = new GridLength(0.15, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) },
					//new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) },
					//new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) },
				},
				//ColumnSpacing = 0,
				//ColumnDefinitions = {
				//new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) },
				//	new ColumnDefinition { Width = new GridLength(1.18, GridUnitType.Star) },
				//	new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
				//	new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
				//},
			};

			var nameLabel = new Label()
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				FontSize = StyleManager.GetAppResource<double>("FontSize_12"),
				TextColor = StyleManager.GetAppResource<Color>("textColor"),
				Text = "Hello"
			};
			//nameLabel.SetBinding(Label.TextProperty, "Name");

			var separator = new TopGradientBGControl()
			{
				HeightRequest = 1,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};

			_mainGrid.Children.Add(nameLabel, 0, 1, 0, 1);
			_mainGrid.Children.Add(separator, 0, 1, 1, 2);

			var rightLabel = new Label()
			{
				Text = "Delete",
				FontSize = StyleManager.GetAppResource<Double>("FontSize_15"),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			var leftLabel = new Label()
			{
				Text = "Edit",
				FontSize = StyleManager.GetAppResource<Double>("FontSize_15"),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			var swipeView = new SwipeView()
			{
				SwippingView = _mainGrid,
				LeftView = leftLabel,
				RightView = rightLabel,
				BindingContext = this,
			};
			swipeView.SetBinding(SwipeView.RightCommandProperty, RightCommandProperty.PropertyName);
			//TODO check 
			swipeView.RightCommandParameter = this;

			swipeView.SetBinding(SwipeView.LeftCommandProperty, LeftCommandProperty.PropertyName);
			swipeView.LeftCommandParameter = this;

			View = swipeView;
		}

		#endregion
	}
}
