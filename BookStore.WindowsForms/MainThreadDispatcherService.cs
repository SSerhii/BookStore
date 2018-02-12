using System;
using BookStore.Core.Interfaces;

namespace BookStore.WindowsForms
{
    public class MainThreadDispatcherService : IMainThreadDispatcherService
    {
        public void DispatchOnMainThread(Action action)
        {
            action.Invoke();
        }
    }
}
