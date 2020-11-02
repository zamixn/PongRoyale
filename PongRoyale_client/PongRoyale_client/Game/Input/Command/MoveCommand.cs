using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Game.Command
{
    public abstract class MoveCommand
    {
        protected Dictionary<Keys, bool> inputBuffer;

        public MoveCommand(Dictionary<Keys, bool> buffer)
        {
            inputBuffer = buffer;
        }

        public abstract void Execute();
        public abstract void Undo();

    }
}
