using Android.App;
using Android.Content.PM;
using Android.OS;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Aper.Droid
{
	[Activity (Label = "Aper", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			Forms.Init (this, bundle);

			base.OnCreate (bundle);

			var view = RxApp.SuspensionHost.GetAppState<AppBootstrapper>().CreateMainPage();
			SetPage(view);
		}
	}
}

