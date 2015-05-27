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
		public MonkeyCellViewModel (ReactiveCommand<Unit> RemoveMonkey)
		{
			DeleteCommand = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					var monkey = model;
					await RemoveMonkey.ExecuteAsyncTask(monkey, e);
				});
			DeleteCommand.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Deleting Monkey", ex))
				.Subscribe(result => this.Log().Debug("{0}", result));
		}

		public Monkey Monkey { get; set; }

		public ReactiveCommand<Unit> DeleteCommand { get; set; }
	}
}

