using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LyrickPick.Processors
{
	public class DataParser
	{
		public List<Song> GetSongList(string jsonData)
		{
			List<Song> items = new List<Song>();

			dynamic dynObj = JsonConvert.DeserializeObject(jsonData);

			foreach (var data in dynObj.message.body.track_list)
			{
				string songName = data.track.track_name;
				string artistName = data.track.artist_name;
				int MMID = data.track.commontrack_id;
				Song song = new Song(artistName, songName, MMID);
				items.Add(song);
			}

			return items;
		}
	}
}