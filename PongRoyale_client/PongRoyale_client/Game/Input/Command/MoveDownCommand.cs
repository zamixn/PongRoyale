﻿using PongRoyale_client.Game.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Game.Input.Command
{
    class MoveDownCommand : MoveCommand
    {
        public MoveDownCommand(Dictionary<Keys, bool> buffer) : base(buffer)
        {
        }

        public override void Execute()
        {
            inputBuffer[Keys.Down] = true;
        }

        public override void Undo()
        {
            inputBuffer[Keys.Down] = false;
        }
    }
}
