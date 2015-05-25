using System;
using System.Collections.ObjectModel;
using XamarinFormsReactiveListView.ViewModels;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinFormsReactiveListView
{
	public interface IMonkeyService
	{
		Task<int> Remove(Monkey monkey);
		Task<int> Add(Monkey monkey);
		Task<List<Monkey>> GetAll();
	}
}

