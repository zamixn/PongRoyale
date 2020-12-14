using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Mediator
{
    public class ConcreteMediator : IAbstractMediator
    {
        private ArenaFacade arenaFacade;
        private ServerConnection serverConnection;
        public ConcreteMediator()
        {
            arenaFacade = ArenaFacade.Instance;
            serverConnection = ServerConnection.Instance;
        }
        public void Notify(string message, object data)
        {
            if (message == "SendEndGameMessages")
                serverConnection.SendEndGameMessage();
            else if (message == "SendEndGameMessages")
                serverConnection.SendEndGameMessage();
            else if (message == "SendRoundReset")
            {
                object[] receivedData = (object[])data;
                serverConnection.SendRoundReset(receivedData[0] as BallType[], receivedData[1] as byte[]);
            }
            else if (message == "SendPowerUpToPaddle")
            {
                object[] receivedData = (object[])data;
                serverConnection.SendTranferPowerUpToPaddle((byte)receivedData[0], (byte)receivedData[1], receivedData[2] as PoweredUpData);
            }
            else if (message == "SendBallPoweredUpMessage")
            {
                object[] receivedData = (object[])data;
                serverConnection.SendBallPoweredUpMessage((byte)receivedData[0], (byte)receivedData[1], receivedData[2] as PoweredUpData);
            }
            else if (message == "SendObstacleSpawnedMessage")
            {
                object[] receivedData = (object[])data;
                serverConnection.SendObstacleSpawnedMessage((byte)receivedData[0], receivedData[1] as Obstacle);
            }
            else if (message == "SendPowerupSpawnedMessage")
            {
                object[] receivedData = (object[])data;
                serverConnection.SendPowerupSpawnedMessage((byte)receivedData[0], receivedData[1] as PowerUp);
            }
            else if (message == "PlayerSyncMessageReceived")
            {
                arenaFacade.PlayerSyncMessageReceived(data as NetworkMessage);
            }
            else if (message == "BallSyncMessageReceived")
            {
                arenaFacade.BallSyncMessageReceived(data as NetworkMessage);
            }
            else if (message == "ResetRoundMessageReceived")
            {
                object[] receivedData = (object[])data;
                arenaFacade.ResetRoundMessageReceived(receivedData[0] as BallType[], receivedData[1] as byte[], receivedData[2] as byte[], receivedData[3] as byte[]);
            }
            else if (message == "ObstacleSpawnedMessageReceived")
            {
                object[] receivedData = (object[])data;
                arenaFacade.ObstacleSpawnedMessageReceived((byte)receivedData[0], receivedData[1] as Obstacle);
            }
            else if (message == "PowerUpSpawnedMessageReceived")
            {
                object[] receivedData = (object[])data;
                arenaFacade.PowerUpSpawnedMessageReceived((byte)receivedData[0], receivedData[1] as PowerUp, receivedData[2] as PoweredUpData);
            }
            else if (message == "OnReceivedBallPowerUpMessage")
            {
                object[] receivedData = (object[])data;
                arenaFacade.OnReceivedBallPowerUpMessage((byte)receivedData[0], (byte)receivedData[1], receivedData[2] as PoweredUpData);
            }
            else if (message == "OnReceivedTransferPowerUpMessage")
            {
                object[] receivedData = (object[])data;
                arenaFacade.OnReceivedTransferPowerUpMessage((byte)receivedData[0], (byte)receivedData[1], receivedData[2] as PoweredUpData);
            }
            else
            {
                throw new Exception("BadMessageSent");
            }
        }

        public bool GetBool(string message, object data)
        {
            if (message == "IsRoomMaster")
            {
                return serverConnection.IsRoomMaster;
            }
            else if(message == "IsConnected")
            {
                return serverConnection.IsConnected();
            }
            else if (message == "IdMatches")
            {
                return serverConnection.IdMatches((byte)data);
            }
            else
            {
                throw new Exception("BadMessageSent");
            }
        }

        public Paddle PaddleGetter(string message, object data)
        {
            if (message == "GetPaddle")
            {
                return ArenaFacade.Instance.LocalPaddle;
            }
            else
            {
                throw new Exception("BadMessageSent");
            }
        }

        public Dictionary<byte, IBall> BallGetter(string message, object data)
        {
            if (message == "GetArenaBalls")
            {
                return ArenaFacade.Instance.ArenaBalls;
            }
            else
            {
                throw new Exception("BadMessageSent");
            }
        }
    }
}
