using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client;
using System;
using System.Collections.Generic;
using System.Text;
using PongRoyale_shared;
using System.Drawing;
using System.Linq;
using PongRoyale_client.Game.ArenaObjects.Powerups;

namespace PongRoyale_client.Tests
{
    [TestClass()]
    public class NetworkDataAdapterTests
    {
        private NetworkDataAdapter Converter;

        [TestMethod()]
        public void NetworkDataAdapterTest()
        {
            throw new NotImplementedException();
        }


        [TestInitialize()]
        public void Initialize()
        {
            Converter = new NetworkDataAdapter();
        }

        [TestCleanup()]
        public void Cleanup() { }

        [TestMethod()]
        public void EncodeStringTest()
        {
            var input = "hello";
            var data = Converter.EncodeString(input);
            var output = Converter.DecodeString(data);
            var expected = input;
            Assert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeFloatTest()
        {
            var input = 3.14f;
            var data = Converter.EncodeFloat(input);
            var output = Converter.DecodeFloat(data);
            var expected = input;
            Assert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeFloatTest2()
        {
            var input = 3.14f;
            var data = Converter.EncodeFloat(input);
            var output = Converter.DecodeFloat(data.PrependBytes(new byte[] { 0, 0, 0 }), 3);
            var expected = input;
            Assert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeIntTest()
        {
            var input = 6;
            var data = Converter.EncodeInt(input);
            var output = Converter.DecodeInt(data);
            var expected = input;
            Assert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeIntTest2()
        {
            var input = 1;
            var data = Converter.EncodeInt(input);
            var output = Converter.DecodeInt(data.PrependBytes(new byte[] { 0, 0, 0 }), 3);
            var expected = input;
            Assert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeColorTest()
        {
            var input = Color.FromArgb(255, 255, 0, 0);
            var data = Converter.EncodeColor(input);
            var output = Converter.DecodeColor(data);
            var expected = input;
            Assert.AreEqual(output, expected);

        }

        [TestMethod()]
        public void EncodeColorTest2()
        {
            var input = Color.FromArgb(255, 255, 0, 0);
            var data = Converter.EncodeColor(input);
            var output = Converter.DecodeColor(data.PrependBytes(new byte[] { 0, 0, 0 }), 3);
            var expected = input;
            Assert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeVectorTest()
        {
            var input = new Vector2(2.2f, 3.3f);
            var data = Converter.EncodeVector(input);
            var output = Converter.DecodeVector2(data);
            var expected = input;
            Assert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeVectorTest2()
        {
            var input = new Vector2(2.2f, 3.3f);
            var data = Converter.EncodeVector(input);
            var output = Converter.DecodeVector2(data.PrependBytes(new byte[] { 0, 0, 0 }), 3);
            var expected = input;
            Assert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeGameStartMessageTest()
        {
            var playerIds = new byte[] { 0, 1, 2, 3 };
            var paddleTypes = new byte[] { 4, 5, 6, 7 };
            byte ballType = 2;
            byte roomMasterId = 3;

            var data = Converter.EncodeGameStartMessage(playerIds, paddleTypes, ballType, roomMasterId);
            Converter.DecodeGameStartMessage(data, out byte[] pIds, out PaddleType[] pTypes, out BallType bType, out byte rmId);
            var expected = playerIds.AppendBytes(paddleTypes).AppendBytes(new byte[] { ballType, roomMasterId });
            var output = pIds.AppendBytes(pTypes.Select(pt => (byte)pt).ToArray()).AppendBytes(new byte[] { (byte)bType, rmId });
            CollectionAssert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeBallDataTest()
        {
            var ballIds = new byte[] { 0, 1 };
            var ballPositions = new Vector2[] { new Vector2(1.2f, 1.3f), new Vector2(5, 6) };
            var ballDirections = new Vector2[] { new Vector2(.5f, .5f), new Vector2(.1f, .2f) };

            var data = Converter.EncodeBallData(ballIds, ballPositions, ballDirections);
            Converter.DecodeBallData(data, out byte[] bIds, out Vector2[] bPs, out Vector2[] bDs);
            var expected = ballIds
                .AppendBytes(ballPositions.Select(p => Converter.EncodeVector(p)).SelectMany(i => i).ToArray())
                .AppendBytes(ballDirections.Select(p => Converter.EncodeVector(p)).SelectMany(i => i).ToArray());
            var output = bIds
                .AppendBytes(bPs.Select(p => Converter.EncodeVector(p)).SelectMany(i => i).ToArray())
                .AppendBytes(bDs.Select(p => Converter.EncodeVector(p)).SelectMany(i => i).ToArray());
            CollectionAssert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeRoundOverDataTest()
        {
            var ballTypes = new BallType[] { BallType.Deadly, BallType.Normal };
            var ballIds = new byte[] { 1, 2 };
            var playerIds = new byte[] { 3, 4 };
            var playerLifes = new byte[] { 2, 2 };

            var data = Converter.EncodeRoundOverData(ballTypes, ballIds, playerIds, playerLifes);
            Converter.DecodeRoundOverData(data, out BallType[] bTs, out byte[] bIds, out byte[] pIds, out byte[] pLifes);
            var expected = ballTypes.Select(t => (byte)t).ToArray()
                .AppendBytes(ballIds).AppendBytes(playerIds).AppendBytes(playerLifes);
            var output = bTs.Select(t => (byte)t).ToArray()
                .AppendBytes(bIds).AppendBytes(pIds).AppendBytes(pLifes);
            CollectionAssert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeObstacleDataTest()
        {
            byte id = 4;
            float width = 123.2f;
            float height = 345.67f;
            float duration = 4.3f;
            float posX = 5.6f;
            float posY = 4.6f;
            byte type = 2;

            var data = Converter.EncodeObstacleData(id, width, height, duration, posX, posY, type);
            Converter.DecodeObstacleData(data, out byte _id, out float _width, out float _height, out float _duration, out float _posX, out float _posY, out byte _type);
            var expected = (new byte[] { id })
                        .AppendBytes(new float[] { width, height, duration, posX, posY }.Select(f => Converter.EncodeFloat(f)).SelectMany(i => i).ToArray())
                        .AppendBytes(new byte[] { type });
            var output = (new byte[] { _id })
                        .AppendBytes(new float[] { _width, _height, _duration, _posX, _posY }.Select(f => Converter.EncodeFloat(f)).SelectMany(i => i).ToArray())
                        .AppendBytes(new byte[] { _type });
            CollectionAssert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodePowerupDataTest()
        {
            byte id = 4;
            float radius = 345.67f;
            float duration = 4.3f;
            float posX = 5.6f;
            float posY = 4.6f;
            byte type = 2;
            var pData = new byte[] { 3, 2, 4, 1, 5, 0, 6 };

            var data = Converter.EncodePowerupData(id, radius, duration, posX, posY, type, pData);
            Converter.DecodePowerupData(data, out byte _id, out float _radius, out float _duration, out float _posX, out float _posY, out byte _type, out byte[] _pData);
            var expected = (new byte[] { id })
                        .AppendBytes(new float[] { radius, duration, posX, posY }.Select(f => Converter.EncodeFloat(f)).SelectMany(i => i).ToArray())
                        .AppendBytes(new byte[] { type })
                        .AppendBytes(pData);
            var output = (new byte[] { _id })
                        .AppendBytes(new float[] { _radius, _duration, _posX, _posY }.Select(f => Converter.EncodeFloat(f)).SelectMany(i => i).ToArray())
                        .AppendBytes(new byte[] { _type })
                        .AppendBytes(_pData);
            CollectionAssert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeBallPoweredUpDataTest()
        {
            byte bId = 4;
            byte pId = 5;
            var pData = new byte[] { 3, 2, 4, 1, 5, 0, 6 };

            var data = Converter.EncodeBallPoweredUpData(bId, pId, pData);
            Converter.DecodeBallPoweredUpData(data, out byte _bId, out byte _pId, out byte[] _pData);
            var expected = (new byte[] { bId, pId })
                        .AppendBytes(pData);
            var output = (new byte[] { _bId, _pId })
                        .AppendBytes(_pData);
            CollectionAssert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodePaddlePoweredUpDataTest()
        {
            byte pId = 5;
            byte bId = 4;
            var pData = new byte[] { 3, 2, 4, 1, 5, 0, 6 };

            var data = Converter.EncodePaddlePoweredUpData(pId, bId, pData);
            Converter.DecodePaddlePoweredUpData(data, out byte _pId, out byte _bId, out byte[] _pData);
            var expected = (new byte[] { bId, pId })
                        .AppendBytes(pData);
            var output = (new byte[] { _bId, _pId })
                        .AppendBytes(_pData);
            CollectionAssert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodePaddlePoweredUpDataTest1()
        {
            byte pId = 5;
            byte bId = 4;
            var pData = new PoweredUpData() { ChangeBallDirection = true};

            var data = Converter.EncodePaddlePoweredUpData(pId, bId, pData);
            Converter.DecodePaddlePoweredUpData(data, out byte _pId, out byte _bId, out PoweredUpData _pData);
            var expected = (new byte[] { bId, pId })
                        .AppendBytes(pData.ToByteArray());
            var output = (new byte[] { _bId, _pId })
                        .AppendBytes(_pData.ToByteArray());
            CollectionAssert.AreEqual(output, expected);
        }

        [TestMethod()]
        public void EncodeBallPoweredUpDataTest1()
        {
            byte bId = 4;
            byte pId = 5;
            var pData = new PoweredUpData() { ChangeBallDirection = true };

            var data = Converter.EncodeBallPoweredUpData(bId, pId, pData);
            Converter.DecodeBallPoweredUpData(data, out byte _bId, out byte _pId, out PoweredUpData _pData);
            var expected = (new byte[] { bId, pId })
                        .AppendBytes(pData.ToByteArray());
            var output = (new byte[] { _bId, _pId })
                        .AppendBytes(_pData.ToByteArray());
            CollectionAssert.AreEqual(output, expected);
        }
    }
}