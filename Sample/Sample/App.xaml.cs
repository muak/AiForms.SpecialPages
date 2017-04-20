using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Unity;
using Sample.Views;

namespace Sample
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override void OnInitialized()
		{
			InitializeComponent();

			AiForms.SpecialPages.SvgLoader.Init(this.GetType());

			NavigationService.NavigateAsync("MyTabbed");
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

