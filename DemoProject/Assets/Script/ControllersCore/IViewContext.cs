using System;
using System.Threading;
using System.Threading.Tasks;

namespace Script.ControllersCore
{
    public interface IViewContext : IDisposable
    {
        int Id { get; }

        Task ShowModalAsync(Func<Task> action, CancellationToken cancellationToken = default);

        Task<IViewContext> LockAsync(CancellationToken cancellationToken = default);

        IViewContext CreateChild();
    }
}