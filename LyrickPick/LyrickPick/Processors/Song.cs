using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
    public class Song
    {
        static string artist;
        static string song;
        static string year; 
        public Song(string artistName, string songTitle)
        {
            artist = artistName;
            song = songTitle;
        }
    }
}