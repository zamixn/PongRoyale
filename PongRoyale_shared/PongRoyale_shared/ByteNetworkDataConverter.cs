using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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

        public byte[] EncodeBallData(byte[] ballIds, Vector2[] ballPositions, Vector2[] ballDirections)
        {
            byte[] data = ballIds.PrependBytes(new byte[] { (byte)ballIds.Length });
            for (int i = 0; i < ballPositions.Length; i++)
            {
                data = data.AppendBytes(EncodeVector(ballPositions[i]));
            }
            for (int i = 0; i < ballDirections.Length; i++)
            {
                data = data.AppendBytes(EncodeVector(ballDirections[i]));
            }
            return data;
        }

        public void DecodeBallData(byte[] data, out byte[] ballIds, out Vector2[] ballPositions, out Vector2[] ballDirections)
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
            ballDirections = new Vector2[length];
            for (int i = 0; i < length; i++)
            {
                int index = length + (Vector2.ByteSize * length) + (Vector2.ByteSize * i);
                ballDirections[i] = DecodeVector2(data, index + 1);
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

        public byte[] EncodeObstacleData(byte id, float width, float height, float duration, float posX, float posY, byte type)
        {
            return (new byte[] { id })
                        .AppendBytes(EncodeFloat(width))
                        .AppendBytes(EncodeFloat(height))
                        .AppendBytes(EncodeFloat(duration))
                        .AppendBytes(EncodeFloat(posX))
                        .AppendBytes(EncodeFloat(posY))
                        .AppendBytes(new byte[] { type });
        }

        public void DecodeObstacleData(byte[] data, out byte id, out float width, out float height, out float duration, out float posX, out float posY, out byte type)
        {
            int index = 0;
            id = data[0];
            index += 1;
            width = DecodeFloat(data, index);
            index += 4;
            height = DecodeFloat(data, index);
            index += 4;
            duration = DecodeFloat(data, index);
            index += 4;
            posX = DecodeFloat(data, index);
            index += 4;
            posY = DecodeFloat(data, index);
            index += 4;
            type = data[index];
        }

        public byte[] EncodePowerupData(byte id, float radius, float duration, float posX, float posY, byte type, byte[] powerUppedData)
        {
            return (new byte[] { id })
                        .AppendBytes(EncodeFloat(radius))
                        .AppendBytes(EncodeFloat(duration))
                        .AppendBytes(EncodeFloat(posX))
                        .AppendBytes(EncodeFloat(posY))
                        .AppendBytes(new byte[] { type })
                        .AppendBytes(powerUppedData);
        }

        public void DecodePowerupData(byte[] data, out byte id, out float radius, out float duration, out float posX, out float posY, out byte type, out byte[] powerUppedData)
        {
            int index = 0;
            id = data[0];
            index += 1;
            radius = DecodeFloat(data, index);
            index += 4;
            duration = DecodeFloat(data, index);
            index += 4;
            posX = DecodeFloat(data, index);
            index += 4;
            posY = DecodeFloat(data, index);
            index += 4;
            type = data[index];
            index += 1;
            powerUppedData = new byte[data.Length - index];
            for (int i = 0; i < powerUppedData.Length; i++)
                powerUppedData[i] = data[index++];
        }

        public byte[] EncodeBallPoweredUpData(byte ballId, byte powerupId, byte[] poweredUp)
        {
            return poweredUp.PrependBytes(new byte[] { ballId, powerupId });
        }
        public void DecodeBallPoweredUpData(byte[] data, out byte ballId, out byte powerUpId, out byte[] poweredUp)
        {
            ballId = data[0];
            powerUpId = data[1];
            poweredUp = new byte[data.Length - 2];
            for (int i = 0; i < poweredUp.Length; i++)
                poweredUp[i] = data[i + 2];
        }
    }
}
