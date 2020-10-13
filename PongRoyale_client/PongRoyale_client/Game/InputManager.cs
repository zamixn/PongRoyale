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
            { Keys.Right, false }
        };

        private void UpdateBuffer(Keys keycode, bool value)
        {
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
            switch (keycode)
            {
                case Keys.Left:
                case Keys.Right:
                    UpdateBuffer(keycode, false);
                    break;
            }
        }

        public void OnKeyDown(Keys keycode)
        {
            switch (keycode)
            {
                case Keys.Left:
                case Keys.Right:
                    UpdateBuffer(keycode, true);
                    break;
            }
        }
    }
}
