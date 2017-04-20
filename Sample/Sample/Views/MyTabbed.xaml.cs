using AiForms.SpecialPages;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Sample.Views
{
	public partial class MyTabbed : TabbedPageEx
	{
		public MyTabbed()
		{
			InitializeComponent();

			SelectedColor = Color.Orange;
			UnSelectedColor = Color.DimGray;
			SelectedTextColor = Color.Gray;
			UnSelectedTextColor = Color.Silver;
			BarBackgroundColor = Color.White;
			StatusBarBackColor = Color.LightBlue;
			IsTextHidden = false;

			this.On<Android>().SetOffscreenPageLimit(2);

			TabAttributes = new List<TabAttribute>{
				new TabAttribute{
					Title = "Abc",
					Resource = "camera.svg",
				},
				new TabAttribute{
					Title = "Def",
					Resource = "colours.svg",
				},
				new TabAttribute{
					Title = "Xyz",
					Resource = "back-in-time.svg",
				},
			};
		}
	}
}

