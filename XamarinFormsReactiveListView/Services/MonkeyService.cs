using System;
using System.Collections.ObjectModel;
using XamarinFormsReactiveListView.ViewModels;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;
using ReactiveUI;

namespace XamarinFormsReactiveListView
{
	public class MonkeyService : IMonkeyService
	{
		public ObservableCollection<Monkey> GetAll ()
		{
			return Monkeys;
		}

		public void Remove(Monkey monkey)
		{
			Monkeys.Remove (monkey);
		}

		public MonkeyService ()
		{
			var monkeyList = new List<Monkey> {
				new Monkey { Name = "George" },
				new Monkey { Name = "Bobo" },
				new Monkey { Name = "Magic" }
			};
			Monkeys = new ObservableCollection<Monkey> (monkeyList);
		}

		public ObservableCollection<Monkey> Monkeys { get; protected set; }
	}
}

