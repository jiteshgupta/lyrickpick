using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
	public class Song
	{
		private string artist;
		private string title;
        private int musixmatchArtistID;
		private int musixmatchID;

		public Song()
		{
		}

		public Song(string artistName, string songTitle)
		{
			artist = artistName;
			title = songTitle;
		}

		public Song(string artistName, string songTitle, int trackID)
		{
			artist = artistName;
			title = songTitle;
			musixmatchID = trackID;
		}

        public Song(string artistName, string songTitle, int trackID, int artistID)
        {
            artist = artistName;
            title = songTitle;
            musixmatchID = trackID;
            musixmatchArtistID = artistID;
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

        public int getMMIDArtist()
        {
            return musixmatchArtistID;
        }

        public void setMMID(int trackID)
		{
			musixmatchID = trackID;
		}
	}
}