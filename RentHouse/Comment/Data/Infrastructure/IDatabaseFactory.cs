using System;

namespace Comment.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        HouseContext Get();
    }
}