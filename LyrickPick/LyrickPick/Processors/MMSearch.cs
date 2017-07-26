using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LyrickPick.Processors
{
    public class MMSearch
    {
        private static string musixmatchAPIkey = "8b7654870c8395335a30eb19039218f6";
        private string songsData = string.Empty;

        /*
        public int matchArtist(string artist)
        {
            string url = "http://api.musixmatch.com/ws/1.1/artist.search?q_artist=" + Uri.EscapeDataString(artist) + "&apikey=" + musixmatchAPIkey;
            string data = FetchData.FetchDataFromURL(url);
            JObject j = JObject.Parse(data);
            //returns the first
            int artistID = (int)j.SelectToken("message.body.artist_list[0].artist.artist_id");
            return artistID;
        }
        */
        //returns list of artistIDs
        public List<int> matchArtist(string artist)
        {
            string url = "http://api.musixmatch.com/ws/1.1/artist.search?q_artist=" + Uri.EscapeDataString(artist) + "&apikey=" + musixmatchAPIkey;
            string data = FetchData.FetchDataFromURL(url);
            return DataParser.GetArtistMatches(data);
        }
        //return list of trackID
        public List<int> matchTrack(string songtitle)
        {
            string url = "http://api.musixmatch.com/ws/1.1/track.search?page=1&page_size=100&q_track=" + Uri.EscapeDataString(songtitle) + "&apikey=" + musixmatchAPIkey;
            string data = FetchData.FetchDataFromURL(url);
            
            return DataParser.GetTrackMatches(data);
        }
        
        public int matchGenrebyArtist(string artist)
        {
            string url = "http://api.musixmatch.com/ws/1.1/artist.search?q_artist=" + Uri.EscapeDataString(artist) + "&apikey=" + musixmatchAPIkey;
            string data = FetchData.FetchDataFromURL(url);
            JObject j = JObject.Parse(data);
            //returns the first
            int artistID = (int)j.SelectToken("message.body.artist_list[0].artist.artist_id");
            return artistID;
        }

    }
}