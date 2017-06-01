using System;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Controls
{
	public class ControllableScrollListView : ListView
	{
		#region -- Public properties --

		public static readonly BindableProperty IsScrollEnabledProperty =
			BindableProperty.Create(nameof(IsScrollEnabled), typeof(bool), typeof(ControllableScrollListView), true);

		public bool IsScrollEnabled
		{
			get { return (bool)GetValue(IsScrollEnabledProperty); }
			set
			{
				SetValue(IsScrollEnabledProperty, value);
				OnPropertyChanged(nameof(IsScrollEnabled));
			}
		}

		#endregion
	}
}
