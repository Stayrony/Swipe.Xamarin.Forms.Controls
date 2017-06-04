using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using NControl.Controls.iOS;
using NControl.iOS;
using UIKit;

namespace Swipe.Xamarin.Forms.Controls.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			NControlViewRenderer.Init();
			NControls.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
