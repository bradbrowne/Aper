using System;
using ReactiveUI;
using System.Reactive;
using Xamarin.Forms;
using Splat;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;

namespace XamarinFormsReactiveListView.ViewModels
{
	public class MonkeyListViewModel : ReactiveObject, IRoutableViewModel
	{
		IMonkeyService _monkeyService;

		public MonkeyListViewModel (IScreen hostScreen = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
			_monkeyService = Locator.Current.GetService<IMonkeyService>();
			Monkeys = _monkeyService.GetAll ();

			AddMonkey = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("AddMonkey");
					Monkeys.Add(new MonkeyCellViewModel(_monkeyService) { Monkey = new Monkey { Name = DateTime.Now.ToString() } });
				});
			AddMonkey.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Adding Monkey", ex))
				.Subscribe(result => Debug.WriteLine("{0}", result));
			
			Select = ReactiveCommand.CreateAsyncTask (async (model, e) => {
				Debug.WriteLine("SelectedItemChangedEventArgs: " + ((MonkeyCellViewModel)model).Monkey.Name);
			});
			Select.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Selecting Monkey", ex))
				.Subscribe(result => Debug.WriteLine("{0}", result));
		}

		public IScreen HostScreen { get; protected set; }

		public string UrlPathSegment {
			get { return "Monkey List"; }
		}

		public ReactiveCommand<Unit> AddMonkey { get; protected set; }
		public ReactiveCommand<Unit> Select { get; protected set; }

		public ObservableCollection<MonkeyCellViewModel> Monkeys { get; protected set; }

		private object selectedItem;
		public object SelectedItem
		{
			get { return this.selectedItem; }
			set { this.selectedItem = value; }
		}
	}
}

