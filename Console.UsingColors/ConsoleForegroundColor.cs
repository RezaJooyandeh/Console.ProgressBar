using System;

namespace UsingConsoleColors
{
	public readonly struct ConsoleForegroundColor : IDisposable
	{
		private readonly ConsoleColor _previousForegroundColor;

		public ConsoleForegroundColor(ConsoleColor? foregroundColor)
		{
			_previousForegroundColor = Console.ForegroundColor;

			if (foregroundColor.HasValue)
				Console.ForegroundColor = foregroundColor.Value;
		}

		public void Dispose()
			=> Console.ForegroundColor = _previousForegroundColor;
	}
}
