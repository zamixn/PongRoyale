using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public class RoomSettings : Singleton<RoomSettings>
    {
        public List<NetworkPlayer> Players { get; private set; }
        public BallType BallType { get; private set; }
        public NetworkPlayer RoomMaster { get; private set; }


        public void SetRoomSettings(byte[] playerIds, PaddleType[] playerPaddleTypes, BallType ballType, byte roomMasterId)
        {
            Players = new List<NetworkPlayer>();
            for (int i = 0; i < playerIds.Length; i++)
            {
                NetworkPlayer p = new NetworkPlayer(playerIds[i], playerPaddleTypes[i]);
                Players.Add(p);
                if (playerIds[i] == roomMasterId)
                    RoomMaster = p;
            }
            BallType = ballType;
            NextBallId = 0;
        }


        public static byte NextBallId = 0;
        public byte GetNextBallId()
        {
            return NextBallId++;
        }
    }
}
