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
    }
}
