using System.Windows.Forms;
using BookStore.Core.Interfaces;
using System.Threading.Tasks;

namespace BookStore.WindowsForms
{
    class UserInteraction : IUserInteraction
    {
        public Task ShowMessageAsync(string message)
        {
            return Task.Run(() => MessageBox.Show(message));
        }
    }
}
