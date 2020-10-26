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
        public Dictionary<byte, NetworkPlayer> Players { get; private set; }
        public BallType BallType { get; private set; }
        public NetworkPlayer RoomMaster { get; private set; }


        public void SetRoomSettings(byte[] playerIds, PaddleType[] playerPaddleTypes, BallType ballType, byte roomMasterId)
        {
            Players = new Dictionary<byte, NetworkPlayer>();
            for (int i = 0; i < playerIds.Length; i++)
            {
                NetworkPlayer p = new NetworkPlayer(playerIds[i], life: byte.MaxValue, playerPaddleTypes[i]);
                Players.Add(playerIds[i], p);
                if (playerIds[i] == roomMasterId)
                    RoomMaster = p;
            }
            BallType = ballType;
        }

        public byte GetPlayerWonId()
        {
            var alive = Players.Where(p => p.Value.Life > 0);
            var winner = alive.Count() > 0 ? alive.First().Value : Players.Values.First();
            return winner.Id;
        }
        public string GetPlayerWonName()
        {
            return Player.ConstructName(GetPlayerWonId());
        }
        public void SetPlayerWon(byte id)
        {
            foreach (var p in Players)
            {
                if(p.Key != id)
                    p.Value.SetLife(0);
            }
        }
    }
}
