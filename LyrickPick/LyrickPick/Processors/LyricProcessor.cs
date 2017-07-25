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

		public string[] SpliceSong (string lyricsJson)
		{
			//array list of every line as a separate item
			var obj = JsonConvert.DeserializeObject(lyricsJson);
			JObject j = JObject.Parse(lyricsJson);
			string lyricsBody = (string)j.SelectToken("message.body.lyrics.lyrics_body");

			string[] lines = lyricsBody.Split('\n').ToArray();
			return lines;

		}
		//takes in song lyrics broken into array of strings of each line
		//returns list of lines and count of each
		public List<Tuple<string, int>> CountLines (List<String> SongLyrics)
		{
			List<Tuple<string, int>> count = new List<Tuple<string, int>>();
			return count;
		}

		public string selectLine (string[] lines, List<String> selectedLines)
		{
			int randomLineNumber = rand.Next(0, lines.Length);

			string line;
			if (randomLineNumber == lines.Length - 1)
			{
				line = lines[randomLineNumber-1] + "\n" + lines[randomLineNumber];
			}
			else
			{
				line = lines[randomLineNumber] + "\n" + lines[randomLineNumber + 1];
			}
			
			while (selectedLines.Contains(line))
			{
				randomLineNumber = rand.Next(0, lines.Length);
				line = lines[randomLineNumber];
			}

			return line;
		}
	}
}