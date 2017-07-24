using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
    public class LyricProcessor
    {
        //takes in json or string of one song's lyrics
        //returns array of strings of each line
        public static List<String> SpliceSong (string LyricsJson)
        {
            //array list of every line as a separate item
            var obj = JObject.Parse(LyricsJson);
            var lyricsBody = (string)obj["body"]["lyrics"]["lyrics_body"];
            List<String> lines = lyricsBody.Split('\n').ToList();
            return lines;

        }
        //takes in song lyrics broken into array of strings of each line
        //returns list of lines and count of each
        public static List<Tuple<string, int>> CountLines (List<String> SongLyrics)
        {
            List<Tuple<string, int>> count = new List<Tuple<string, int>>();
            return count;
        }

        public static string selectLine (List<String> lines, List<String> selectedLines)
        {
            string line = selectedLines[0];
            while (selectedLines.Contains(line))
            {
                line = lines[new Random().Next(0,lines.Count)];
            }
            return line;
        }
    }
}