using SQLite.Net;
using SQLite.Net.Async;

namespace Aper.Services
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
		SQLiteAsyncConnection GetConnectionAsync();
	}
}