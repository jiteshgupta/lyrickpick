using System;

namespace LyrickPick.Processors
{
	public class FetchSongs
	{
		private static string lastFMApiKey = "4bd79a8ccabeea1dc0b33f9de91f481b";
		private static string musicxmatchAPIkey = "8b7654870c8395335a30eb19039218f6";
		private static string dataFormat = "json";
		private string songsData = String.Empty;

		public string getSongsData()
		{
			return songsData;
		}

		public FetchSongs()
		{
			GetSongsMusicXmatch();
		}

		public void GetSongsLastFM()
		{
			string url = "http://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key=" + lastFMApiKey + "&format=" + dataFormat;
			songsData = FetchData.FetchDataFromURL(url);
		}

		public void GetSongsMusicXmatch()
		{
			string url = "http://api.musixmatch.com/ws/1.1/chart.tracks.get?page=1&page_size=50&country=us&f_has_lyrics=1&apikey=" + musicxmatchAPIkey;
			songsData = FetchData.FetchDataFromURL(url);
		}
	}
}