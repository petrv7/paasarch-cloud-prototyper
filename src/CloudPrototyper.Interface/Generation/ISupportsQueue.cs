using System;

namespace CloudPrototyper.Interface.Generation
{
    public interface ISupportsQueue
    {
        public bool SupportsQueue(Type queue);
    }
}
