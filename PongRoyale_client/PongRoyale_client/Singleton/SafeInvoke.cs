using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace PongRoyale_client.Singleton
{
    public class SafeInvoke : Singleton<SafeInvoke>
    {
        private Control Control;

        public void Setup(Control control)
        {
            Control = control;
        }


        public void Invoke(Action action)
        {
            if (Control.InvokeRequired)
            {
                Control.Invoke(new MethodInvoker(() => action?.Invoke()));
            }
            else
            {
                action?.Invoke();
            }
        }
    }
}
