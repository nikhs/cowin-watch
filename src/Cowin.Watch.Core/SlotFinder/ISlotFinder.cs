using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core
{
    public interface ISlotFinder
    {
        public Task<IEnumerable<Center>> FindBy(IFinderFilter finderFilter, CancellationToken none);
    }
}