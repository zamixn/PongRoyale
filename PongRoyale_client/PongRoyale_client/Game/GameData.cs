﻿using PongRoyale_client.Game.ArenaObjects;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Paddles;
using PongRoyale_shared;
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
        public static readonly float DefaultBallSpeed = 1.25f;
        public static readonly float DefaultBallSize = 20f;


        public static readonly Dictionary<ArenaObjectType, Color> ObstacleColors = new Dictionary<ArenaObjectType, Color>()
        {
            { ArenaObjectType.NonPassable, Color.SandyBrown },
            { ArenaObjectType.Passable, Color.FromArgb(128, Color.SandyBrown) }
        };

        public static readonly Dictionary<ArenaObjectType, Color> PowerupColors = new Dictionary<ArenaObjectType, Color>()
        {
            { ArenaObjectType.NonPassable, Color.DarkGreen },
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
            StartDelay = 1.5f,

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
