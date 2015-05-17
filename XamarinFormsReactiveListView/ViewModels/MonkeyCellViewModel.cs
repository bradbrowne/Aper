using System;
using ReactiveUI;
using XamarinFormsReactiveListView.Models;
using System.Reactive;
using Splat;

namespace XamarinFormsReactiveListView.ViewModels
{
	public class MonkeyCellViewModel : ReactiveObject
	{
		public MonkeyCellViewModel ()
		{
			DeleteCommand = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					System.Diagnostics.Debug.WriteLine("DeleteCommand");
					var monkey = model as MonkeyCellViewModel;
					//Monkeys.Remove(monkey);
				});
		}

		public override string ToString ()
		{
			return string.Format ("MonkeyCellViewModel");
		}

		public Monkey Monkey { get; set; }
		public string Name 
		{ 
			get 
			{ 
				return Monkey.Name;
			}
		}

		public ReactiveCommand<Unit> DeleteCommand { get; set; }
	}
}

