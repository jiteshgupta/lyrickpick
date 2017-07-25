using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Request library
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;


namespace LyrickPick.Processors
{
	public class FetchLyrics
	{
		private List<string> lyricsList;
		private static string dataFormat = "json";
		private string lyricsData = String.Empty;
		private static string musixmatchAPIkey = "8b7654870c8395335a30eb19039218f6";

		public string getlyricsData()
		{
			return lyricsData;
		}

		public FetchLyrics()
		{
			lyricsList = new List<String>();
		}

		public string GetLyrics(int MMID)
		{
			string url = "http://api.musixmatch.com/ws/1.1/track.lyrics.get?track_id=" + MMID + "&apikey=" + musixmatchAPIkey;
			string lyricsDataData = FetchData.FetchDataFromURL(url);
			return lyricsData;
		}

		public string GetLyrics(Song song)
		{
			return GetLyrics(song.getMMID());
		}

		public int isMatch(Song song)
		{
			string url = "http://api.musixmatch.com/ws/1.1/matcher.track.get?q_artist=" + song.getArtist() + "&q_track=" + song.getTitle() + "&apikey=" + musixmatchAPIkey;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.AutomaticDecompression = DecompressionMethods.GZip;

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				lyricsData = reader.ReadToEnd();
			}
			var obj = JObject.Parse(lyricsData);
			var returnCode = (string)obj["message"]["header"]["status_code"];
			if (returnCode.Equals("200"))
				return (int)obj["body"]["track"]["track_id"];
			return -1;
		}
	}
	
}