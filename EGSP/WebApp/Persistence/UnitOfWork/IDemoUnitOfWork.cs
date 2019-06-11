using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IDemoUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        ICustomerTypeRepository CustomerTypeRepository { get; }

        int Complete();
        void Dispose();
    }
}