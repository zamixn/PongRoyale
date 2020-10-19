using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared
{
    public interface IClonable<T>
    {
        T Clone();
    }
}
