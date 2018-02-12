using System;
using System.Windows.Forms;
using BookStore.Core.ViewModel;
using BookStore.Core;

namespace BookStore.WindowsForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalDependencies.MainThreadDispatcherService = new MainThreadDispatcherService();
            GlobalDependencies.UserInteraction = new UserInteraction();
            GlobalDependencies.DataController = new LocalFileDataController();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BookStoreView(new BookStoreViewModel()));
        }
    }
}
