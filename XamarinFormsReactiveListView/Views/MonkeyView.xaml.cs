using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsReactiveListView.ViewModels;
using ReactiveUI;

namespace XamarinFormsReactiveListView
{
	public partial class MonkeyView : ContentPage, IViewFor<MonkeyViewModel>
	{
		public MonkeyView ()
		{
			InitializeComponent ();

			this.Bind(ViewModel, x => x.Monkey.Name, x => x.Name.Text);
			this.BindCommand(ViewModel, vm => vm.Update, v => v.Update);
		}

		public MonkeyViewModel ViewModel {
			get { return (MonkeyViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<MonkeyView, MonkeyViewModel>(x => x.ViewModel, default(MonkeyViewModel), BindingMode.OneWay);

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (MonkeyViewModel)value; }
		}
	}
}

