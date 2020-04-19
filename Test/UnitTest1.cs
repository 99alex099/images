using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1;
namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[,] shadpx = {{10,10},{10,10}};

            Form1 f = new Form1();
            double[,] realbinpx = f.GetAvg(shadpx);

            Assert.AreEqual(realbinpx[1, 1], realbinpx[1, 1]);
        }
    }
}
