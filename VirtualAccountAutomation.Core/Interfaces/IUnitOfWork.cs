using VirtualAccountAutomation.Infrastructure.Interfaces;

namespace VirtualAccountAutomation.Core.Interfaces
{
    public interface IUnitOfWork
    {
         IVirtualAccountRepository VirtualAccounts { get; }
    }
}