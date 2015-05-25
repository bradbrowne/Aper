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
using System.Linq;
using System.Collections.Specialized;

namespace XamarinFormsReactiveListView.ViewModels
{
	public class MonkeyListViewModel : ReactiveObject, IRoutableViewModel
	{
		IMonkeyService _monkeyService;
		public ObservableCollection<MonkeyCellViewModel> MonkeyList = new ObservableCollection<MonkeyCellViewModel>();

		public MonkeyListViewModel (IScreen hostScreen = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
			_monkeyService = Locator.Current.GetService<IMonkeyService>();

			var monkeyList = from m in _monkeyService.GetAll ()
				select new MonkeyCellViewModel(MonkeyList) { Monkey = m };
			foreach (var monkey in monkeyList) {
				MonkeyList.Add (monkey);
			}

			Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs> (ev => MonkeyList.CollectionChanged += ev, ev => MonkeyList.CollectionChanged -= ev)
				.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Remove)
				.Subscribe (x => {
					Debug.WriteLine("Remove NotifyCollectionChangedEventHandler: " + x.EventArgs.Action.ToString());
				});
			Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs> (ev => MonkeyList.CollectionChanged += ev, ev => MonkeyList.CollectionChanged -= ev)
				.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add)
				.Subscribe (x => {
					Debug.WriteLine("Add NotifyCollectionChangedEventHandler: " + x.EventArgs.Action.ToString());
					//throw new Exception("Error in Add");
				});
			
			AddMonkey = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("AddMonkey");
					var monkey = new Monkey { Name = DateTime.Now.ToString() };
					_monkeyService.Add(monkey);
					MonkeyList.Add(new MonkeyCellViewModel(MonkeyList){ Monkey = monkey });
				});
			AddMonkey.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Adding Monkey", ex))
				.Subscribe(result => {
					Debug.WriteLine("{0}", result);
				});

			RemoveMonkey = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("RemoveMonkey");
					var monkey = MonkeyList[0];
					_monkeyService.Remove(monkey.Monkey);
					MonkeyList.Remove(monkey);
				});
			RemoveMonkey.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Removing Monkey", ex))
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
		public ReactiveCommand<Unit> RemoveMonkey { get; protected set; }
		public ReactiveCommand<Unit> Select { get; protected set; }

		private object selectedItem;
		public object SelectedItem
		{
			get { return this.selectedItem; }
			set { this.selectedItem = value; }
		}
	}
}

