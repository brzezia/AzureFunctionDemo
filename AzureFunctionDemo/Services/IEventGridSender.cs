using System.Threading.Tasks;
using AzureFunctionDemo.Model;

namespace AzureFunctionDemo.AzureGrid
{
    public interface IEventGridSender<T>
    {
        Task SendAsync(Car car);
    }

    public class EventGridSender<T> : IEventGridSender<T>
    {
        public Task SendAsync(Car car)
        {
            throw new System.NotImplementedException();
        }
    }


}