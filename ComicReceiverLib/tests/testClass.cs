using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DailyDilbertViewer;
using System.Drawing;
using System.IO;

namespace ComicReceiverLib.tests
{
    [TestClass]
    class testClass
    {
        
        [TestMethod]
        public void testReceiveFunction()
        {
            DilbertReceiver receiver = new DilbertReceiver();
            Image image = receiver.getDilbertComicImageByDate(DateTime.Now);
            Assert.IsTrue(image.Height > 0);
            Assert.IsTrue(image.Width > 0);
        }

    }
}
