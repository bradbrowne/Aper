using System;
using ReactiveUI;
using XamarinFormsReactiveListView.Models;
using System.Reactive;

namespace XamarinFormsReactiveListView
{
	public class MonkeyCellViewModel : ReactiveObject
	{
		public MonkeyCellViewModel (IScreen hostScreen = null)
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

