using LyrickPick.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyrickPickFunctionality
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Program.start();
			Quiz qz = new Quiz();
			while (Console.ReadLine().Equals("Next"))
			{
				string question = qz.Question();
				Console.WriteLine(question);
				string userInput = Console.ReadLine();

				if (String.Equals("hint", userInput, StringComparison.OrdinalIgnoreCase))
				{
					string hint = qz.processHint();
					Console.WriteLine(hint);
					userInput = Console.ReadLine();
				}

				if (String.Equals(qz.GetCurrentContext().GetCurrentSongTitle(), userInput, StringComparison.OrdinalIgnoreCase))
				{
					Console.WriteLine("Correct!");
				}
				else
				{
					Console.WriteLine("Wrong!");
					Console.WriteLine("Song:- " + qz.GetCurrentContext().GetCurrentSongTitle());
					Console.WriteLine("Artist:- " + qz.GetCurrentContext().GetCurrentSongArtist());
				}
			}
		}

		public static void start()
		{
			Console.WriteLine("Welcome to the Lyric Pick game. Here we go...");
		}
	}
}
