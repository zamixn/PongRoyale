using PongRoyale_client.Game.Command;
using PongRoyale_client.Game.Input.Command;
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
        private Dictionary<Keys, bool> InputBuffer;

        private Dictionary<Keys, MoveCommand> InputDownBindings;
        private Dictionary<Keys, MoveCommand> InputUpBindings;
        private List<MoveCommand> Commands;
        public InputManager()
        {
            Commands = new List<MoveCommand>();
            InputBuffer = new Dictionary<Keys, bool>()
            {
                { Keys.Left, false },
                { Keys.Right, false },
                { Keys.Up, false },
                { Keys.Down, false }
            };
            InputDownBindings = new Dictionary<Keys, MoveCommand>()
            {
                { Keys.Left, new MoveLeftCommand(InputBuffer) },
                { Keys.Right, new MoveRightCommand(InputBuffer) },
                { Keys.Up, new MoveUpCommand(InputBuffer) },
                { Keys.Down, new MoveDownCommand(InputBuffer) }
            };
            InputUpBindings = new Dictionary<Keys, MoveCommand>()
            {
                { Keys.Left, new UndoMoveLeftCommand(InputBuffer) },
                { Keys.Right, new UndoMoveRightCommand(InputBuffer) },
                { Keys.Up, new UndoMoveUpCommand(InputBuffer) },
                { Keys.Down, new UndoMoveDownCommand(InputBuffer) }
            };
        }

        public void UndoLastInput()
        {
            Commands[Commands.Count - 1].Undo();
            Commands.RemoveAt(Commands.Count - 1);
        }

        private void ExecuteInput(Keys keycode)
        {
            if (InputBuffer.ContainsKey(keycode))
            {
                InputDownBindings[keycode].Execute();
                Commands.Add(InputDownBindings[keycode]);
                if(Commands.Count > 100)
                {
                    Commands.RemoveAt(0);
                }
            }
        }

        private void UndoInput(Keys keycode)
        {
            if (InputBuffer.ContainsKey(keycode))
            {
                InputUpBindings[keycode].Execute();
                Commands.Add(InputDownBindings[keycode]);
                if (Commands.Count > 100)
                {
                    Commands.RemoveAt(0);
                }
            }
        }

        public bool IsKeyDown(Keys keycode)
        {
            if (InputBuffer.TryGetValue(keycode, out bool b))
                return b;
            return false;
        }


        public void OnKeyUp(Keys keycode)
        {
            UndoInput(keycode);
        }

        public void OnKeyDown(Keys keycode)
        {
            ExecuteInput(keycode);
        }
    }
}
