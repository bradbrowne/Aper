﻿using System;
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

