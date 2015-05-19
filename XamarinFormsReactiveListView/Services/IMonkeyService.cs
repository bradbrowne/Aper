using System;
using System.Collections.ObjectModel;
using XamarinFormsReactiveListView.ViewModels;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;
using ReactiveUI;

namespace XamarinFormsReactiveListView
{
	public interface IMonkeyService
	{
		void Remove(MonkeyCellViewModel monkey);
		ReactiveList<MonkeyCellViewModel> GetAll();
	}
}

