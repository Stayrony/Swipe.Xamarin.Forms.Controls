using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using Swipe.Xamarin.Forms.Controls.Controls;
using Swipe.Xamarin.Forms.Controls.Droid.Renderers;

[assembly: ExportRenderer(typeof(GestureViewRecognizer), typeof(GestureViewRecognizerRenderer))]
namespace Swipe.Xamarin.Forms.Controls.Droid.Renderers
{
	public class GestureViewRecognizerRenderer : ViewRenderer
	{
		#region -- Overrides --

		public override bool OnTouchEvent(Android.Views.MotionEvent e)
		{
			var boxView = (GestureViewRecognizer)Element;
			var scale = boxView.Width / Width;

			var touchInfo = new[]{
				new NGraphics.Point(e.GetX() * scale, e.GetY() * scale)
			};

			var result = false;
			// Handle touch actions
			switch (e.Action)
			{
				case MotionEventActions.Down:
					if (boxView != null)
					{
						boxView.TouchesBegan(touchInfo);
						result = true;
					}
					break;

				case MotionEventActions.Move:
					if (boxView != null)
					{
						boxView.TouchesMoved(touchInfo);
						result = true;
					}
					break;

				case MotionEventActions.Up:
					if (boxView != null)
					{
						boxView.TouchesEnded(touchInfo);
						result = true;
					}
					break;

				case MotionEventActions.Cancel:
					if (boxView != null)
					{
						boxView.TouchesCancelled(touchInfo);
						result = true;
					}
					break;
			}

			return result;
		}

		#endregion
	}
}
