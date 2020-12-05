using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client
{
    public class ArenaDimensions
    {
        public Vector2 Size { get; private set; }
        public Vector2 Center { get; private set; }
        public Vector2 RenderOrigin { get; private set; }
        public float Radius { get; private set; }

        public ArenaDimensions(Vector2 size, Vector2 center, Vector2 renderOrigin, float radius)
        {
            Size = size;
            Center = center;
            Radius = radius;
            RenderOrigin = renderOrigin;
        }


        public float GetDistanceFromCenter(Vector2 point)
        {
            return Vector2.Distance(Center, point);
        }
    }


}
