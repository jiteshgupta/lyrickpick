using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LyrickPick.Processors
{
	[Serializable]
	public class Quiz
	{
		public List<Song> songs;
		public List<Song> selectedSongs;
		public FetchLyrics fl;
		public LyricProcessor lp;
		public DataParser dp;
		public FetchSongs fs;
		public Random random = new Random();
		public int pageNum = 1;

		public string foo = "Test";

		public Context context;
		public int totalScore = 0;

		public Context GetCurrentContext()
		{
			return context;
		}

		public string GetFoo()
		{
			return foo;
		}

		public LyricProcessor GetLyricProcessor()
		{
			return lp;
		}

		public Quiz()
		{
			selectedSongs = new List<Song>();

			fl = new FetchLyrics();
			fs = new FetchSongs();
			dp = new DataParser();
			lp = new LyricProcessor();
			//populate the songs list
			songs = dp.GetSongList(fs.getSongsData());

		}
		/*stretch goal
		public Quiz(string Genre)
		{
			
		}
		*/

		public string Question()
		{
			//create a new question context
			context = new Context();
			foo = "Fooo";
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
			selectedSongs.Add(song);
			songs.Remove(song);
			if (songs.Count == 0)
			{
				incrementPage();
				//populate the songs list
				songs = dp.GetSongList(fs.GetSongs(pageNum));
			}
			return song;
		}

		public void incrementPage()
		{
			pageNum++;
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