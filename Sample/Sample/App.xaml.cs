using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Unity;
using Sample.Views;
using Microsoft.Practices.Unity;

namespace Sample
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override async void OnInitialized()
		{
			InitializeComponent();

			AiForms.SpecialPages.SvgLoader.Init(this.GetType());

			//await NavigationService.NavigateAsync("MyNavigationPage/MyTabbed");

			var tabbed = new TabbedHasNaviPage();
			var naviA = new NaviA();
			var naviB = new NaviB();
			var naviC = new NaviC();

			await naviA.PushAsync(Container.Resolve<MainPage>());
			await naviB.PushAsync(Container.Resolve<SecondPage>());
			await naviC.PushAsync(Container.Resolve<NextPage>());

			tabbed.Children.Add(naviA);
			tabbed.Children.Add(naviB);
			tabbed.Children.Add(naviC);

			MainPage = tabbed;
		}

		protected override void RegisterTypes()
		{
			this.GetType().GetTypeInfo().Assembly.DefinedTypes
			          .Where(t => t.Namespace?.EndsWith(".Views", System.StringComparison.Ordinal) ?? false)
			          .ForEach(t => {
			              Container.RegisterTypeForNavigation(t.AsType(), t.Name);
			          });
		}
	}
}

