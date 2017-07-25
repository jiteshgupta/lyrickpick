using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
	public class Quiz
	{
		private List<Song> songs;
		private List<Song> selectedSongs;
		private FetchLyrics fl;
		private LyricProcessor lp;
		private Random random = new Random();

		private Song currentSong;
		private List<String> selectedLines;

		private int totalScore = 0;

		public string GetCurrentSongTitle()
		{
			return currentSong.getTitle();
		}

		public string GetCurrentSongArtist()
		{
			return currentSong.getArtist();
		}

		public Quiz()
		{
			FetchSongs fs = new FetchSongs();
			DataParser dp = new DataParser();

			//populate the songs list
			songs = dp.GetSongList(fs.getSongsData());

			selectedSongs = new List<Song>();

			fl = new FetchLyrics();
			lp = new LyricProcessor();
		}
		/*stretch goal
		public Quiz(string Genre)
		{
			
		}
		*/

		public string QuestionLastFM()
		{
			//select a song
			currentSong = selectSong();
			selectedSongs.Add(currentSong);

			currentSong.setMMID(fl.isMatch(currentSong));
			while (currentSong.getMMID() < 0)
			{
				currentSong = selectSong();
				selectedSongs.Add(currentSong);
				currentSong.setMMID(fl.isMatch(currentSong));
			}
			string json = fl.GetLyrics(currentSong);
			string[] lines = lp.SpliceSong(json);
			List<String> selectedLines = new List<String>();
			string question = lp.selectLine(lines, selectedLines);
			return question;

		}
		public string Question()
		{
			//select a song
			currentSong = selectSong();
			selectedSongs.Add(currentSong);

			//fetch lyrics
			string json = fl.GetLyrics(currentSong.getMMID());

			//splice song
			string[] lines = lp.SpliceSong(json);
			selectedLines = new List<String>();

			//randomly select a line from the song
			string question = lp.selectLine(lines, selectedLines);
			selectedLines.Add(question);

			return question;

		}
		public Song selectSong()
		{
			Song song = songs[random.Next(0, songs.Count)];
			while (selectedSongs.Contains(song))
			{
				song = songs[random.Next(0, songs.Count)];
			}
			return song;
		}
	}
}