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

namespace XamarinFormsReactiveListView
{
	public class MonkeyService : IMonkeyService
	{
		static object locker = new object ();
		SQLiteConnection database;

		public List<Monkey> GetAll ()
		{
			lock (locker) {
				return (from i in database.Table<Monkey>() select i).ToList();
			}
		}

		public int Remove(Monkey monkey)
		{
			lock (locker) {
				return database.Delete<Monkey>(monkey.Id);
			}
		}

		public int Add(Monkey monkey)
		{
			lock (locker) {
				return database.Insert(monkey);
			}
		}

		public MonkeyService ()
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();
			database.CreateTable<Monkey>();
		}

		public ObservableCollection<Monkey> Monkeys { get; protected set; }
	}
}

