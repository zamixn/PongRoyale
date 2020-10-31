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
            var data = NetworkDataConverterAdapter.Instance.EncodeBallData(
                    new byte[] { 0, 1, 2 },
                    new Vector2[] { new Vector2(17, 12), new Vector2(50, -60), new Vector2(-3.14f, 3.14f) },
                    new Vector2[] { new Vector2(0.1f, 0.9f), new Vector2(0, 1), new Vector2(1.23f, 0.15f) });
            NetworkDataConverterAdapter.Instance.DecodeBallData(data, out byte[] ids, out Vector2[] poss, out Vector2[] dirs);
        }
    }
}
