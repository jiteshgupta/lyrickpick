using System;
using System.Collections.Generic;

namespace LyrickPick.Processors
{
    [Serializable]
	public class Quiz
	{
		
		public static FetchLyrics fl = new FetchLyrics();
		public static LyricProcessor lp = new LyricProcessor(); 
		public static DataParser dp = new DataParser();
		public static FetchSongs fs = new FetchSongs();
		//populate the songs list
		public static List<Song> songs = dp.GetSongList(fs.getSongsData());

		public Random random = new Random();
		public int pageNum = 1;
		public Context context;
		public int totalScore = 0;

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

		}

		public string Question()
		{
			//create a new question context
			context = new Context();

			//select a song
			Song currentSong = selectSong();
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
			songs.Remove(song);
			if (songs.Count == 0)
			{
				incrementPage();
				//populate the songs list
				songs = dp.GetSongList(fs.GetSongs(pageNum));
			}
			return song;
		}

		private void incrementPage()
		{
			pageNum++;
		}

		public string ProcessHint()
		{
			if (context.GetIsHintUsed())
			{
				return "Hint has already been used!!!";
			}
			else
			{
				context.SetIsHintUsed(true);
                var hint = lp.selectLine(context.GetCurrentSongLines(), context.GetSelectedLinesList());
				context.AddtoSelectedLines(hint);
                return hint;
			}
		}
	}
}