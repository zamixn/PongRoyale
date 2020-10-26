﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_shared
{
    public class ByteNetworkDataConverter : INetworkDataConverter
    {
        public byte[] EncodeString(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
        public string DecodeString(byte[] b)
        {
            return Encoding.UTF8.GetString(b, 0, b.Length);
        }

        public byte[] EncodeFloat(float f)
        {
            return BitConverter.GetBytes(f);
        }
        public float DecodeFloat(byte[] b, int index = 0)
        {
            return BitConverter.ToSingle(b, index);
        }

        public byte[] EncodeInt(int i)
        {
            return BitConverter.GetBytes(i);
        }
        public int DecodeInt(byte[] b, int index = 0)
        {
            return BitConverter.ToInt32(b, index);
        }

        public byte[] EncodeColor(Color c)
        {
            return new byte[] { c.A, c.R, c.G, c.B };
        }
        public Color DecodeColor(byte[] data, int index = 0)
        {
            return Color.FromArgb(data[index], data[index + 1], data[index + 2], data[index + 3]);
        }

        public byte[] EncodeVector(Vector2 v)
        {
            return EncodeFloat(v.X).AppendBytes(EncodeFloat(v.Y));
        }
        public Vector2 DecodeVector2(byte[] data, int index = 0)
        {
            return new Vector2(DecodeFloat(data, index), DecodeFloat(data, index + 4));
        }

        public byte[] EncodeGameStartMessage(byte[] playerIds, byte[] paddleTypes, byte ballType, byte roomMasterId)
        {
            byte[] data = playerIds.AppendBytes(paddleTypes)
                          .PrependBytes(new byte[] { (byte)playerIds.Length, ballType, roomMasterId });
            return data;
        }
        public void DecodeGameStartMessage(byte[] data, out byte[] playerIds, out PaddleType[] paddleTypes, out BallType ballType, out byte roomMasterId)
        {
            int playerCount = data[0];
            playerIds = new byte[playerCount];
            paddleTypes = new PaddleType[playerCount];
            ballType = (BallType)data[1];
            roomMasterId = data[2];
            for (int i = 0; i < playerCount; i++)
            {
                playerIds[i] = data[i + 3];
                paddleTypes[i] = (PaddleType)data[i + 3 + playerCount];
            }
        }

        public byte[] EncodeBallData(byte[] ballIds, Vector2[] ballPositions)
        {
            byte[] data = ballIds.PrependBytes(new byte[] { (byte)ballIds.Length });
            for (int i = 0; i < ballPositions.Length; i++)
            {
                data = data.AppendBytes(EncodeVector(ballPositions[i]));
            }
            return data;
        }

        public void DecodeBallData(byte[] data, out byte[] ballIds, out Vector2[] ballPositions)
        {
            byte length = data[0];
            ballIds = new byte[length];
            ballPositions = new Vector2[length];
            for (int i = 0; i < length; i++)
            {
                ballIds[i] = data[i + 1];
                int index = length + (Vector2.ByteSize) * i;
                ballPositions[i] = DecodeVector2(data, index + 1);
            }
        }

        public byte[] EncodeRoundOverData(BallType[] ballTypes, byte[] ballIds, byte[] playerIds, byte[] playerLifes)
        {
            byte[] data = new byte[ballTypes.Length + 1];
            data[0] = (byte)ballTypes.Length;
            for (int i = 0; i < ballTypes.Length; i++)
                data[1 + i] = (byte)ballTypes[i];
            data = data.AppendBytes(ballIds)
                .AppendBytes(new byte[] { (byte)playerIds.Length })
                .AppendBytes(playerIds).AppendBytes(playerLifes);
            return data;
        }
        public void DecodeRoundOverData(byte[] data, out BallType[] ballTypes, out byte[] ids, out byte[] playerIds, out byte[] playerLifes)
        {
            ballTypes = new BallType[data[0]];
            for (int i = 0; i < ballTypes.Length; i++)
                ballTypes[i] = (BallType)data[i + 1];
            ids = new byte[data[0]];
            for (int i = 0; i < ballTypes.Length; i++)
                ids[i] = data[1 + ballTypes.Length + i];

            int offset = 1 + ballTypes.Length * 2;
            playerIds = new byte[data[offset]];
            playerLifes = new byte[playerIds.Length];
            for (int i = 0; i < playerIds.Length; i++)
            {
                playerIds[i] = data[1 + offset + i];
                playerLifes[i] = data[1 + playerIds.Length + offset + i];
            }
        }

        public byte[] EncodeObstacleData(float width, float height, Color color, float duration, float posX, float posY)
        {
            return EncodeFloat(width)
                        .AppendBytes(EncodeFloat(height))
                        .AppendBytes(EncodeColor(color))
                        .AppendBytes(EncodeFloat(duration))
                        .AppendBytes(EncodeFloat(posX))
                        .AppendBytes(EncodeFloat(posY));
        }

        public void DecodeObstacleData(byte[] data, out float width, out float height, out Color color, out float duration, out float posX, out float posY)
        {
            int index = 0;
            width = DecodeFloat(data, index);
            index += 4;
            height = DecodeFloat(data, index);
            index += 4;
            color = DecodeColor(data, index);
            index += 4;
            duration = DecodeFloat(data, index);
            index += 4;
            posX = DecodeFloat(data, index);
            index += 4;
            posY = DecodeFloat(data, index);
        }
    }
}
