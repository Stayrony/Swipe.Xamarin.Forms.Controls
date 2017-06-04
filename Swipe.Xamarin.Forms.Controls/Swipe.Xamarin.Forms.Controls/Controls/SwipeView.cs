using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Controls
{
	public class SwipeView : ContentView
	{
		private GestureViewRecognizer _touchView;
		private const int _lenghtToClick = 10;
		private double _movingDistance;
		private bool _isTouchesBegan = false;
		private double _startTouchesMovedX;
		private AbsoluteLayout _absoluteLayout;

		private double _startTouchX;
		private double _offsetX;
		private double _swappingBoxPanGesturePosX;
		private ClickableContentView _swipeRightClickableView;
		private ClickableContentView _swipeLeftClickableView;

		public SwipeView()
		{
		}

		#region -- Public properties --

		public static readonly BindableProperty TouchCommandProperty =
			BindableProperty.Create(nameof(TouchCommand), typeof(ICommand), typeof(SwipeView), default(ICommand));

		public ICommand TouchCommand
		{
			get { return (ICommand)GetValue(TouchCommandProperty); }
			set { SetValue(TouchCommandProperty, value); }
		}

		public static readonly BindableProperty TouchCommandParameterProperty =
			BindableProperty.Create(nameof(TouchCommandParameter), typeof(object), typeof(SwipeView), default(object));

		public object TouchCommandParameter
		{
			get { return (object)GetValue(TouchCommandParameterProperty); }
			set { SetValue(TouchCommandParameterProperty, value); }
		}

		public static readonly BindableProperty RightCommandProperty =
			BindableProperty.Create(nameof(RightCommand), typeof(ICommand), typeof(SwipeView), default(ICommand));

		public ICommand RightCommand
		{
			get { return (ICommand)GetValue(RightCommandProperty); }
			set { SetValue(RightCommandProperty, value); }
		}

		public static readonly BindableProperty RightCommandParameterProperty =
			BindableProperty.Create(nameof(RightCommandParameter), typeof(object), typeof(SwipeView), default(object));

		public object RightCommandParameter
		{
			get { return (object)GetValue(RightCommandParameterProperty); }
			set { SetValue(RightCommandParameterProperty, value); }
		}

		public static readonly BindableProperty LeftCommandProperty =
			BindableProperty.Create(nameof(LeftCommand), typeof(ICommand), typeof(SwipeView), default(ICommand));

		public ICommand LeftCommand
		{
			get { return (ICommand)GetValue(LeftCommandProperty); }
			set { SetValue(LeftCommandProperty, value); }
		}

		public static readonly BindableProperty LeftCommandParameterProperty =
			BindableProperty.Create(nameof(LeftCommandParameter), typeof(object), typeof(SwipeView), default(object));

		public object LeftCommandParameter
		{
			get { return (object)GetValue(LeftCommandParameterProperty); }
			set { SetValue(LeftCommandParameterProperty, value); }
		}

		public static readonly BindableProperty LeftViewProperty =
			BindableProperty.Create(nameof(LeftView), typeof(View), typeof(SwipeView), default(View));

		public View LeftView
		{
			get { return (View)GetValue(LeftViewProperty); }
			set { SetValue(LeftViewProperty, value); }
		}

		public static readonly BindableProperty RightViewProperty =
			BindableProperty.Create(nameof(RightView), typeof(View), typeof(SwipeView), default(View));

		public View RightView
		{
			get { return (View)GetValue(RightViewProperty); }
			set { SetValue(RightViewProperty, value); }
		}

		public static readonly BindableProperty SwippingViewProperty =
			BindableProperty.Create(nameof(SwippingView), typeof(View), typeof(SwipeView), default(View));

		public View SwippingView
		{
			get { return (View)GetValue(SwippingViewProperty); }
			set { SetValue(SwippingViewProperty, value); }
		}

		#endregion

		#region -- Overrides --

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			//FIXME Maybe the method should not be called here
			InitSwapUI();
		}

		#endregion

		#region -- Private helpers --

		private void OnLeftContentClick()
		{
			LeftCommand?.Execute(LeftCommandParameter);
		}
		private void OnRightContentClick()
		{
			RightCommand?.Execute(RightCommandParameter);
		}

		private void InitSwapUI()
		{
			_touchView = new GestureViewRecognizer();
			_touchView.OnTouchesBegan += _touchHandler_OnTouchesBegan;
			_touchView.OnTouchesMoved += _touchHandler_OnTouchesMoved;
			_touchView.OnTouchesEnded += _touchHandler_OnTouchesEnded;
			_touchView.OnTouchesCancelled += _touchHandler_OnTouchesCancelled;

			_absoluteLayout = new AbsoluteLayout();

			_swipeLeftClickableView = new ClickableContentView()
			{
				Content = LeftView,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
			_swipeLeftClickableView.Command = new Command(OnLeftContentClick);

			_swipeRightClickableView = new ClickableContentView()
			{
				Content = RightView,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
			_swipeRightClickableView.Command = new Command(OnRightContentClick);

			var grid = new Grid()
			{
				BackgroundColor = Color.FromHex("#888888"),
				HeightRequest = 250,
				ColumnSpacing = 0,
				ColumnDefinitions = {
					new ColumnDefinition { Width =  new GridLength(0.4, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) },
					new ColumnDefinition { Width =  new GridLength(0.4, GridUnitType.Star) },
				},
			};

			grid.Children.Add(_swipeLeftClickableView, 0, 1, 0, 1);
			grid.Children.Add(_swipeRightClickableView, 2, 3, 0, 1);
			_absoluteLayout.Children.Add(grid, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
			_absoluteLayout.Children.Add(grid);

			_absoluteLayout.Children.Add(SwippingView, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
			_absoluteLayout.Children.Add(_touchView, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

			this.Content = _absoluteLayout;
		}

		private async Task CompleteSwappingAsync(double offsetX)
		{
			if (offsetX > _swipeLeftClickableView.Width / 2)
				offsetX = _swipeLeftClickableView.Width;
			else if ((offsetX > 0) && (offsetX < _swipeLeftClickableView.Width / 2))
				offsetX = 0;
			else if (offsetX < -_swipeRightClickableView.Width / 2)
				offsetX = -_swipeRightClickableView.Width;
			else if ((offsetX < 0) && (offsetX > -_swipeRightClickableView.Width / 2))
				offsetX = 0;

			//HACK work only for iOS, for Android see native Renderer
			MessagingCenter.Send<SwipeView, bool>(this, "IsScrollListEnabled", false);

			await SwippingView.TranslateTo(offsetX, 0);
			_touchView.TranslationX = offsetX;
			MessagingCenter.Send<SwipeView, bool>(this, "IsScrollListEnabled", true);
		}

		private void _touchHandler_OnTouchesBegan(object sender, IEnumerable<NGraphics.Point> e)
		{
			// should clear movingDistance because this value uses for click.
			_movingDistance = 0;
			_startTouchX = e.FirstOrDefault().X;
			_swappingBoxPanGesturePosX = SwippingView.TranslationX;
			_isTouchesBegan = true;
		}

		private void _touchHandler_OnTouchesMoved(object sender, IEnumerable<NGraphics.Point> e)
		{
			MessagingCenter.Send<SwipeView, bool>(this, "IsScrollListEnabled", false);

			var curPointX = e.FirstOrDefault().X;
			if (_isTouchesBegan)
			{
				_startTouchesMovedX = curPointX;
				_isTouchesBegan = false;
			}

			_offsetX = _swappingBoxPanGesturePosX + curPointX - _startTouchesMovedX;
			_movingDistance += curPointX - _startTouchX;

			if (_offsetX > _swipeLeftClickableView.Width)
				_offsetX = _swipeLeftClickableView.Width;
			else if (_offsetX < -_swipeRightClickableView.Width)
				_offsetX = -_swipeRightClickableView.Width;

			SwippingView.TranslationX = _offsetX;
		}

		private void _touchHandler_OnTouchesEnded(object sender, IEnumerable<NGraphics.Point> e)
		{
			var curPointX = e.FirstOrDefault();
			_movingDistance += curPointX.X - _startTouchX;
			OnMovingEnded();
		}

		private async void _touchHandler_OnTouchesCancelled(object sender, IEnumerable<NGraphics.Point> e)
		{
			await CompleteSwappingAsync(_offsetX);
		}

		private void OnClick()
		{
			TouchCommand?.Execute(TouchCommandParameter);
		}

		private bool IsClick()
		{
			return Math.Abs(_movingDistance) < _lenghtToClick;
		}

		private async void OnMovingEnded()
		{
			var isClicked = IsClick();
			await CompleteSwappingAsync(_offsetX);
			if (isClicked)
			{
				OnClick();
			}
		}

		#endregion
	}
}
