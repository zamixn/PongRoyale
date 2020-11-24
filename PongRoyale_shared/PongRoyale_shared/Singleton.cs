using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_shared
{
    public abstract class Singleton<T> where T : new()
    {
        private static readonly T instance = new T();


        public static T Instance { 
            get {
                return instance;
            } 
        }
    }
}
