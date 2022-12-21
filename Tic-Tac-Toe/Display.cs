using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
	class Display
	{
		public static void ShowWarning(string message)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.Write(message + "\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine();
		}

		public static void BackToMenu()
		{
			Console.WriteLine();
			Console.WriteLine("Pressione Enter para voltar ao menu anterior.");
			Console.ReadLine();
		}

		public static string AlignMessage(string message, int blankSpace)
		{
			return String.Format($"{{0,-{blankSpace}}}", String.Format("{0," + ((blankSpace + message.Length) / 2).ToString() + "}", message));
		}

		public static void GameInterface(string message)
		{
			Console.Clear();
			int blankSpace = 52;
			int totalCharsHeader = 72;
			message = AlignMessage(message, blankSpace);
			string title = AlignMessage("Tic-Tac-Toe", blankSpace);
			Console.BackgroundColor = ConsoleColor.Green;

			Console.WriteLine(new string('=', totalCharsHeader));
			Console.Write("=========|");
			Console.Write(title);
			Console.Write("|=========\n");
			Console.WriteLine(new string('=', totalCharsHeader));

			Console.BackgroundColor = ConsoleColor.Magenta;

			Console.WriteLine(new string('-', totalCharsHeader));
			Console.Write("---------|");
			Console.Write(message);
			Console.Write("|---------\n");
			Console.WriteLine(new string('-', totalCharsHeader));
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine();
		}

		public static void ShowMenu()
		{
			Console.WriteLine("1 - Cadastrar Novo Jogador");
			Console.WriteLine("2 - Histórico do Jogo");
			Console.WriteLine("3 - Jogar!");
			Console.WriteLine("0 - Sair do Jogo");
			Console.WriteLine();
		}

		public static void ShowLogMenu()
		{
			Console.WriteLine("1 - Ver Histórico de Jogadores");
			Console.WriteLine("2 - Ver Histórico de Partidas");
			Console.WriteLine("0 - Voltar ao Menu Principal");
			Console.WriteLine();
		}
	}
}
