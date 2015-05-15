using System;
using ReactiveUI;
using XamarinFormsReactiveListView.Models;
using System.Reactive;

namespace XamarinFormsReactiveListView
{
	public class MonkeyViewCellViewModel : ReactiveObject
	{
		public MonkeyViewCellViewModel (IScreen hostScreen = null)
		{
			
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

