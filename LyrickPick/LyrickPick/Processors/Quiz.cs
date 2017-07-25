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
        private DataParser dp;
        private FetchSongs fs;
		private Random random = new Random();
        private int pageNum = 1;

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
                songs = dp.GetSongList(fs.GetSongsMusicXmatch(pageNum));
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