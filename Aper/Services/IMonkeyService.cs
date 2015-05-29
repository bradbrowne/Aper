using System.Collections.Generic;
using System.Threading.Tasks;
using Aper.Models;

namespace Aper.Services
{
	public interface IMonkeyService
	{
		Task<int> DeleteAsync(Monkey monkey);
		Task<int> InsertAsync(Monkey monkey);
		Task<int> UpdateAsync(Monkey monkey);
		Task<List<Monkey>> GetAllAsync();
	}
}

