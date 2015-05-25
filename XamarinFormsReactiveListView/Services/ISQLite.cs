using System;
using SQLite;
using SQLite.Net;

namespace XamarinFormsReactiveListView.Services
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}