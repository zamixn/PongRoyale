using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared.Tests
{
    [TestClass()]
    public class NetworkMessageTests
    {
        [TestMethod()]
        public void NetworkMessageTest()
        {
            var expected = new NetworkMessage(1, NetworkMessage.MessageType.BallPoweredUp, new byte[] { 1, 2, 3, 4, 5, 6 });
            var result = NetworkMessage.FromBytes(expected.ToBytes());
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShallowCloneTest()
        {
            var expected = new NetworkMessage(1, NetworkMessage.MessageType.BallPoweredUp, new byte[] { 1, 2, 3, 4, 5, 6 });
            var result = expected.ShallowClone();
            Assert.AreEqual(true, expected.RefEquals(result));
        }

        [TestMethod()]
        public void DeepCloneTest()
        {
            var expected = new NetworkMessage(1, NetworkMessage.MessageType.BallPoweredUp, new byte[] { 1, 2, 3, 4, 5, 6 });
            var result = expected.DeepClone();
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void ToBytesTest()
        {
            Action a = () => {
                NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[NetworkMessage.MAX_MESSAGE_BYTE_LENGTH + 1]);
                var data = m.ToBytes();
            };
            Assert.ThrowsException<Exception>(a);
        }

        [TestMethod()]
        public void FromBytesTest()
        {
            var expected = new NetworkMessage(1, NetworkMessage.MessageType.BallPoweredUp, new byte[] { 1, 2, 0, 4, 5, 6 });
            var result = NetworkMessage.FromBytes(expected.ToBytes());
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ValidateSenderTest()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            Assert.AreEqual(false, m.ValidateSender());
        }


        [TestMethod()]
        public void EqualsTest()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            Assert.AreEqual(false, m.Equals(1));
        }
        [TestMethod()]
        public void EqualsTest2()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            NetworkMessage m2 = new NetworkMessage(1, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            Assert.AreEqual(false, m.Equals(m2));
        }
        [TestMethod()]
        public void EqualsTest3()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            NetworkMessage m2 = new NetworkMessage(0, NetworkMessage.MessageType.BallSync, new byte[0]);
            Assert.AreEqual(false, m.Equals(m2));
        }
        [TestMethod()]
        public void EqualsTest4()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            NetworkMessage m2 = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[1]);
            Assert.AreEqual(false, m.Equals(m2));
        }



        [TestMethod()]
        public void RefEqualsTest()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            Assert.AreEqual(false, m.RefEquals(1));
        }
        [TestMethod()]
        public void RefEqualsTest2()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            NetworkMessage m2 = new NetworkMessage(1, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            Assert.AreEqual(false, m.RefEquals(m2));
        }
        [TestMethod()]
        public void RefEqualsTest3()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            NetworkMessage m2 = new NetworkMessage(0, NetworkMessage.MessageType.BallSync, new byte[0]);
            Assert.AreEqual(false, m.RefEquals(m2));
        }
        [TestMethod()]
        public void RefEqualsTest4()
        {
            NetworkMessage m = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[0]);
            NetworkMessage m2 = new NetworkMessage(0, NetworkMessage.MessageType.BallPoweredUp, new byte[1]);
            Assert.AreEqual(false, m.RefEquals(m2));
        }
    }
}