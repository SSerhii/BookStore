using System;

namespace BookStore.Core.Interfaces
{
    public interface IMainThreadDispatcherService
    {
        void DispatchOnMainThread(Action action);
    }
}
