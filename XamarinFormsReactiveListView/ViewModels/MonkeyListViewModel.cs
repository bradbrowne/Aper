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
		}

		public IScreen HostScreen { get; protected set; }

		public string UrlPathSegment {
			get { return "Contact List"; }
		}

		public ReactiveCommand<Unit> AddMonkey { get; protected set; }

		public ReactiveList<MonkeyCellViewModel> Monkeys { get; protected set; }
	}
}

