using Foundation;
using ReactiveUI;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Aper.iOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : FormsApplicationDelegate
	{
		public AppDelegate()
		{
			RxApp.SuspensionHost.CreateNewAppState = () => new AppBootstrapper();
		}

		public override UIWindow Window { get; set; }
		AutoSuspendHelper suspendHelper;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();
			RxApp.SuspensionHost.SetupDefaultSuspendResume();

			suspendHelper = new AutoSuspendHelper(this);
			suspendHelper.FinishedLaunching(app, options);

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Calabash.Start();
			#endif

			Window = new UIWindow (UIScreen.MainScreen.Bounds);

			var bootstrapper = RxApp.SuspensionHost.GetAppState<AppBootstrapper>();

			Window.RootViewController = bootstrapper.CreateMainPage().CreateViewController();
			Window.MakeKeyAndVisible ();

			return true;
		}

		public override void DidEnterBackground(UIApplication application)
		{
			suspendHelper.DidEnterBackground(application);
		}

		public override void OnActivated(UIApplication application)
		{
			suspendHelper.OnActivated(application);
		}
	}
}

