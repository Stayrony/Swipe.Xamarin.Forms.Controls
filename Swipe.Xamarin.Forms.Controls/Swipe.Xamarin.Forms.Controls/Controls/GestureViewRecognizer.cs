using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Controls
{
	public class GestureViewRecognizer : ContentView
	{
		public event EventHandler<IEnumerable<NGraphics.Point>> OnTouchesBegan;
		public event EventHandler<IEnumerable<NGraphics.Point>> OnTouchesMoved;
		public event EventHandler<IEnumerable<NGraphics.Point>> OnTouchesEnded;
		public event EventHandler<IEnumerable<NGraphics.Point>> OnTouchesCancelled;

		public virtual void TouchesBegan(IEnumerable<NGraphics.Point> point)
		{
			OnTouchesBegan?.Invoke(this, point);
		}

		public virtual void TouchesMoved(IEnumerable<NGraphics.Point> point)
		{
			OnTouchesMoved?.Invoke(this, point);
		}

		public virtual void TouchesEnded(IEnumerable<NGraphics.Point> point)
		{
			OnTouchesEnded?.Invoke(this, point);
		}

		public virtual void TouchesCancelled(IEnumerable<NGraphics.Point> point)
		{
			OnTouchesCancelled?.Invoke(this, point);
		}
	}
}
