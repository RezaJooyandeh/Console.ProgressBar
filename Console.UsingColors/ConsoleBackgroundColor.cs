using System;

namespace UsingConsoleColors
{
	public readonly struct ConsoleBackgroundColor : IDisposable
	{
		private readonly ConsoleColor _prevoiusBackgroundColor;

		public ConsoleBackgroundColor(ConsoleColor? foregroundColor)
		{
			_prevoiusBackgroundColor = Console.ForegroundColor;

			if (foregroundColor.HasValue)
				Console.ForegroundColor = foregroundColor.Value;
		}

		public void Dispose()
			=> Console.ForegroundColor = _prevoiusBackgroundColor;
	}
}
