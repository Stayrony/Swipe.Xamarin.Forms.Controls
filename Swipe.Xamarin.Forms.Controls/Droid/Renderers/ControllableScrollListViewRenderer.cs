using System;
using Android.Views;
using Swipe.Xamarin.Forms.Controls.Controls;
using Swipe.Xamarin.Forms.Controls.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ControllableScrollListView), typeof(ControllableScrollListViewRenderer))]
namespace Swipe.Xamarin.Forms.Controls.Droid.Renderers
{
	public class ControllableScrollListViewRenderer : ListViewRenderer
	{
		private ControllableScrollListView _extendedListView;
		private int _mPosition;
		private float xDistance, yDistance, lastX, lastY;

		#region -- Overrides --

		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
				return;

			_extendedListView = (ControllableScrollListView)Element;
		}

		public override bool DispatchTouchEvent(MotionEvent e)
		{
			if (e.ActionMasked == MotionEventActions.Down)
			{
				// Record the position the list the touch landed on
				_mPosition = this.Control.PointToPosition((int)e.GetX(), (int)e.GetY());

				xDistance = yDistance = 0f;
				lastX = e.GetX();
				lastY = e.GetY();

				return base.DispatchTouchEvent(e);
			}

			if (e.ActionMasked == MotionEventActions.Move)
			{
				float curX = e.GetX();
				float curY = e.GetY();
				xDistance += Math.Abs(curX - lastX);
				yDistance += Math.Abs(curY - lastY);
				lastX = curX;
				lastY = curY;
				if (xDistance > yDistance)
				{
					_extendedListView.IsEnabled = false;
				}
			}

			if (e.ActionMasked == MotionEventActions.Up)
			{

				_extendedListView.IsEnabled = true;

				//Check if we are still within the same view
				if (this.Control.PointToPosition((int)e.GetX(), (int)e.GetY()) == _mPosition)
				{
					base.DispatchTouchEvent(e);
				}
				else
				{
					// Clear pressed state, cancel the action
					Pressed = false;
					Invalidate();
					return true;
				}
			}
			return base.DispatchTouchEvent(e);
		}

		#endregion
	}
}
