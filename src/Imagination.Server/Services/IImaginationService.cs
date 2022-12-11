using System.IO;
using System.Threading.Tasks;

namespace Imagination.Services
{
    public interface IImaginationService
    {
        Task<CoversionResponse> Convert(Stream sourceStream);
    }
}
