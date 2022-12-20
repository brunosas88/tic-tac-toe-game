using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
	class Board
	{
		private char[,] board;

		public Board()
		{
			board = new char[,]
			{
				{'1', '|', '2', '|', '3'},
				{'4', '|', '5', '|', '6'},
				{'7', '|', '8', '|', '9'}
			};
		}

		public char[,] GetBoard ()
		{
			return board;
		}

		public void PrintBoard()
		{
			Console.WriteLine("----------");
			for (int row = 0; row < board.GetLength(0); row++)
			{
				for (int column = 0; column < board.GetLength(1); column++)
				{
					Console.Write(board[row, column] + " ");
				}
				Console.WriteLine();
				Console.WriteLine("----------");
			}
		}

		public void UpdateBoard(int position, int player)
		{

			char character = (player == 1) ? 'X' : 'O';

			switch (position)
			{
				case 1:
					board[0, 0] = character;
					break;
				case 2:
					board[0, 2] = character;
					break;
				case 3:
					board[0, 4] = character;
					break;
				case 4:
					board[1, 0] = character;
					break;
				case 5:
					board[1, 2] = character;
					break;
				case 6:
					board[1, 4] = character;
					break;
				case 7:
					board[2, 0] = character;
					break;
				case 8:
					board[2, 2] = character;
					break;
				case 9:
					board[2, 4] = character;
					break;
			}
		}
	}
}
