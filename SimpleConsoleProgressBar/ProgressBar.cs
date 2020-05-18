using System;
using System.Text;

namespace SimpleConsoleProgressBar
{
	public class ProgressBar
	{
		private readonly decimal _minimum;
		private readonly decimal _maximum = 100;
		private readonly int _length = 80;
		private readonly string _completedSymbol;
		private readonly string _inCompletedSymbol;
		private readonly string _format;
		private readonly ConsoleColor? _foregroundColor;
		private readonly ConsoleColor? _backgroundColor;
		private Position? _positionToDraw;

		public ProgressBar(
			decimal minimum = 0,
			decimal maximum = 100,
			int length = 80,
			string completedSymbol = "█",
			string incompletedSymbol = "░",
			string format = "[{0}] {1}",
			ConsoleColor? foregroundColor = null,
			ConsoleColor? backgroundColor = null,
			Position? positionToDraw = null)
		{
			VerifyMinimumMaximum(minimum, maximum);
			VerifyLengthValid(length);

			_minimum = minimum;
			_maximum = maximum;
			_completedSymbol = completedSymbol;
			_inCompletedSymbol = incompletedSymbol;
			_length = length;
			_positionToDraw = positionToDraw;
			_format = format;
			_foregroundColor = foregroundColor;
			_backgroundColor = backgroundColor;
		}

		public void Update(decimal value)
		{
			VerifyValueInRange(value, _minimum, _maximum);

			using (new CursorPosition(PositionToDraw()))
			using (new ConsoleColors(_foregroundColor, _backgroundColor))
				Console.WriteLine(_format, Build(value, _minimum, _maximum, _length, _completedSymbol, _inCompletedSymbol), value);

			Position PositionToDraw()
			{
				if (_positionToDraw is null)
					_positionToDraw = new Position(Console.CursorLeft, Console.CursorTop);

				return _positionToDraw.Value;
			}
		}

		public static string Build(decimal value, decimal minimum = 0, decimal maximum = 100, int length = 80, string completedSymbol = "█", string incompletedSymbol = "░")
		{
			VerifyMinimumMaximum(minimum, maximum);
			VerifyValueInRange(value, minimum, maximum);
			VerifyLengthValid(length);

			if (maximum == minimum)
				return string.Empty;

			var progress = (int)((value - minimum) * length / (maximum - minimum));

			var sb = new StringBuilder(length);

			for (var i = 0; i < progress; i++)
				sb.Append(completedSymbol);
			for (var i = progress; i < length; i++)
				sb.Append(incompletedSymbol);

			return sb.ToString();
		}

		private static void VerifyLengthValid(int length)
		{
			if (length < 0)
				throw new ArgumentOutOfRangeException(nameof(length), "Length should not be negative.");
		}

		private static void VerifyValueInRange(decimal value, decimal minimum, decimal maximum)
		{
			if (value > maximum || value < minimum)
				throw new ArgumentOutOfRangeException(nameof(value), "Value should be between minimum and maximum.");
		}

		private static void VerifyMinimumMaximum(decimal minimum, decimal maximum)
		{
			if (minimum > maximum)
				throw new ArgumentOutOfRangeException(nameof(minimum), "Minimum value should be smaller than maximum.");
		}

		private readonly struct CursorPosition : IDisposable
		{
			private readonly Position _position;

			public CursorPosition(Position position)
			{
				_position = new Position(Console.CursorLeft, Console.CursorTop);

				Console.CursorLeft = position.Left;
				Console.CursorTop = position.Top;
			}

			public void Dispose()
			{
				Console.CursorLeft = _position.Left;
				Console.CursorTop = _position.Top;
			}
		}

		private readonly struct ConsoleColors : IDisposable
		{
			private readonly ConsoleColor _foregroundColor;
			private readonly ConsoleColor _backgroundColor;

			public ConsoleColors(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
			{
				_foregroundColor = Console.ForegroundColor;
				_backgroundColor = Console.BackgroundColor;

				if (foregroundColor != null)
					Console.ForegroundColor = foregroundColor.Value;
				if (backgroundColor != null)
					Console.BackgroundColor = backgroundColor.Value;
			}

			public void Dispose()
			{
				Console.ForegroundColor = _foregroundColor;
				Console.BackgroundColor = _backgroundColor;
			}
		}

		public readonly struct Position
		{
			public readonly int Left;
			public readonly int Top;

			public Position(int left, int top)
			{
				Left = left;
				Top = top;
			}
		}
	}
}
