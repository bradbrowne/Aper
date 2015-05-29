using System;
using Android.App;
using ReactiveUI;
using Android.Runtime;
using Aper.ViewModels;

namespace Aper.Droid
{
	[Application(Label = "Aper")]
	public class App : Application
	{
		public App()
		{
		}

		AutoSuspendHelper suspendHelper;

		App(IntPtr handle, JniHandleOwnership owner) : base(handle, owner) { }

		public override void OnCreate()
		{
			base.OnCreate();

			suspendHelper = new AutoSuspendHelper(this);

			RxApp.SuspensionHost.CreateNewAppState = () => {
				Console.WriteLine("Creating app state");
				return new AppBootstrapper();
			};

			RxApp.SuspensionHost.SetupDefaultSuspendResume();
		}
	}
}
