using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using System.Reactive;
using XamarinFormsReactiveListView.ViewModels;

namespace XamarinFormsReactiveListView.Views
{
	public partial class MonkeyListView : ContentPage, IViewFor<MonkeyListViewModel>
	{
		public MonkeyListView () 
		{
			InitializeComponent ();

			this.OneWayBind(ViewModel, x => x.Monkeys, x => x.MonkeyList.ItemsSource);
			this.BindCommand(ViewModel, vm => vm.AddMonkey, v => v.AddMonkey);
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

