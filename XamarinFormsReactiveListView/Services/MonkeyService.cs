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
		private static readonly AsyncLock Mutex = new AsyncLock ();
		SQLiteAsyncConnection databaseAsync;

		public async Task CreateDatabaseAsync ()
		{
			using (await Mutex.LockAsync ().ConfigureAwait (false)) {
				await databaseAsync.CreateTableAsync<Monkey> ().ConfigureAwait (false);
			}
		}

		public async Task<List<Monkey>> GetAllAsync ()
		{
			List<Monkey> monkeys;
			using (await Mutex.LockAsync ().ConfigureAwait (false)) {
				monkeys = await databaseAsync.Table<Monkey> ().ToListAsync ().ConfigureAwait (false);
			}
			return monkeys;
		}

		public async Task<int> DeleteAsync(Monkey monkey)
		{
			int result;
			using (await Mutex.LockAsync ().ConfigureAwait (false)) {
				result = await databaseAsync.DeleteAsync<Monkey>(monkey.Id).ConfigureAwait (false);
			}
			return result;
		}

		public async Task<int> InsertAsync(Monkey monkey)
		{
			int result;
			using (await Mutex.LockAsync ().ConfigureAwait (false)) {
				result = await databaseAsync.InsertAsync(monkey).ConfigureAwait (false);
			}
			return result;
		}

		public async Task<int> UpdateAsync(Monkey monkey)
		{
			int result;
			using (await Mutex.LockAsync ().ConfigureAwait (false)) {
				result = await databaseAsync.UpdateAsync(monkey).ConfigureAwait(false);
			}
			return result;
		}

		public MonkeyService ()
		{
			databaseAsync = DependencyService.Get<ISQLite> ().GetConnectionAsync ();
			databaseAsync.CreateTableAsync<Monkey>();
		}
	}
}

