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
        static Random rand = new Random();
        //takes in json or string of one song's lyrics
        //returns array of strings of each line
        public static List<String> SpliceSong (string LyricsJson)
        {
            //array list of every line as a separate item
            var obj = JsonConvert.DeserializeObject(LyricsJson);
            JObject j = JObject.Parse(LyricsJson);
            //string lyricsBody = (string)obj.["message"]["body"]["lyrics"]["lyrics_body"];
            //string lyricsBody = obj.message.body.lyrics.lyrics_body;
            //string lyricsBody = (string)j.SelectToken("lyrics_body");
            string lyricsBody = (string)j.SelectToken("message.body.lyrics.lyrics_body");

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
            string line = lines[rand.Next(0, lines.Count)];
            while (selectedLines.Contains(line))
            {
                line = lines[rand.Next(0,lines.Count)];
            }
            return line;
        }
    }
}