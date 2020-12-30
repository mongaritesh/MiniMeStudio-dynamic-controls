using System.Collections.Generic;
using System.Threading.Tasks;

using MiniMeStudio.Core.Models;

namespace MiniMeStudio.Core.Contracts.Services
{
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetMasterDetailDataAsync();
    }
}
