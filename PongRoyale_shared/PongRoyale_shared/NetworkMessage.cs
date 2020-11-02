using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PongRoyale_shared
{
    public class NetworkMessage : IClonable<NetworkMessage>
    {
        public const int MAX_MESSAGE_BYTE_LENGTH = 64;
        public enum MessageType : byte
        {
            Invalid = 0,
            ConnectedToServer = 1,  // server sent only
            Chat = 2,
            PlayerSync = 3,
            BallSync = 4,
            GameStart = 5,
            GameEnd = 6,
            RoundReset = 7,
            ObstacleSpawned = 8,
            PowerupSpawned = 9,
            BallPoweredUp = 10,
            PaddlePowerUp = 11,
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

        public NetworkMessage ShallowClone()
        {
            return (NetworkMessage)this.MemberwiseClone();
        }

        public NetworkMessage DeepClone()
        {
            NetworkMessage clone = (NetworkMessage)this.MemberwiseClone();
            clone.SenderId = SenderId;
            clone.Type = Type;
            clone.ByteContents = ByteContents.ToArray();
            return clone;
        }

        public byte[] ToBytes()
        {
            var data = ByteContents.PrependBytes(new byte[] { SenderId, (byte)Type });
            if (NetworkMessage.MAX_MESSAGE_BYTE_LENGTH < data.Length)
                throw new Exception("Network message data limit reached");
            return data;
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
