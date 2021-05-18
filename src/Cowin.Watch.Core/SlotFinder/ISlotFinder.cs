using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core
{
    public interface ISlotFinder
    {
        public Task<ICentersResponse> FindBy(IFinderFilter finderFilter, CancellationToken cancellationToken);
    }
}