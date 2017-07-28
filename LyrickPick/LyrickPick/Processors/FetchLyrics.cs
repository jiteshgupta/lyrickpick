using Newtonsoft.Json.Linq;


namespace LyrickPick.Processors
{
    public class FetchLyrics
	{
        private static string musixmatchAPIkey = "8b7654870c8395335a30eb19039218f6";
        private string lyricsData = string.Empty;
		
        public string GetLyricsData()
		{
			return lyricsData;
		}

		public string GetLyrics(int MMID)
		{
			string url = "http://api.musixmatch.com/ws/1.1/track.lyrics.get?commontrack_id=" + MMID + "&apikey=" + musixmatchAPIkey;
			string lyricsData = FetchData.FetchDataFromURL(url);
			return lyricsData;
		}

		public string GetLyrics(Song song)
		{
			return GetLyrics(song.getMMID());
		}

        /*not used
		public int isMatch(Song song)
		{
			string url = "http://api.musixmatch.com/ws/1.1/matcher.track.get?q_artist=" + song.getArtist() + "&q_track=" + song.getTitle() + "&apikey=" + musixmatchAPIkey;
			lyricsData = FetchData.FetchDataFromURL(url);

			var obj = JObject.Parse(lyricsData);
			var returnCode = (string)obj["message"]["header"]["status_code"];
			if (returnCode.Equals("200"))
				return (int)obj["body"]["track"]["commontrack_id"];
			return -1;
		}
        */
	}
	
}