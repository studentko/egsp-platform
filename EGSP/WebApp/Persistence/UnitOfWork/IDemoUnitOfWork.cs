using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IDemoUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        ICustomerTypeRepository CustomerTypeRepository { get; }
        ITicketRepository TicketRepository { get; }
        IBusStationRepository BusStationRepository { get; }
        IBusLineRepository BusLineRepository { get; }

        int Complete();
        void Dispose();
    }
}