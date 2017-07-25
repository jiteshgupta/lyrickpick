using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
    public class Quiz
    {
        static List<Song> songs;
        static List<Song> selectedSongs;
        static FetchLyrics fl;
        static int score = 0;
        static Random random = new Random();

        public Quiz()
        {
            FetchSongs fs = new FetchSongs();
            DataParser dp = new DataParser();
            selectedSongs = new List<Song>();
            //selectedSongs.Add(new Song());
            fl = new FetchLyrics();
            songs = dp.GetSongList(fs.getSongsData());
        }
        /*stretch goal
        public Quiz(string Genre)
        {
            
        }
        */

        public string Question()
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
            List<String> lines = LyricProcessor.SpliceSong(json);
            List<String> selectedLines = new List<String>();
            string question = LyricProcessor.selectLine(lines, selectedLines);
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