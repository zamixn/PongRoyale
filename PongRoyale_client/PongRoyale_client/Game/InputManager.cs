using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Game
{
    public class InputManager : Singleton<InputManager>
    {
        private Dictionary<Keys, bool> InputBuffer = new Dictionary<Keys, bool>()
        {
            { Keys.Left, false },
            { Keys.Right, false },
            { Keys.Up, false },
            { Keys.Down, false }
        };

        private void TryUpdateBuffer(Keys keycode, bool value)
        {
            if(InputBuffer.ContainsKey(keycode))
                InputBuffer[keycode] = value;
        }

        public bool IsKeyDown(Keys keycode)
        {
            if (InputBuffer.TryGetValue(keycode, out bool b))
                return b;
            return false;
        }


        public void OnKeyUp(Keys keycode)
        {
            TryUpdateBuffer(keycode, false);
        }

        public void OnKeyDown(Keys keycode)
        {
            TryUpdateBuffer(keycode, true);
        }
    }
}
