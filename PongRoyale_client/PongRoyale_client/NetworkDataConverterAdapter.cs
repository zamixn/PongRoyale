﻿using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client
{
    public class NetworkDataConverterAdapter : Singleton<NetworkDataConverterAdapter>, INetworkDataConverter
    {
        private INetworkDataConverter Converter;

        public NetworkDataConverterAdapter()
        {
            Converter = new ByteNetworkDataConverter();
        }

        public NetworkDataConverterAdapter(INetworkDataConverter converter)
        {
            Converter = converter;
        }

        public void DecodeBallData(byte[] data, out byte[] ballIds, out Vector2[] ballPositions)
        {
            Converter.DecodeBallData(data, out ballIds, out ballPositions);
        }

        public Color DecodeColor(byte[] data, int index = 0)
        {
           return Converter.DecodeColor(data, index);
        }

        public float DecodeFloat(byte[] b, int index = 0)
        {
            return Converter.DecodeFloat(b, index);
        }

        public void DecodeGameStartMessage(byte[] data, out byte[] playerIds, out PaddleType[] paddleTypes, out BallType ballType, out byte roomMasterId)
        {
            Converter.DecodeGameStartMessage(data, out playerIds, out paddleTypes, out ballType, out roomMasterId);
        }

        public int DecodeInt(byte[] b, int index = 0)
        {
            return DecodeInt(b, index);
        }

        public void DecodeRoundOverData(byte[] data, out BallType[] ballTypes, out byte[] ids, out byte[] playerIds, out byte[] playerLifes)
        {
            Converter.DecodeRoundOverData(data, out ballTypes, out ids, out playerIds, out playerLifes);
        }

        public string DecodeString(byte[] b)
        {
            return Converter.DecodeString(b);
        }

        public Vector2 DecodeVector2(byte[] data, int index = 0)
        {
            return Converter.DecodeVector2(data, index);
        }

        public byte[] EncodeBallData(byte[] ballIds, Vector2[] ballPositions)
        {
            return Converter.EncodeBallData(ballIds, ballPositions);
        }

        public byte[] EncodeColor(Color c)
        {
            return Converter.EncodeColor(c);
        }

        public byte[] EncodeFloat(float f)
        {
            return Converter.EncodeFloat(f);
        }

        public byte[] EncodeGameStartMessage(byte[] playerIds, byte[] paddleTypes, byte ballType, byte roomMasterId)
        {
            return Converter.EncodeGameStartMessage(playerIds, paddleTypes, ballType, roomMasterId);
        }

        public byte[] EncodeInt(int i)
        {
            return Converter.EncodeInt(i);
        }

        public byte[] EncodeRoundOverData(BallType[] ballTypes, byte[] ballIds, byte[] playerIds, byte[] playerLifes)
        {
            return Converter.EncodeRoundOverData(ballTypes, ballIds, playerIds, playerLifes);
        }

        public byte[] EncodeString(string s)
        {
            return Converter.EncodeString(s);
        }

        public byte[] EncodeVector(Vector2 v)
        {
            return Converter.EncodeVector(v);
        }

        public byte[] EncodeObstacleData(byte id, float width, float height, float duration, float posX, float posY, byte type)
        {
            return Converter.EncodeObstacleData(id, width, height, duration, posX, posY, type);
        }
        public void DecodeObstacleData(byte[] data, out byte id, out float width, out float height, out float duration, out float posX, out float posY, out byte type)
        {
            Converter.DecodeObstacleData(data, out id, out width, out height, out duration, out posX, out posY, out type);
        }
        public byte[] EncodeObstacleData(byte id, Obstacle obstacle)
        {
            return Converter.EncodeObstacleData(id, obstacle.Width, obstacle.Heigth, obstacle.Duration, obstacle.PosX, obstacle.PosY, (byte)obstacle.Type);
        }
        public Obstacle DecodeObstacleData(byte[] data, out byte id)
        {
            Converter.DecodeObstacleData(data, out id, out float width, out float height, out float duration, out float posX, out float posY, out byte type);
            var obs = new Obstacle(posX, posY, duration, width, height);
            obs.SetTypeParams((ArenaObjectType)type);
            return obs;
        }

        public byte[] EncodePowerupData(byte id, float radius, float duration, float posX, float posY, byte type)
        {
            return Converter.EncodePowerupData(id, radius, duration, posX, posY, type);
        }

        public void DecodePowerupData(byte[] data, out byte id, out float radius, out float duration, out float posX, out float posY, out byte type)
        {
            Converter.DecodePowerupData(data, out id, out radius, out duration, out posX, out posY, out type);
        }
        public byte[] EncodePowerupData(byte id, Powerup pwoerup)
        {
            return Converter.EncodePowerupData(id, pwoerup.Radius, pwoerup.Duration, pwoerup.PosX, pwoerup.PosY, (byte)pwoerup.Type);
        }
        public Powerup DecodePowerupData(byte[] data, out byte id)
        {
            Converter.DecodePowerupData(data, out id, out float radius, out float duration, out float posX, out float posY, out byte type);
            var obs = new Powerup(duration, posX, posY,radius * 2f, radius * 2f, 
                false, false, false, false, false, false);
            obs.SetTypeParams((ArenaObjectType)type);
            return obs;
        }
    }
}
