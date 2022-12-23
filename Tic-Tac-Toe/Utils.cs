using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
	class Utils
	{

		public static void WriteJSON(List<Player> players, List<Match> matches)
		{
			string filePath = @"C:\Users\Public\Documents\players.json";
			
			string jsonStringPlayers = JsonSerializer.Serialize(players, new JsonSerializerOptions() { WriteIndented = true });
			
			using (StreamWriter outputFile = new StreamWriter(filePath))			
				outputFile.WriteLine(jsonStringPlayers);

			filePath = @"C:\Users\Public\Documents\matches.json";
			
			string jsonStringMatches = JsonSerializer.Serialize(matches, new JsonSerializerOptions() { WriteIndented = true });

			using (StreamWriter outputFile = new StreamWriter(filePath))
				outputFile.WriteLine(jsonStringMatches);
		}

		public static void ReadJSON(ref List<Player> players, ref List<Match> matches)
		{
			string filePath = @"C:\Users\Public\Documents\players.json";		

			if (File.Exists(filePath))
			{
				using (StreamReader inputFile = new StreamReader(filePath))
				{
					string json = inputFile.ReadToEnd();
					players = JsonSerializer.Deserialize<List<Player>>(json);
				}
			}

			filePath = @"C:\Users\Public\Documents\matches.json";

			if (File.Exists(filePath))
			{
				using (StreamReader inputFile = new StreamReader(filePath))
				{
					string json = inputFile.ReadToEnd();
					matches = JsonSerializer.Deserialize<List<Match>>(json);
				}
			}
		}


	}
}
