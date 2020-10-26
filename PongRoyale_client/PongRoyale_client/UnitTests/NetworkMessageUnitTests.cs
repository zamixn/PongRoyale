using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.UnitTests
{
    public static class NetworkMessageUnitTests
    {

        public static void TestBallSyncEncodingAndDecoding()
        {
            var data = NetworkMessage.EncodeBallData(
                    new byte[] { 0, 1, 2 },
                    new Vector2[] { new Vector2(17, 12), new Vector2(50, -60), new Vector2(-3.14f, 3.14f) });
            NetworkMessage.DecodeBallData(data, out byte[] ids, out Vector2[] poss);
        }
    }
}
