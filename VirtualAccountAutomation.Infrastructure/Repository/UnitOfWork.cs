using VirtualAccountAutomation.Core.Interfaces;
using VirtualAccountAutomation.Infrastructure.Interfaces;

namespace VirtualAccountAutomation.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IVirtualAccountRepository virtualAccountRepository)
        {
            VirtualAccounts = virtualAccountRepository;
        }

        public IVirtualAccountRepository VirtualAccounts { get; }
    }
}