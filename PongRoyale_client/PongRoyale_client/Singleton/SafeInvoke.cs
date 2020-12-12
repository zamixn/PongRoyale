using PongRoyale_shared;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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
        public CancellationTokenSource DelayedCancellableToken(float delay, Action action)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var d = TimeSpan.FromSeconds(delay);
            Task.Delay(d, token).ContinueWith(t => { if (!source.IsCancellationRequested) { action.Invoke(); } source.Dispose(); });
            return source;
        }
        public Task DelayedInvoke(float delay, Action action)
        {
            var d = TimeSpan.FromSeconds(delay);
            return Task.Delay(d).ContinueWith(t => action.Invoke());
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
