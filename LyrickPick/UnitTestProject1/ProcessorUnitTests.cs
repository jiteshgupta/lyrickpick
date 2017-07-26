using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LyrickPick.Processors;
using System.Collections.Generic;

namespace LyrickPickUnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestQuiz()
        {
            Quiz qz = new Quiz();
            string question = qz.Question();
            Console.WriteLine(question);
            Assert.IsNotNull(question);

        }
        [TestMethod]
        public void TestArtistMatch()
        {
            MMSearch mm = new MMSearch();
            List<int> artistIDs = mm.matchArtist("justin bieber");

            FetchSongs fs = new FetchSongs();
            fs.GetSongsByArtist(artistIDs[0], 1);
            fs.GetSongsByArtist("justin bieber", 1);
        }
        [TestMethod]
        public void TestGuessMatch()
        {
            string artistName = "Luis Fonsi feat. Daddy Yankee & Justin Bieber";
            string songTitle = "Despacito - Remix";
            int MMID = 71538874;
            int MMIDArtist = 33466556;
            Song currentSong = new Song(artistName, songTitle, MMID, MMIDArtist);

            ResultsProcessor rp = new ResultsProcessor();
            Assert.IsTrue(rp.checkSongGuess("despacito remix", currentSong));
            Assert.IsTrue(rp.checkArtistGuess("luis fonsi", currentSong));
        }

        [TestMethod]
        public void TestSongSelection()
        {
            Quiz qz = new Quiz();
            int questions = 100;
            string title;
            for (int i = 1; i <= questions; i++)
            {
                title = qz.selectSong().getTitle();
            }
           
        }

        [TestMethod]
        public void TestGuessCleanup()
        {
            ResultsProcessor rp = new ResultsProcessor();
            rp.fixGuess("it's gona beme");
        }


    }
}
