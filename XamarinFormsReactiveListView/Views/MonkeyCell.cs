using System;
using Xamarin.Forms;
using ReactiveUI;
using System.Reactive;
using System.Diagnostics;
using XamarinFormsReactiveListView.Models;

namespace Beeteem.Views
{
	public class MonkeyCell : TextCell
	{
		public MonkeyCell ()
		{
			var moreAction = new MenuItem { Text = "More" };
			moreAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			moreAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
			};

			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
			deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			deleteAction.SetBinding (MenuItem.CommandProperty, new Binding ("DeleteCommand"));

			ContextActions.Add (moreAction);
			ContextActions.Add (deleteAction);
		}
	}
}

