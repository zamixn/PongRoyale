using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_shared
{
    public interface INetworkDataConverter
    {
        byte[] EncodeString(string s);
        string DecodeString(byte[] b);

        byte[] EncodeFloat(float f);
        float DecodeFloat(byte[] b, int index = 0);

        byte[] EncodeInt(int i);
        int DecodeInt(byte[] b, int index = 0);

        byte[] EncodeColor(Color c);
        Color DecodeColor(byte[] data, int index = 0);

        byte[] EncodeVector(Vector2 v);
        Vector2 DecodeVector2(byte[] data, int index = 0);

        byte[] EncodeGameStartMessage(byte[] playerIds, byte[] paddleTypes, byte ballType, byte roomMasterId);
        void DecodeGameStartMessage(byte[] data, out byte[] playerIds, out PaddleType[] paddleTypes, out BallType ballType, out byte roomMasterId);

        byte[] EncodeBallData(byte[] ballIds, Vector2[] ballPositions);

        void DecodeBallData(byte[] data, out byte[] ballIds, out Vector2[] ballPositions);

        byte[] EncodeRoundOverData(BallType[] ballTypes, byte[] ballIds, byte[] playerIds, byte[] playerLifes);
        void DecodeRoundOverData(byte[] data, out BallType[] ballTypes, out byte[] ids, out byte[] playerIds, out byte[] playerLifes);

        byte[] EncodeObstacleData(byte id, float width, float height, float duration, float posX, float posY, byte type);

        void DecodeObstacleData(byte[] data, out byte id, out float width, out float height, out float duration, out float posX, out float posY, out byte type);

        byte[] EncodePowerupData(byte id, float radius, float duration, float posX, float posY, byte type);

        void DecodePowerupData(byte[] data, out byte id, out float radius, out float duration, out float posX, out float posY, out byte type);
    }
}
