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
		public ReactiveList<MonkeyCellViewModel> GetAll ()
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
			Monkeys = new ReactiveList<MonkeyCellViewModel> (monkeyList);
		}

		public ReactiveList<MonkeyCellViewModel> Monkeys { get; protected set; }
	}
}

