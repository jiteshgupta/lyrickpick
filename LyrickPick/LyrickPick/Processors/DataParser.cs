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

			foreach (var data in dynObj.tracks.track)
			{
				string songName = data.name;
				string artistName = data.artist.name;
				Song song = new Song(artistName, songName);
				items.Add(song);
			}

			return items;
		}
	}
}