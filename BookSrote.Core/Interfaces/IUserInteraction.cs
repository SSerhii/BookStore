using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IUserInteraction
    {
        Task ShowMessageAsync(string message);
    }
}
