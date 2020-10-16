using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PongRoyale_shared
{
    public class NetworkMessage
    {
        public enum MessageType : byte
        {
            ConnectedToServer = 0,  // server sent only
            Chat = 1,
            playerSync = 2,
            GameStart = 3
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
        public static float DecodeFloat(byte[] b)
        {
            return BitConverter.ToSingle(b, 0);
        }

        public static byte[] EncodeGameStartMessage(byte[] playerIds, byte[] paddleTypes, byte ballType)
        {
            byte[] data = playerIds.AppendBytes(paddleTypes)
                          .PrependBytes(new byte[] { (byte)playerIds.Length, ballType });
            return data;
        }
        public static void DecodeGameStartMessage(byte[] data, out byte[] playerIds, out PaddleType[] paddleTypes, out BallType ballType)
        {
            int playerCount = data[0];
            playerIds = new byte[playerCount];
            paddleTypes = new PaddleType[playerCount];
            ballType = (BallType)data[1];
            for (int i = 0; i < playerCount; i++)
            {
                playerIds[i] = data[i + 2];
                paddleTypes[i] = (PaddleType)data[i + 2 + playerCount];
            }
        }
    }
}
