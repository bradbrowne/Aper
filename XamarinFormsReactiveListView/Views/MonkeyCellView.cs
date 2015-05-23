using System;
using Xamarin.Forms;
using ReactiveUI;
using System.Reactive;
using System.Diagnostics;
using XamarinFormsReactiveListView.Models;
using XamarinFormsReactiveListView.ViewModels;

namespace XamarinFormsReactiveListView.Views
{
	public class MonkeyCellView : ViewCell, IViewFor<MonkeyCellViewModel>
	{
		public MonkeyCellView ()
		{
			var nameLabel = new Label ();
			nameLabel.SetBinding<MonkeyCellViewModel>(Label.TextProperty, f => f.Monkey.Name);
			nameLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
			nameLabel.VerticalOptions = LayoutOptions.Center;

			var viewLayout = new StackLayout () {
				Padding = new Thickness (10, 0, 10, 0),
				Orientation = StackOrientation.Horizontal,
				Children = { nameLabel }
			};

			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
			deleteAction.SetBinding (MenuItem.CommandProperty, new Binding ("DeleteCommand"));
			deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));

			this.ContextActions.Add (deleteAction);

			this.View = viewLayout;
		}

		public ReactiveCommand<Unit> DeleteCommand { get; set; }

		public MonkeyCellViewModel ViewModel {
			get { return (MonkeyCellViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<MonkeyCellView, MonkeyCellViewModel>(x => x.ViewModel, default(MonkeyCellViewModel), BindingMode.OneWay);

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (MonkeyCellViewModel)value; }
		}
	}
}

