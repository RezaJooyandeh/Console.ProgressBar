using System;
using System.Threading.Tasks;
using ProgtextBar;

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
					progressBar.Update(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom Length");
				var progressBar = new ProgressBar(length: 60);
				for (var i = 0; i <= 60; i++)
				{
					await Task.Delay(10);
					progressBar.Update(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom range");
				var progressBar = new ProgressBar(minimum: 1, maximum: 20);
				for (var i = 1; i <= 18; i += 1)
				{
					await Task.Delay(20);
					progressBar.Update(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Decimal range");
				var progressBar = new ProgressBar(minimum: 0, maximum: 1m);
				for (var i = 0m; i <= 0.9m; i += 0.1m)
				{
					await Task.Delay(20);
					progressBar.Update(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom symbols");
				var progressBar = new ProgressBar(completedSymbol: "‡", incompletedSymbol: " ");
				for (var i = 0; i <= 40; i++)
				{
					await Task.Delay(10);
					progressBar.Update(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom Format");
				var progressBar = new ProgressBar(format: "|{0}| {1}% completed.");
				for (var i = 0; i <= 89; i++)
				{
					await Task.Delay(10);
					progressBar.Update(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Custom Color");
				var progressBar = new ProgressBar(format: "{0}", foregroundColor: ConsoleColor.Green, backgroundColor: ConsoleColor.Blue);
				for (var i = 0; i <= 70; i++)
				{
					await Task.Delay(10);
					progressBar.Update(i);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("Super custom");
				ConsoleColor foreground = Console.ForegroundColor;
				ConsoleColor background = Console.BackgroundColor;

				for (var i = 0; i <= 70; i++)
				{
					await Task.Delay(10);

					Console.CursorLeft = 10;
					Console.CursorTop = 15;
					Console.Write("[[[[");
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.BackgroundColor = ConsoleColor.DarkMagenta;
					Console.Write(ProgressBar.Build(
						value: i,
						minimum: 0,
						maximum: 100,
						length: 50,
						completedSymbol: "‡",
						incompletedSymbol: "."));
					Console.ForegroundColor = foreground;
					Console.BackgroundColor = background;
					Console.Write("]]] {0}%", i);
				}
			}

			Console.ReadKey();
		}
	}
}
