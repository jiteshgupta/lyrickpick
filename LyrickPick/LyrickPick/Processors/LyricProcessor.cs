using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
			JObject j = JObject.Parse(lyricsJson);
			string lyricsBody = (string)j.SelectToken("message.body.lyrics.lyrics_body");

			string[] lines = lyricsBody.Split('\n').ToArray();

			//remove lines which do not contain any alphabetic character
			string[] outputLines = ProcessLines(lines);

			return outputLines;
		}

		public string[] ProcessLines(string[] lines)
		{
			lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
			lines = lines.Where(x => Regex.IsMatch(x, "[a-zA-Z]")).ToArray();

			//MusicXmatch appends a line in the end, "*** These Lyrics are not for Commercial Use", remove that
			lines = lines.Where(x => !x.StartsWith("**")).ToArray();
			return lines;
		}

		//takes in song lyrics broken into array of strings of each line
		//returns list of lines and count of each
		public List<Tuple<string, int>> CountLines (List<string> SongLyrics)
		{
			List<Tuple<string, int>> count = new List<Tuple<string, int>>();
			return count;
		}

		public string selectLine (string[] lines, List<string> selectedLines)
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