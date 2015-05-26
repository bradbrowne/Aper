using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ReactiveUI;

namespace XamarinFormsReactiveListView.Droid
{
	[Activity (Label = "XamarinFormsReactiveListView.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			global::Xamarin.Forms.Forms.Init (this, bundle);

			base.OnCreate (bundle);

			var view = RxApp.SuspensionHost.GetAppState<AppBootstrapper>().CreateMainPage();
			SetPage(view);
		}
	}
}

