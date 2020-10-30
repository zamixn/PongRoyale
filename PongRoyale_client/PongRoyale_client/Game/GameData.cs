using PongRoyale_client.Game.ArenaObjects;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Paddles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public class GameData
    {
        public class PaddleSettings
        {
            public int Size;
            public float Speed;
            public float Thickness;
            public int Life;
        }

        public static readonly Dictionary<System.Type, PaddleSettings> PaddleSettingsDict =
            new Dictionary<System.Type, PaddleSettings>()
        {
            {typeof(NormalPaddle), new PaddleSettings(){
                Size = 20,
                Speed = 1.5f,
                Thickness = 10,
                Life = 3
            }},
            {typeof(LongPaddle), new PaddleSettings(){
                Size = 35,
                Speed = .8f,
                Thickness = 13,
                Life = 4
            }},
            {typeof(ShortPaddle), new PaddleSettings(){
                Size = 13,
                Speed = 2.25f,
                Thickness = 7,
                Life = 2
            }}
        };


        public static readonly float DefaultBallSpeed = 1.25f;
        public static readonly float DefaultBallSize = 20f;


        public static readonly Dictionary<ArenaObjectType, Color> ObstacleColors = new Dictionary<ArenaObjectType, Color>()
        {
            { ArenaObjectType.NonPassable, Color.DarkGray },
            { ArenaObjectType.Passable, Color.FromArgb(128, Color.DarkGreen) }
        };

        public static readonly ArenaObjectSpawnerParams ObstacleSpawnerParams = new ArenaObjectSpawnerParams()
        {
            StartDelay = 2.5f,

            MinInterval = 1.5f,
            MaxInterval = 2.5f,

            MinWidth = 50,
            MaxWidth = 120,

            MinHeight = 50,
            MaxHeight = 120,

            MinDuration = 1f,
            MaxDuration = 5f,
        };


        public static readonly ArenaObjectSpawnerParams PowerUpSpawnerParams = new ArenaObjectSpawnerParams()
        {
            StartDelay = 2.5f,

            MinInterval = 3f,
            MaxInterval = 6f,

            MinWidth = 50,
            MaxWidth = 50,

            MinHeight = 50,
            MaxHeight = 50,

            MinDuration = 5f,
            MaxDuration = 10f,
        };
    }
}
