using System;
using ReactiveUI;
using System.Reactive;
using SQLite.Net.Attributes;

namespace Aper.Models
{
	public class Monkey
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}

