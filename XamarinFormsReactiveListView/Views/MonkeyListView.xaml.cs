using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using System.Reactive;
using XamarinFormsReactiveListView.ViewModels;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace XamarinFormsReactiveListView.Views
{
	public partial class MonkeyListView : ContentPage, IViewFor<MonkeyListViewModel>
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
					Debug.WriteLine("Refreshing");
					ViewModel.Refresh.ExecuteAsyncTask(x.EventArgs);
					((ListView)x.Sender).EndRefresh();
				});

			Observable.FromEventPattern<SelectedItemChangedEventArgs> (ev => MonkeyList.ItemSelected += ev, ev => MonkeyList.ItemSelected -= ev)
				.Where (x => x.EventArgs.SelectedItem != null)
				.Subscribe (x => {
					((ListView)x.Sender).SelectedItem = null;
					ViewModel.Select.Execute(x.EventArgs.SelectedItem);
				});
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

