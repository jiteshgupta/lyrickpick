using System;

namespace LyrickPick.Processors
{
	public class FetchSongs
	{
		private static string musixmatchAPIkey = "8b7654870c8395335a30eb19039218f6";
		private string songsData = String.Empty;

		public string getSongsData()
		{
			return songsData;
		}

		public FetchSongs()
		{
			GetSongsMusicXmatch();
		}

		public void GetSongsMusicXmatch()
		{
			string url = "http://api.musixmatch.com/ws/1.1/chart.tracks.get?page=1&page_size=50&country=us&f_has_lyrics=1&apikey=" + musixmatchAPIkey;
			songsData = FetchData.FetchDataFromURL(url);
		}
        public string GetSongsMusicXmatch(int pageNum)
        {
            string url = "http://api.musixmatch.com/ws/1.1/chart.tracks.get?page=" + pageNum + "&page_size=50&country=us&f_has_lyrics=1&apikey=" + musixmatchAPIkey;
            songsData = FetchData.FetchDataFromURL(url);
            return songsData;
        }
    }
}