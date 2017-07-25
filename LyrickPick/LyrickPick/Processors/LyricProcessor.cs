using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
	public class LyricProcessor
	{
		private Random rand = new Random();
		//takes in json or string of one song's lyrics
		//returns array of strings of each line

		public static List<String> SpliceSong (string lyricsJson)
		{
			//array list of every line as a separate item
			var obj = JsonConvert.DeserializeObject(lyricsJson);
			JObject j = JObject.Parse(lyricsJson);
			string lyricsBody = (string)j.SelectToken("message.body.lyrics.lyrics_body");

			List<String> lines = lyricsBody.Split('\n').ToList();
			return lines;

		}
		//takes in song lyrics broken into array of strings of each line
		//returns list of lines and count of each
		public List<Tuple<string, int>> CountLines (List<String> SongLyrics)
		{
			List<Tuple<string, int>> count = new List<Tuple<string, int>>();
			return count;
		}

		public string selectLine (List<String> lines, List<String> selectedLines)
		{
			string line = lines[rand.Next(0, lines.Count)];
			while (selectedLines.Contains(line))
			{
				line = lines[rand.Next(0,lines.Count)];
			}
			return line;
		}
	}
}