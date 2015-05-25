using System;
using System.Collections.ObjectModel;
using XamarinFormsReactiveListView.ViewModels;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;

namespace XamarinFormsReactiveListView
{
	public interface IMonkeyService
	{
		void Remove(Monkey monkey);
		ObservableCollection<Monkey> GetAll();
	}
}

