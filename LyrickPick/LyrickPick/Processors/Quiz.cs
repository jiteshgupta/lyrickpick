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

		private Context context;
		private int totalScore = 0;

		public Context GetCurrentContext()
		{
			return context;
		}

		public LyricProcessor GetLyricProcessor()
		{
			return lp;
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
			Song currentSong = selectSong();
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
			//create a new question context
			context = new Context();

			//select a song
			Song currentSong = selectSong();
			selectedSongs.Add(currentSong);
			context.SetCurrentSong(currentSong);

			//fetch lyrics
			string json = fl.GetLyrics(currentSong.getMMID());

			//splice song
			string[] lines = lp.SpliceSong(json);
			context.SetCurrentSongLines(lines);

			//randomly select a line from the song
			string question = lp.selectLine(lines, new List<string>());
			context.AddtoSelectedLines(question);

			//ask question
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

		public string processHint()
		{
			string hint;
			if (context.GetIsHintUsed())
			{
				hint = "Hint has already been used!!!";
			}
			else
			{
				context.SetIsHintUsed(true);
				hint = lp.selectLine(context.GetCurrentSongLines(), context.GetSelectedLinesList());
				context.AddtoSelectedLines(hint);
			}

			return hint;
		}
	}
}