using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
    public class Song
    {
        static string artist;
        static string title;
        static int musixmatchID;
        public Song(string artistName, string songTitle)
        {
            artist = artistName;
            title = songTitle;
        }
        public string getArtist()
        {
            return artist;
        }
        public string getTitle()
        {
            return title;
        }
        public int getMMID()
        {
            return musixmatchID;
        }
        public void setMMID(int trackID)
        {
            musixmatchID = trackID;
        }
    }
}