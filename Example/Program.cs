using System;
using System.Threading.Tasks;
using ConsoleProgressbar;
using UsingConsoleColors;

namespace Example
{
	internal class Program
	{
		private static async Task Main()
		{
			{
				Console.WriteLine("Default");
				var progressBar = new ProgressBar();
				for (var i = 0; i <= 60; i++)
				{
					await Task.Delay(20);
					progressBar.Draw(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom Length");
				var progressBar = new ProgressBar(length: 60);
				for (var i = 0; i <= 60; i++)
				{
					await Task.Delay(10);
					progressBar.Draw(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom range");
				var progressBar = new ProgressBar(minimum: 1, maximum: 20);
				for (var i = 1; i <= 18; i += 1)
				{
					await Task.Delay(20);
					progressBar.Draw(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Decimal range");
				var progressBar = new ProgressBar(minimum: 0, maximum: 1m);
				for (var i = 0m; i <= 0.9m; i += 0.1m)
				{
					await Task.Delay(20);
					progressBar.Draw(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom symbols");
				var progressBar = new ProgressBar(completedSymbol: "‡", incompletedSymbol: " ");
				for (var i = 0; i <= 40; i++)
				{
					await Task.Delay(10);
					progressBar.Draw(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom Format");
				var progressBar = new ProgressBar(formatter: (progressBar, value, valuePercentage, minimum, maximum) => $"|{progressBar}| {valuePercentage}% completed");
				for (var i = 0; i <= 89; i++)
				{
					await Task.Delay(10);
					progressBar.Draw(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom Color");
				var progressBar = new ProgressBar(foregroundColor: ConsoleColor.Green, backgroundColor: ConsoleColor.Blue);
				for (var i = 0; i <= 70; i++)
				{
					await Task.Delay(10);
					progressBar.Draw(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Super custom");

				for (var i = 0; i <= 70; i++)
				{
					await Task.Delay(10);

					Console.CursorLeft = 10;
					Console.CursorTop = 15;
					using (new ConsoleForegroundColor(ConsoleColor.Yellow))
					using (new ConsoleBackgroundColor(ConsoleColor.DarkMagenta))
					{
						Console.Write("[[[[");
						Console.Write(ProgressBar.Build(
							value: i,
							minimum: 0,
							maximum: 100,
							length: 50,
							completedSymbol: "‡",
							incompletedSymbol: "."));
						Console.Write("]]] {0}%", i);
					}
				}
			}

			Console.ReadKey();
		}
	}
}
