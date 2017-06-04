using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Controls
{
	public class TopGradientBGControl : ContentView
	{
		public TopGradientBGControl()
		{
			SKCanvasView canvasView = new SKCanvasView();
			canvasView.PaintSurface += OnCanvasViewPaintSurface;
			Content = canvasView;
		}

		private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			SKImageInfo info = e.Info;
			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			var colors = new SKColor[] { new SKColor(85, 124, 131), new SKColor(58, 175, 169), new SKColor(230, 239, 194) };
			var shader = SKShader.CreateLinearGradient(new SKPoint(0, info.Height / 2), new SKPoint(info.Width, info.Height / 2), colors, null, SKShaderTileMode.Clamp);
			var paint = new SKPaint() { Shader = shader };
			canvas.DrawPaint(paint);
		}
	}
}
