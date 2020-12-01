using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Paddles
{
    public class PaddleSettings
    {
        public int Size;
        public float Speed;
        public float Thickness;
        public int Life;
        public IPaddleColor PaddleColor;
        public PaddleType PType;
    }

    public class PaddleDataFactory
    {
        static Dictionary<PaddleType, PaddleSettings> types = new Dictionary<PaddleType, PaddleSettings>();

        public static PaddleSettings GetPaddleData(PaddleType type)
        {
            PaddleSettings settings = null;
            if (types.ContainsKey(type))
            {
                settings = types[type];
            }
            else
            {
                switch (type)
                {
                    case PaddleType.Normal: settings = new PaddleSettings()
                    {
                        Size = 20,
                        Speed = 1.5f,
                        Thickness = 10,
                        Life = 3,
                        PaddleColor = new PaddleColorBlack(),
                        PType = PaddleType.Normal
                    };
                        break;
                    case PaddleType.Long: settings = new PaddleSettings()
                    {
                        Size = 35,
                        Speed = .8f,
                        Thickness = 13,
                        Life = 4,
                        PaddleColor = new PaddleColorBlack(),
                        PType = PaddleType.Long
                    };
                        break;
                    case PaddleType.Short: settings = new PaddleSettings()
                    {
                        Size = 13,
                        Speed = 2.25f,
                        Thickness = 7,
                        Life = 2,
                        PaddleColor = new PaddleColorRed(),
                        PType = PaddleType.Short
                    };
                        break;
                    default:
                        break;
                }
                types.Add(type, settings);
            }
            return settings;
        }
    }
}
