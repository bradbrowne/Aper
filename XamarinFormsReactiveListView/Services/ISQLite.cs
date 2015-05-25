using System;
using SQLite;
using SQLite.Net;
using SQLite.Net.Async;

namespace XamarinFormsReactiveListView.Services
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
		SQLiteAsyncConnection GetConnectionAsync();
	}
}