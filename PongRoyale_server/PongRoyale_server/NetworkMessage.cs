using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_server
{
    public class NetworkMessage
    {
        public enum MessageType : byte
        {
            ConnectedToServer = 0,  // server sent only
            Chat = 1
        }

        public byte SenderId;
        public MessageType Type;
        public string Contents;

        public NetworkMessage(byte senderId, MessageType type, string contents)
        {
            SenderId = senderId;
            Type = type;
            Contents = contents;
        }

        public byte[] ToBytes()
        {
            int byteCount = Encoding.UTF8.GetByteCount(Contents + 1);
            byte[] byteData = new byte[byteCount];
            byteData = Encoding.ASCII.GetBytes(Contents);
            return byteData.PrependBytes(new byte[] { SenderId, (byte)Type });
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
            string messageContents = Encoding.UTF8.GetString(buffer, 2, count);
            NetworkMessage networkMessage = new NetworkMessage(senderId, (MessageType)messageType, messageContents);
            return networkMessage;
        }

        public bool ValidateSender()
        {
            return SenderId != 0;
        }
    }
}
