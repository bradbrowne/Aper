using System;
using ReactiveUI;
using XamarinFormsReactiveListView.Models;
using System.Reactive;
using Splat;
using System.Reactive.Linq;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace XamarinFormsReactiveListView.ViewModels
{
	public class MonkeyCellViewModel : ReactiveObject
	{
		public MonkeyCellViewModel (ObservableCollection<MonkeyCellViewModel> monkeyList)
		{
			DeleteCommand = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("DeleteCommand");
					var monkey = model as MonkeyCellViewModel;
					monkeyList.Remove(monkey);
				});
			DeleteCommand.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Deleting Monkey", ex))
				.Subscribe(result => Debug.WriteLine("{0}", result));
		}

		public Monkey Monkey { get; set; }

		public ReactiveCommand<Unit> DeleteCommand { get; set; }
	}
}

