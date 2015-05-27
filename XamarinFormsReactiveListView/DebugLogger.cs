using System;
using Splat;
using System.Diagnostics;

namespace XamarinFormsReactiveListView
{
	public class DebugLogger : ILogger
	{
		public void Write(string message, LogLevel logLevel)
		{
			if ((int)logLevel < (int)Level)
			{
				return;
			}

			Debug.WriteLine(message);
		}

		public LogLevel Level { get; set; }
	}
}

