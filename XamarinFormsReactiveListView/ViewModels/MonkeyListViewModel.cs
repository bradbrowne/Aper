using System;
using ReactiveUI;
using System.Reactive;
using Xamarin.Forms;
using Splat;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinFormsReactiveListView
{
	public class MonkeyListViewModel : ReactiveObject, IRoutableViewModel
	{
		public MonkeyListViewModel (IScreen hostScreen = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

			DeleteCommand = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("DeleteCommand");
					var monkey = model as MonkeyCellViewModel;
					Monkeys.Remove(monkey);
				});

			AddMonkey = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("AddMonkey");
					Monkeys.Add(new MonkeyCellViewModel { Monkey = new Monkey { Name = DateTime.Now.ToString() }, DeleteCommand = DeleteCommand });
				});
			
			Monkeys = new ObservableCollection<MonkeyCellViewModel> (new List<MonkeyCellViewModel> {
				new MonkeyCellViewModel { Monkey = new Monkey { Name = "George" }, DeleteCommand = DeleteCommand },
				new MonkeyCellViewModel { Monkey = new Monkey { Name = "Bobo" }, DeleteCommand = DeleteCommand },
				new MonkeyCellViewModel { Monkey = new Monkey { Name = "Magic" }, DeleteCommand = DeleteCommand }});
		}

		public IScreen HostScreen { get; protected set; }

		public string UrlPathSegment {
			get { return "Contact List"; }
		}

		public ReactiveCommand<Unit> DeleteCommand { get; protected set; }
		public ReactiveCommand<Unit> AddMonkey { get; protected set; }

		public ObservableCollection<MonkeyCellViewModel> Monkeys { get; protected set; }
	}
}

