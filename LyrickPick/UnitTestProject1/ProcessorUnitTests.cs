using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LyrickPick.Processors;

namespace LyrickPickUnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Quiz qz = new Quiz();
            string question = qz.Question();
            Console.WriteLine(question);
            Assert.IsNotNull(question);

        }
        [TestMethod]
        public void TestMethod2()
        {
            MMSearch mm = new MMSearch();
            int artistID = mm.matchArtist("justin bieber");

            FetchSongs fs = new FetchSongs();
            fs.GetSongsByArtist(artistID, 1);
            fs.GetSongsByArtist("justin bieber", 1);
        }
    }
}
