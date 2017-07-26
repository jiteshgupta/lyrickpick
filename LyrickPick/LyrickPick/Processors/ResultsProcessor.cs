using System;
using System.Collections.Generic;

namespace LyrickPick.Processors
{
    public class ResultsProcessor
    {
        MMSearch mm = new MMSearch();
        public Boolean checkArtistGuess(string userGuess, Song currentSong)
        {
            List<int> guesses = mm.matchArtist(userGuess);
            return guesses.Contains(currentSong.getMMIDArtist());
        }
        public Boolean checkSongGuess(string userGuess, Song currentSong)
        {
            List<int> guesses = mm.matchTrack(userGuess);
            if (guesses.Contains(currentSong.getMMID()))
            {
                return true;
            }
            guesses = mm.matchTrack(fixGuess(userGuess));
            return guesses.Contains(currentSong.getMMID());
        }
        public string fixGuess(string userGuess)
        {
            string url = "https://api.cognitive.microsoft.com/bing/v5.0/spellcheck/?text=" + Uri.EscapeDataString(userGuess);
            string data = FetchData.FetchDataBSC(url);
            DataParser dp = new DataParser();
            string result = dp.FixGuess(userGuess, data);
            return result;
        }
    }
}