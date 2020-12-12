using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Chat.Memento
{
    class ChatOriginator
    {
        private string State;

        public ChatMemento CreateMemento()
        {
            return new ChatMemento(State);
        }

        public void RestoreState(ChatMemento memento)
        {
            memento.RestoreState(this);
        }

        public void SetState(string state)
        {
            State = state;
        }
        public string GetState()
        {
            return State;
        }
    }
}
