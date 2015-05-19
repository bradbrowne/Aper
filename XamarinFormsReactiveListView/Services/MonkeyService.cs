using System;
using System.Collections.ObjectModel;
using XamarinFormsReactiveListView.ViewModels;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;

namespace XamarinFormsReactiveListView
{
	public class MonkeyService : IMonkeyService
	{
		public ObservableCollection<MonkeyCellViewModel> GetAll ()
		{
			return Monkeys;
		}

		public void Remove(MonkeyCellViewModel monkey)
		{
			Monkeys.Remove (monkey);
		}

		public MonkeyService ()
		{
			var monkeyList = new List<MonkeyCellViewModel> {
				new MonkeyCellViewModel (this) { Monkey = new Monkey { Name = "George" } },
				new MonkeyCellViewModel (this) { Monkey = new Monkey { Name = "Bobo" } },
				new MonkeyCellViewModel (this) { Monkey = new Monkey { Name = "Magic" } }
			};
			Monkeys = new ObservableCollection<MonkeyCellViewModel> (monkeyList);
		}

		public ObservableCollection<MonkeyCellViewModel> Monkeys { get; protected set; }
	}
}

