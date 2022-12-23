using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
	class Player
	{
		private string nome;
		private int playOrder;
		private int points;
		private int victories;
		private int draws;
		private int defeats;

		public Player() { }

		public Player(string nome)
		{
			this.nome = nome;
			playOrder = 0;
			points = 0;
			victories = 0;
			draws = 0;
			defeats = 0;
		}

		public string Nome { get => nome; set => nome = value; }
		public int PlayOrder { get => playOrder; set => playOrder = value; }
		public int Points { get => points; set => points = value; }
		public int Victories { get => victories; set => victories = value; }
		public int Draws { get => draws; set => draws = value; }
		public int Defeats { get => defeats; set => defeats = value; }
	}
}
