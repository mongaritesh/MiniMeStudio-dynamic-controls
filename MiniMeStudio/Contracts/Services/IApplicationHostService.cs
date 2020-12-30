using System.Threading.Tasks;

namespace MiniMeStudio.Contracts.Services
{
    public interface IApplicationHostService
    {
        Task StartAsync();

        Task StopAsync();
    }
}
