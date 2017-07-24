using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Request library
using System.Net;
using System.IO;

namespace LyrickPick.Processors
{
    public class FetchSongs
    {
        private List<string> songsList;
        private static string lastFMApiKey = "4bd79a8ccabeea1dc0b33f9de91f481b";
        private static string dataFormat = "json";
        private string songsData = String.Empty;

        public string getSongsData()
        {
            return songsData;
        }

        public FetchSongs()
        {
            songsList = new List<String>();
            GetSongs();
        }

        public void GetSongs()
        {
            string url = "http://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key=" + lastFMApiKey + "&format=" + dataFormat;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                songsData = reader.ReadToEnd();
            }
        }
    }
}