using System;
using System.Collections.Generic;
using CoreGraphics;
using Swipe.Xamarin.Forms.Controls.Controls;
using Swipe.Xamarin.Forms.Controls.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GestureViewRecognizer), typeof(GestureViewRecognizerRenderer))]
namespace Swipe.Xamarin.Forms.Controls.iOS.Renderers
{
	public class GestureViewRecognizerRenderer : ViewRenderer
	{
		#region -- Overrides --

		public override void TouchesBegan(Foundation.NSSet touches, UIKit.UIEvent evt)
		{
			var boxView = (GestureViewRecognizer)Element;
			if (boxView != null)
			{
				boxView.TouchesBegan(GetPointsFromTouches(touches));
			}
		}

		public override void TouchesMoved(Foundation.NSSet touches, UIKit.UIEvent evt)
		{
			var boxView = (GestureViewRecognizer)Element;
			if (boxView != null)
			{
				boxView.TouchesMoved(GetPointsFromTouches(touches));
			}
		}

		public override void TouchesEnded(Foundation.NSSet touches, UIKit.UIEvent evt)
		{
			var boxView = (GestureViewRecognizer)Element;
			if (boxView != null)
			{
				boxView.TouchesEnded(GetPointsFromTouches(touches));
			}
		}

		public override void TouchesCancelled(Foundation.NSSet touches, UIKit.UIEvent evt)
		{
			var boxView = (GestureViewRecognizer)Element;
			if (boxView != null)
			{
				boxView.TouchesCancelled(GetPointsFromTouches(touches));
			}
		}

		#endregion

		#region -- Private helpers --

		private IEnumerable<NGraphics.Point> GetPointsFromTouches(Foundation.NSSet touches)
		{
			var points = new List<NGraphics.Point>();

			foreach (var touch in touches.ToArray<UITouch>())
			{
				CGPoint point = touch.LocationInView(this);
				points.Add(new NGraphics.Point(point.X, point.Y));
			}

			return points;
		}

		#endregion
	}
}
