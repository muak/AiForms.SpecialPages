using System.Reflection;
using System.Runtime.Remoting.Contexts;
using AiForms.SpecialPages;
using AiForms.SpecialPages.Droid;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPageEx), typeof(TabbedPageExRenderer))]
namespace AiForms.SpecialPages.Droid
{
	public class TabbedPageExRenderer : TabbedPageRenderer,TabLayout.IOnTabSelectedListener
	{

		private TabbedPageEx tabbedEx;
		private TabLayout tabs;
		private Window window;

		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e) {
			base.OnElementChanged(e);

			var fieldInfo = typeof(TabbedPageRenderer).GetField("_tabLayout", BindingFlags.Instance | BindingFlags.NonPublic);
			tabs = (TabLayout)fieldInfo.GetValue(this);

			var teardownPage = typeof(TabbedPageRenderer).GetMethod("TeardownPage", BindingFlags.Instance | BindingFlags.NonPublic);

			window = (Context as FormsAppCompatActivity).Window;

			if (e.OldElement != null) {

			}

			if (e.NewElement != null) {

				tabbedEx = Element as TabbedPageEx;
				if (!tabbedEx.IsDefaultColor) {
					//OnTabSelectedListenerを上書きする
					tabs.SetOnTabSelectedListener(this);
				}
                
				// https://github.com/xamarin/Xamarin.Forms/blob/master/Xamarin.Forms.Platform.Android/AppCompat/TabbedPageRenderer.cs#L297
				// OnPagePropertyChangedでいらんことしてるので即TeardownPageを呼び出して解除する
				// （TabのTextを子ページのTitleと連動させているが、TabのTextはTabAttributeで設定するようにしているので不要）
				foreach(var page in Element.Children){
					teardownPage.Invoke(this,new object[]{page});
				}

				for (var i = 0; i < tabbedEx.TabAttributes.Count; i++) {
					var attr = tabbedEx.TabAttributes[i];

					if (string.IsNullOrEmpty(attr.Resource)) continue;

					var bitmap = SvgToBitmap.GetBitmap(attr.Resource,24,24);
					var icon = new BitmapDrawable(Context.Resources, bitmap);
					var tab = tabs.GetTabAt(i);
					tab.SetIcon(icon);

					if (!tabbedEx.IsDefaultColor || !attr.IsDefaultColor) {
						var color = tabbedEx.SelectedColor.ToAndroid();

						if (i == 0) {
							if (attr.SelectedColor != Xamarin.Forms.Color.Default) {
								color = attr.SelectedColor.ToAndroid();
							}
							tabs.SetSelectedTabIndicatorColor(color);
                            if (tabbedEx.StatusBarBackColor != Xamarin.Forms.Color.Default) {
                                window.SetStatusBarColor(tabbedEx.StatusBarBackColor.ToAndroid());
                            }
							else if (attr.StatusBarBackColor != Xamarin.Forms.Color.Default) {
								window.SetStatusBarColor(attr.StatusBarBackColor.ToAndroid());
							}
						}
						else {
							color = tabbedEx.UnSelectedColor.ToAndroid();
							if (attr.UnSelectedColor != Xamarin.Forms.Color.Default) {
								color = attr.UnSelectedColor.ToAndroid();
							}
						}
						tab.Icon.SetTint(color);
						tabs.SetTabTextColors(tabbedEx.UnSelectedTextColor.ToAndroid(), tabbedEx.SelectedTextColor.ToAndroid());
					}

					if (tabbedEx.IsTextHidden) {
						tab.SetText("");
					}
				}
			}
		}

		void TabLayout.IOnTabSelectedListener.OnTabReselected(TabLayout.Tab tab) {

		}
		void TabLayout.IOnTabSelectedListener.OnTabSelected(TabLayout.Tab tab) {
			if (tabbedEx == null)
				return;

			int selectedIndex = tab.Position;


			var attr = tabbedEx.TabAttributes[selectedIndex];
			if (attr == null) return;

			var color = tabbedEx.SelectedColor.ToAndroid();
			if (attr.SelectedColor != Xamarin.Forms.Color.Default) {
				color = attr.SelectedColor.ToAndroid();
			}

			tab.Icon.SetTint(color);
			tabs.SetSelectedTabIndicatorColor(color);

            if (tabbedEx.StatusBarBackColor != Xamarin.Forms.Color.Default) {
                window.SetStatusBarColor(tabbedEx.StatusBarBackColor.ToAndroid());
            }
			else if (attr.StatusBarBackColor != Xamarin.Forms.Color.Default) {
				window.SetStatusBarColor(attr.StatusBarBackColor.ToAndroid());
			}

			tabbedEx.Title = tabbedEx.Children[selectedIndex].Title;
			tabbedEx.Children[selectedIndex].PropertyChanged += CurrentPage_PropertyChanged;

			if (Element.Children.Count > selectedIndex && selectedIndex >= 0) {
				Element.CurrentPage = Element.Children[selectedIndex];
			}

		}

		void TabLayout.IOnTabSelectedListener.OnTabUnselected(TabLayout.Tab tab) {
            if (tabbedEx == null) return;

			int selectedIndex = tab.Position;

			var attr = tabbedEx.TabAttributes[selectedIndex];
			if (attr == null) return;

			var color = tabbedEx.UnSelectedColor.ToAndroid();
			if (attr.UnSelectedColor != Xamarin.Forms.Color.Default) {
				color = attr.UnSelectedColor.ToAndroid();
			}

			tab.Icon.SetTint(color);

			tabbedEx.Children[selectedIndex].PropertyChanged -= CurrentPage_PropertyChanged;
		}

		void CurrentPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == Page.TitleProperty.PropertyName) {
				tabbedEx.Title = (sender as Page).Title;
			}
		}
	}
}

