using System;
using System.Collections.ObjectModel;
using Aper.ViewModels;
using Aper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

