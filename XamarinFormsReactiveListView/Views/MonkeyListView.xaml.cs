using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using System.Reactive;
using XamarinFormsReactiveListView.ViewModels;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using Splat;

namespace XamarinFormsReactiveListView.Views
{
	public partial class MonkeyListView : ContentPage, IViewFor<MonkeyListViewModel>, IEnableLogger
	{
		public MonkeyListView () 
		{
			InitializeComponent ();

			this.OneWayBind(ViewModel, x => x.MonkeyCellViewModels, x => x.MonkeyList.ItemsSource);
			this.BindCommand(ViewModel, vm => vm.AddMonkey, v => v.AddMonkey);
			this.OneWayBind(ViewModel, vm => vm.IsPullToRefreshEnabled, v => v.MonkeyList.IsPullToRefreshEnabled);

			this.BindCommand(ViewModel, vm => vm.Refresh, v => v.MonkeyList.RefreshCommand);
			Observable.FromEventPattern<EventHandler, EventArgs> (ev => MonkeyList.Refreshing += ev, ev => MonkeyList.Refreshing -= ev)
				.Subscribe (x => {
					this.Log().Debug("Refreshing");
					ViewModel.Refresh.ExecuteAsyncTask(x.EventArgs);
					((ListView)x.Sender).EndRefresh();
				});

			this.Bind(ViewModel, vm => vm.SelectedItem, v => v.MonkeyList.SelectedItem);

			UserError.RegisterHandler(async ue =>
				{
					RxApp.MainThreadScheduler.ScheduleAsync(async (scheduler, token) =>
						{
							//if (ue.InnerException != null)
							//await HostScreen.Router.Navigate.ExecuteAsync(new LoginViewModel(HostScreen));
							await DisplayAlert(ue.ErrorMessage, ue.InnerException.Message, "OK");
						});
					return await Observable.Return(RecoveryOptionResult.FailOperation);
				});
		}

		public MonkeyListViewModel ViewModel {
			get { return (MonkeyListViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<MonkeyListView, MonkeyListViewModel>(x => x.ViewModel, default(MonkeyListViewModel), BindingMode.OneWay);

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (MonkeyListViewModel)value; }
		}
	}
}

