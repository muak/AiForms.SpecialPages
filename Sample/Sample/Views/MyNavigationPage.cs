using System;
using AiForms.SpecialPages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Sample.Views
{
	public class MyNavigationPage:NavigationPageEx
	{
		public MyNavigationPage()
		{
			BarTextColor = Color.Orange;
			BarBackgroundColor = Color.White;
			StatusBarBackColor = Color.Orange;
		}
	}

	public class NaviA:NavigationPageEx{}
	public class NaviB:NavigationPageEx{}
	public class NaviC:NavigationPageEx{}
}
