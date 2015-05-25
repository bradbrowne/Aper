using System;
using XamarinFormsReactiveListView;
using Xamarin.Forms;
using XamarinFormsReactiveListView.iOS;
using System.IO;
using XamarinFormsReactiveListView.Services;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinIOS;

[assembly: Dependency (typeof (SQLite_iOS))]

namespace XamarinFormsReactiveListView.iOS
{
	public class SQLite_iOS : ISQLite
	{
		public SQLite_iOS ()
		{
		}

		private string GetPath()
		{
			var sqliteFilename = "Monkey.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			return path;
		}

		public SQLiteConnection GetConnection ()
		{
			return new SQLiteConnection(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), GetPath ());
		}

		public SQLiteAsyncConnection GetConnectionAsync()
		{
			var connectionFactory = new Func<SQLiteConnectionWithLock>(()=>new SQLiteConnectionWithLock(new SQLitePlatformIOS(), new SQLiteConnectionString(GetPath(), storeDateTimeAsTicks: false)));
			var asyncConnection = new SQLiteAsyncConnection(connectionFactory);
			return asyncConnection;
		}
	}
}