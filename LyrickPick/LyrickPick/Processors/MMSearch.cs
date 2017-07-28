using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LyrickPick.Processors
{
    public class MMSearch
    {
        private static string musixmatchAPIkey = "8b7654870c8395335a30eb19039218f6";
        private string songsData = string.Empty;
        private Dictionary<string, int> genreDict = new Dictionary<string, int>() {
            {"blues", 2},
            {"comedy", 3},
            {"children's", 4},
            {"classical", 5},
            {"country", 6},
            {"electronic", 7},
            {"holiday", 8},
            {"opera", 9},
            {"singer", 10},
            {"songwriter", 10},
            {"jazz", 11},
            {"latin", 12},
            {"new age", 13},
            {"pop", 14},
            {"r&b", 15},
            {"soul", 15},
            {"soundtrack", 16},
            {"dance", 17},
            {"hip hop", 18},
            {"rap", 18},
            {"world", 19},
            {"alternative", 20},
            {"rock", 21},
            {"christian", 22},
            {"gospel", 22},
            {"vocal", 23},
            {"reggae", 24},
            {"heavy metal", 1153} };

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

        //return music_genre_id (-1 if not listed)
        public int matchGenre(string genre)
        {
            if (genreDict.ContainsKey(genre))
                return genreDict[genre];
            return -1;
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