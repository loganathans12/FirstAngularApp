using System;

namespace mlp.interviews.software.common
{

    public class BaseClass : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) { }
            disposed = true;
        }

        ~BaseClass()
        {
            Dispose(false);
        }
    }
}
