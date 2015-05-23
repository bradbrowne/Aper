using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using System.Reactive;
using XamarinFormsReactiveListView.ViewModels;
using System.Diagnostics;
using System.Reactive.Linq;

namespace XamarinFormsReactiveListView.Views
{
	public partial class MonkeyListView : ContentPage, IViewFor<MonkeyListViewModel>
	{
		public MonkeyListView () 
		{
			InitializeComponent ();

			this.OneWayBind(ViewModel, x => x.Monkeys, x => x.MonkeyList.ItemsSource);
			this.BindCommand(ViewModel, vm => vm.AddMonkey, v => v.AddMonkey);

			Observable.FromEventPattern<SelectedItemChangedEventArgs> (ev => MonkeyList.ItemSelected += ev, ev => MonkeyList.ItemSelected -= ev)
				.Where (x => x.EventArgs.SelectedItem != null)
				.Subscribe (x => {
					((ListView)x.Sender).SelectedItem = null;
					ViewModel.Select.Execute(x.EventArgs.SelectedItem);
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

