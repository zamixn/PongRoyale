using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    }
}
