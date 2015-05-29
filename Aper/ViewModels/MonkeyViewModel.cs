using System;
using System.Reactive;
using System.Reactive.Linq;
using Aper.Models;
using Aper.Services;
using ReactiveUI;
using Splat;

namespace Aper.ViewModels
{
	public class MonkeyViewModel : ReactiveObject, IRoutableViewModel
	{
		public string UrlPathSegment {
			get { return "Monkey"; }
		}
		public IScreen HostScreen { get; protected set; }

		IMonkeyService _monkeyService;

		public MonkeyViewModel (Monkey monkey, IScreen hostScreen = null)
		{
			Monkey = monkey;
			_monkeyService = Locator.Current.GetService<IMonkeyService>();
			
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

			Update = ReactiveCommand.CreateAsyncTask(async (model, e) =>
				{
					this.Log().Debug("Update");
					await _monkeyService.UpdateAsync(Monkey);
					await HostScreen.Router.NavigateBack.ExecuteAsync(null);
				});
			Update.ThrownExceptions
				.SelectMany(ex => UserError.Throw("Error Deleting Monkey", ex))
				.Subscribe(result => this.Log().Debug("{0}", result));
		}

		public Monkey Monkey { get; set; }

		public ReactiveCommand<Unit> Update { get; set; }
	}
}

