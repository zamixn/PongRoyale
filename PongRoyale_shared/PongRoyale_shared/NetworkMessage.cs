using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PongRoyale_shared
{
    public class NetworkMessage
    {
        public enum MessageType : byte
        {
            Invalid = 0,
            ConnectedToServer = 1,  // server sent only
            Chat = 2,
            PlayerSync = 3,
            BallSync = 4,
            GameStart = 5,
            GameEnd = 6,
            RoundReset = 7
        }

        public byte SenderId;
        public MessageType Type;
        public byte[] ByteContents;

        public NetworkMessage(byte senderId, MessageType type, byte[] contents)
        {
            SenderId = senderId;
            Type = type;
            ByteContents = contents;
        }

        public byte[] ToBytes()
        {
            return ByteContents.PrependBytes(new byte[] { SenderId, (byte)Type });
        }

        public static NetworkMessage FromBytes(byte[] buffer)
        {
            int count = 0;
            byte senderId = buffer[0];
            byte messageType = buffer[1];
            for (int i = 2; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                    break;
                count++;
            }
            NetworkMessage networkMessage = new NetworkMessage(senderId, (MessageType)messageType, buffer.Skip(2).ToArray());
            return networkMessage;
        }

        public bool ValidateSender()
        {
            return SenderId != 0;
        }



        public static byte[] EncodeString(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
        public static string DecodeString(byte[] b)
        {
            return Encoding.UTF8.GetString(b, 0, b.Length);
        }

        public static byte[] EncodeFloat(float f)
        {
            return BitConverter.GetBytes(f);
        }
        public static float DecodeFloat(byte[] b, int index = 0)
        {
            return BitConverter.ToSingle(b, index);
        }

        public static byte[] EncodeVector(Vector2 v)
        {
            return EncodeFloat(v.X).AppendBytes(EncodeFloat(v.Y));
        }
        public static Vector2 DecodeVector2(byte[] data, int index = 0)
        {
            return new Vector2(DecodeFloat(data, index), DecodeFloat(data, index + 4));
        }

        public static byte[] EncodeGameStartMessage(byte[] playerIds, byte[] paddleTypes, byte ballType, byte roomMasterId)
        {
            byte[] data = playerIds.AppendBytes(paddleTypes)
                          .PrependBytes(new byte[] { (byte)playerIds.Length, ballType, roomMasterId });
            return data;
        }
        public static void DecodeGameStartMessage(byte[] data, out byte[] playerIds, out PaddleType[] paddleTypes, out BallType ballType, out byte roomMasterId)
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

        public static byte[] EncodeBallData(byte[] ballIds, Vector2[] ballPositions)
        {
            byte[] data = ballIds.PrependBytes(new byte[] { (byte)ballIds.Length });
            for (int i = 0; i < ballPositions.Length; i++)
            {
                data = data.AppendBytes(EncodeVector(ballPositions[i]));
            }
            return data;
        }

        public static void DencodeBallData(byte[] data, out byte[] ballIds, out Vector2[] ballPositions)
        {
            byte length = data[0];
            ballIds = new byte[length];
            ballPositions = new Vector2[length];
            for (int i = 0; i < length; i++)
            {
                int index = 1 + (1 + Vector2.ByteSize) * i;
                ballIds[i] = data[index];
                ballPositions[i] = DecodeVector2(data, index + 1);
            }
        }

        public static byte[] EncodeRoundOverData(BallType[] ballTypes, byte[] ballIds, byte[] playerIds, byte[] playerLifes)
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
        public static void DecodeRoundOverData(byte[] data, out BallType[] ballTypes, out byte[] ids, out byte[] playerIds, out byte[] playerLifes)
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
    }
}
