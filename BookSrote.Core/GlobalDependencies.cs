using BookStore.Core.Interfaces;

namespace BookStore.Core
{
    public static class GlobalDependencies
    {
        public static IMainThreadDispatcherService MainThreadDispatcherService { get; set; }
        public static IUserInteraction UserInteraction { get; set; }
        public static IDataController DataController { get; set; }
    }
}
