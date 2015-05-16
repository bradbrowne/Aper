using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using Beeteem.Views;
using System.Reactive;

namespace XamarinFormsReactiveListView
{
	public partial class MonkeyListView : ContentPage, IViewFor<MonkeyListViewModel>
	{
		public MonkeyListView () 
		{
			InitializeComponent ();

			_constructor ();
		}

		public MonkeyListView (MonkeyListViewModel viewModel)
		{
			InitializeComponent ();

			this.ViewModel = viewModel;
			_constructor ();
		}

		public DataTemplate Cell { get; private set; }
		public ReactiveCommand<Unit> DeleteCommand { get; set; }

		private void _constructor()
		{
			this.OneWayBind(ViewModel, x => x.Monkeys, x => x.MonkeyList.ItemsSource);
			this.BindCommand(ViewModel, vm => vm.DeleteCommand, v => v.DeleteCommand);
			this.BindCommand(ViewModel, vm => vm.AddMonkey, v => v.AddMonkey);

			Cell = new DataTemplate(typeof(MonkeyCell));

			//Bind our cell's text and details properties
			Cell.SetBinding(MonkeyCell.TextProperty, "Name");
			Cell.SetBinding(MonkeyCell.DetailProperty, "Name");

			MonkeyList.ItemTemplate = Cell;
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

