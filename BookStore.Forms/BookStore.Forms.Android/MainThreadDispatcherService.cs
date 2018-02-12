using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BookStore.Core.Interfaces;
using System.Threading;

namespace BookStore.Forms.Droid
{
    class MainThreadDispatcherService : IMainThreadDispatcherService
    {
        public void DispatchOnMainThread(Action action)
        {
            if (Application.SynchronizationContext == SynchronizationContext.Current)
            {
                action.Invoke();
            }
            else
            {
                Application.SynchronizationContext.Post(ignored => action.Invoke(), null);
            }
        }
    }
}