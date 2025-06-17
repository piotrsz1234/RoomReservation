using Microsoft.Extensions.Logging;

namespace RoomReservation.Implementation.Services
{
    public abstract class ServiceBase
    {
        protected ILogger Logger;

        protected ServiceBase(ILogger logger)
        {
            Logger = logger;
        }
    }
}