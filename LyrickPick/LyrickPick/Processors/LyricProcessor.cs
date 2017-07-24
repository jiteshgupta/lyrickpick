using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
    public class LyricProcessor
    {
        //takes in json or string of one song's lyrics
        //returns as list of each line
        public static string[] SpliceSong (string SongLyrics)
        {
            //array list of every line as a separate item
            string[] lines = new string[SongLyrics.Length];
            return lines;

        }
        //takes in list of each line
        public static List<Tuple<string, int>> CountLines (string[] SongLyrics)
        {
            List<Tuple<string, int>> count = new List<Tuple<string, int>>();
            return count;
        }

        public static string selectLine (Array LyricList, Array selectedIndices)
        {
            return "this is the line";
        }
    }
}