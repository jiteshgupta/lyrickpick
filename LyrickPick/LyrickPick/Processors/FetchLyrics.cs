using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Request library
using System.Net;
using System.IO;


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

        public void GetLyrics(Song song)
        {
            string url = "http://api.musixmatch.com/ws/1.1/" + "&apikey" + musixmatchAPIkey;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                lyricsData = reader.ReadToEnd();
            }
        }
    }
    
}