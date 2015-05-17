using System;
using ReactiveUI;
using System.Reactive;
using Xamarin.Forms;
using Splat;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinFormsReactiveListView.ViewModels
{
	public class MonkeyListViewModel : ReactiveObject, IRoutableViewModel
	{
		public MonkeyListViewModel (IScreen hostScreen = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

			AddMonkey = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("AddMonkey");
					Monkeys.Add(new MonkeyCellViewModel { Monkey = new Monkey { Name = DateTime.Now.ToString() } });
				});
			
			Monkeys = new ObservableCollection<MonkeyCellViewModel> (new List<MonkeyCellViewModel> {
				new MonkeyCellViewModel { Monkey = new Monkey { Name = "George" } },
				new MonkeyCellViewModel { Monkey = new Monkey { Name = "Bobo" } },
				new MonkeyCellViewModel { Monkey = new Monkey { Name = "Magic" } }});
		}

		public IScreen HostScreen { get; protected set; }

		public string UrlPathSegment {
			get { return "Contact List"; }
		}

		public ReactiveCommand<Unit> AddMonkey { get; protected set; }

		public ObservableCollection<MonkeyCellViewModel> Monkeys { get; protected set; }
	}
}

