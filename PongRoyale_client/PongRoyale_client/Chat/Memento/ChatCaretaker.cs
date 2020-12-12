using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Chat.Memento
{
    class ChatCaretaker
    {
        private List<ChatMemento> Mementos;

        public ChatCaretaker()
        {
            Mementos = new List<ChatMemento>();
        }

        public void SaveMemento(ChatMemento memento)
        {
            Mementos.Add(memento);
        }

        public void ClearMementos()
        { 
            Mementos.Clear();
        }

        public ChatMemento GetMemento()
        {
            int index = Mementos.Count - 1;
            var m = Mementos[index];
            Mementos.RemoveAt(index);
            Mementos.Insert(0, m);
            return m;
        }
    }
}
