using System;
using Xamarin.Forms;
using System.IO;
using XamarinFormsReactiveListView.Services;
using SQLite.Net.Async;
using XamarinFormsReactiveListView.Droid;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

[assembly: Dependency (typeof (SQLite_Android))]

namespace XamarinFormsReactiveListView.Droid
{
	public class SQLite_Android : ISQLite
	{
		public SQLite_Android ()
		{
		}

		public string GetPath ()
		{
			var sqliteFilename = "Monkey.db3";
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			return Path.Combine(documentsPath, sqliteFilename);
		}

		public SQLiteConnection GetConnection ()
		{
			return new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), GetPath ());
		}

		public SQLiteAsyncConnection GetConnectionAsync()
		{
			var connectionFactory = new Func<SQLiteConnectionWithLock>(()=>new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(GetPath(), storeDateTimeAsTicks: false)));
			var asyncConnection = new SQLiteAsyncConnection(connectionFactory);
			return asyncConnection;
		}
	}
}