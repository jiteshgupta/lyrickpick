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
				string answer = Console.ReadLine();

				if (answer.Equals(qz.GetCurrentSongTitle()))
				{
					Console.WriteLine("Correct!");
				}
				else
				{
					Console.WriteLine("Wrong!");
					Console.WriteLine("Song:- " + qz.GetCurrentSongTitle());
					Console.WriteLine("Artist:- " + qz.GetCurrentSongArtist());
				}
			}
		}

		public static void start()
		{
			Console.WriteLine("Welcome to the Lyric Pick game. Here we go...");
		}
	}
}
