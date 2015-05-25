using System;
using System.Collections.ObjectModel;
using XamarinFormsReactiveListView.ViewModels;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;

namespace XamarinFormsReactiveListView
{
	public interface IMonkeyService
	{
		int Remove(Monkey monkey);
		int Add(Monkey monkey);
		List<Monkey> GetAll();
	}
}

