using System;

namespace AppStore.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}
