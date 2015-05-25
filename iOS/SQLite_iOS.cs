using System;
using XamarinFormsReactiveListView;
using Xamarin.Forms;
using XamarinFormsReactiveListView.iOS;
using System.IO;
using XamarinFormsReactiveListView.Services;
using SQLite.Net;

[assembly: Dependency (typeof (SQLite_iOS))]

namespace XamarinFormsReactiveListView.iOS
{
	public class SQLite_iOS : ISQLite
	{
		public SQLite_iOS ()
		{
		}

		#region ISQLite implementation
		public SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "Monkey.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);

			// This is where we copy in the prepopulated database
			Console.WriteLine (path);
//			if (!File.Exists (path)) {
//				File.Copy (sqliteFilename, path);
//			}

			var conn = new SQLiteConnection(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path);

			// Return the database connection 
			return conn;
		}
		#endregion
	}
}