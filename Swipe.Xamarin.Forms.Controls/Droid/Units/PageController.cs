using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Droid.Units
{
	public class PageController : IPageController
	{
		private ReflectedProxy<Page> _proxy;

		PageController(Page page)
		{
			_proxy = new ReflectedProxy<Page>(page);
		}

		#region -- Public properties --

		public Rectangle ContainerArea
		{
			get
			{
				return _proxy.GetPropertyValue<Rectangle>();
			}

			set
			{
				_proxy.SetPropertyValue(value);
			}
		}

		public bool IgnoresContainerArea
		{
			get
			{
				return _proxy.GetPropertyValue<bool>();
			}

			set
			{
				_proxy.SetPropertyValue(value);
			}
		}

		public ObservableCollection<Element> InternalChildren
		{
			get
			{
				return _proxy.GetPropertyValue<ObservableCollection<Element>>();
			}

			set
			{
				_proxy.SetPropertyValue(value);
			}
		}

		#endregion

		#region -- IPageController implementation --

		public void SendAppearing()
		{
			_proxy.Call();
		}

		public void SendDisappearing()
		{
			_proxy.Call();
		}

		#endregion

		public static IPageController Create(Page page)
		{
			return new PageController(page);
		}
	}
}
