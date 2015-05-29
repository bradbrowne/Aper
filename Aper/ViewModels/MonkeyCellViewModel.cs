using System;
using System.Reactive;
using System.Reactive.Linq;
using Aper.Models;
using ReactiveUI;
using Splat;

namespace Aper.ViewModels
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

		private Monkey monkey;
		public Monkey Monkey { 
			get { return monkey; } 
			set { this.RaiseAndSetIfChanged (ref monkey, value); }
		}

		public ReactiveCommand<Unit> DeleteCommand { get; set; }
	}
}

