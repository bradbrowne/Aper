using System;
using System.Collections.ObjectModel;
using XamarinFormsReactiveListView.ViewModels;
using XamarinFormsReactiveListView.Models;
using System.Collections.Generic;
using ReactiveUI;
using System.IO;
using SQLite.Net;
using Xamarin.Forms;
using XamarinFormsReactiveListView.Services;
using System.Linq;
using SQLite.Net.Async;
using System.Threading.Tasks;

namespace XamarinFormsReactiveListView
{
	public class MonkeyService : IMonkeyService
	{
		SQLiteAsyncConnection databaseAsync;

		public async Task<List<Monkey>> GetAll ()
		{
			return await databaseAsync.Table<Monkey> ().ToListAsync();
		}

		public async Task<int> Remove(Monkey monkey)
		{
			return await databaseAsync.DeleteAsync<Monkey>(monkey.Id);
		}

		public async Task<int> Add(Monkey monkey)
		{
			return await databaseAsync.InsertAsync(monkey);
		}

		public MonkeyService ()
		{
			databaseAsync = DependencyService.Get<ISQLite> ().GetConnectionAsync ();
			databaseAsync.CreateTableAsync<Monkey>();
		}

		public ObservableCollection<Monkey> Monkeys { get; protected set; }
	}
}

