using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new Swipe_Xamarin_Forms_ControlsPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
