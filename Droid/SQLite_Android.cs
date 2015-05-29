using System;
using System.IO;
using Aper.Droid;
using Aper.Services;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency (typeof (SQLite_Android))]

namespace Aper.Droid
{
	public class SQLite_Android : ISQLite
	{
	    public string GetPath ()
		{
			var sqliteFilename = "Monkey.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			return Path.Combine(documentsPath, sqliteFilename);
		}

		public SQLiteConnection GetConnection ()
		{
			return new SQLiteConnection(new SQLitePlatformAndroid(), GetPath ());
		}

		public SQLiteAsyncConnection GetConnectionAsync()
		{
			var connectionFactory = new Func<SQLiteConnectionWithLock>(()=>new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(GetPath(), false)));
			var asyncConnection = new SQLiteAsyncConnection(connectionFactory);
			return asyncConnection;
		}
	}
}