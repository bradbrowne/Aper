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
		Task<int> DeleteAsync(Monkey monkey);
		Task<int> InsertAsync(Monkey monkey);
		Task<int> UpdateAsync(Monkey monkey);
		Task<List<Monkey>> GetAllAsync();
	}
}

