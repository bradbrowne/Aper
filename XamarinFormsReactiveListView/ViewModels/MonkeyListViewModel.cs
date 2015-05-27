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
using System.Threading.Tasks;
using System.ComponentModel;

namespace XamarinFormsReactiveListView.ViewModels
{
	public class MonkeyListViewModel : ReactiveObject, IRoutableViewModel
	{
		IMonkeyService _monkeyService;
		public ObservableCollection<MonkeyCellViewModel> MonkeyCellViewModels = new ObservableCollection<MonkeyCellViewModel>();

		private async void GetMonkeys()
		{
			MonkeyCellViewModels.Clear ();
			var monkeyCellViewModels = from m in await _monkeyService.GetAllAsync ()
				select new MonkeyCellViewModel(RemoveMonkey) { Monkey = m };
			foreach (var monkeyCellViewModel in monkeyCellViewModels) {
				MonkeyCellViewModels.Add (monkeyCellViewModel);
			}
		}

		public MonkeyListViewModel (IScreen hostScreen = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
			_monkeyService = Locator.Current.GetService<IMonkeyService>();

			GetMonkeys ();

			Refresh = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					MonkeyCellViewModels.Clear ();
					var monkeyCellViewModels = from m in await _monkeyService.GetAllAsync ()
						select new MonkeyCellViewModel(RemoveMonkey) { Monkey = m };
					foreach (var monkeyCellViewModel in monkeyCellViewModels) {
						MonkeyCellViewModels.Add (monkeyCellViewModel);
					}
				});
			Refresh.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Refreshing Monkeys", ex))
				.Subscribe(result => {
					Debug.WriteLine("{0}", result);
				});
			
			AddMonkey = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("AddMonkey");
					var monkey = new Monkey { Name = DateTime.Now.ToString() };
					await _monkeyService.InsertAsync(monkey);
					MonkeyCellViewModels.Add(new MonkeyCellViewModel(RemoveMonkey){ Monkey = monkey });
				});
			AddMonkey.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Adding Monkey", ex))
				.Subscribe(result => {
					Debug.WriteLine("{0}", result);
				});

			RemoveMonkey = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("RemoveMonkey");
					var monkey = model as MonkeyCellViewModel;
					await _monkeyService.DeleteAsync(monkey.Monkey);
					MonkeyCellViewModels.Remove(monkey);
				});
			RemoveMonkey.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Removing Monkey", ex))
				.Subscribe(result => Debug.WriteLine("{0}", result));
			
			this.WhenAnyValue (x => x.SelectedItem)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Where(x => x != null)
				.Select(x => x as MonkeyCellViewModel)
				.Log(this, "SelectedItem", x => x.Monkey.Name)
				.Subscribe (x => {
					this.SelectedItem = null;
					this.Log().Debug("SelectedItem: " + x.Monkey.Name);
				});
		}

		public IScreen HostScreen { get; protected set; }

		public string UrlPathSegment {
			get { return "Monkey List"; }
		}

		public ReactiveCommand<Unit> AddMonkey { get; protected set; }
		public ReactiveCommand<Unit> RemoveMonkey { get; protected set; }
		public ReactiveCommand<Unit> Refresh { get; protected set; }
		public ReactiveCommand<Unit> Select { get; protected set; }

		private object selectedItem;
		public object SelectedItem
		{
			get { return this.selectedItem; }
			set { this.RaiseAndSetIfChanged(ref selectedItem, value); }
		}

		public bool IsPullToRefreshEnabled {
			get { 
				return true; 
			}
		}

		private bool isBusy;
		public bool IsBusy
		{
			get { return isBusy; }
			set { this.RaiseAndSetIfChanged(ref isBusy, value); }
		}
	}
}

