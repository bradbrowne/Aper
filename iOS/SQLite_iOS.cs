using System;
using System.IO;
using Aper.iOS;
using Aper.Services;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly: Dependency (typeof (SQLite_iOS))]

namespace Aper.iOS
{
	public class SQLite_iOS : ISQLite
	{
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
			return new SQLiteConnection(new SQLitePlatformIOS(), GetPath ());
		}

		public SQLiteAsyncConnection GetConnectionAsync()
		{
			var connectionFactory = new Func<SQLiteConnectionWithLock>(()=>new SQLiteConnectionWithLock(new SQLitePlatformIOS(), new SQLiteConnectionString(GetPath(), false)));
			var asyncConnection = new SQLiteAsyncConnection(connectionFactory);
			return asyncConnection;
		}
	}
}