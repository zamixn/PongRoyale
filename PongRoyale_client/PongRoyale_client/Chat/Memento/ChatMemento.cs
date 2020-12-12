using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Chat.Memento
{
    class ChatMemento
    {
        private string State;

        public ChatMemento(string state)
        {
            State = state;
        }

        public void RestoreState(ChatOriginator originator)
        {
            originator.SetState(State);
        }

        public override string ToString()
        {
            return State;
        }
    }
}
