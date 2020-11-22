using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void PrependBytesTest()
        {
            byte[] array = new byte[] { 0, 10, 20, 30 };
            byte[] array2 = new byte[] { 40, 50, 60, 70 };
            byte[] arrayTest = new byte[] { 40, 50, 60, 70, 0, 10, 20, 30 };
            byte[] arrayPrepend = Extensions.PrependBytes(array, array2);

            CollectionAssert.AreEqual(arrayTest, arrayPrepend);
        }
        [TestMethod()]
        public void AppendBytesTest()
        {
            byte[] array = new byte[] { 0, 10, 20, 30 };
            byte[] array2 = new byte[] { 40, 50, 60, 70 };
            byte[] arrayTest = new byte[] { 0, 10, 20, 30, 40, 50, 60, 70 };
            byte[] arrayPrepend = Extensions.AppendBytes(array, array2);

            CollectionAssert.AreEqual(arrayTest, arrayPrepend);
        }
    }
}