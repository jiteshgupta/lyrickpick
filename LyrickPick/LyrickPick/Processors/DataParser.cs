using System.Collections.Generic;
using Newtonsoft.Json;

namespace LyrickPick.Processors
{
    public class DataParser
	{
		public static List<Song> GetSongList(string jsonData)
		{
			var items = new List<Song>();

			dynamic dynObj = JsonConvert.DeserializeObject(jsonData);

			foreach (var data in dynObj.message.body.track_list)
			{
				string songName = data.track.track_name;
				string artistName = data.track.artist_name;
				int MMID = data.track.commontrack_id;
                int MMIDArtist = data.track.artist_id;
				Song song = new Song(artistName, songName, MMID, MMIDArtist);
				items.Add(song);
			}

			return items;
		}

        public static List<int> GetTrackMatches(string jsonData)
        {
            var items = new List<int>();

            dynamic dynObj = JsonConvert.DeserializeObject(jsonData);

            foreach (var data in dynObj.message.body.track_list)
            {
                int MMID = data.track.commontrack_id;
                items.Add(MMID);
            }

            return items;
        }
        public static List<int> GetArtistMatches(string jsonData)
        {
            var items = new List<int>();

            dynamic dynObj = JsonConvert.DeserializeObject(jsonData);

            foreach (var data in dynObj.message.body.artist_list)
            {
                int artistID = data.artist.artist_id;
                items.Add(artistID);
            }

            return items;
        }

        //
        /// <summary>
        /// Replace Misspellings
        /// </summary>
        /// <param name="userGuess">User's Guess</param>
        /// <param name="jsonData">Bing Spellcheck return</param>
        /// <returns>Corrected user guess</returns>
        public string FixGuess(string userGuess, string jsonData)
        {
            string result = userGuess;
            dynamic dynObj = JsonConvert.DeserializeObject(jsonData);

            foreach (var flaggedToken in dynObj.flaggedTokens)
            {
                var token = flaggedToken.token;
                var suggestion = flaggedToken.suggestions[0].suggestion;
                result = result.Replace(token, suggestion);
            }

            return result;
        }
    }
}