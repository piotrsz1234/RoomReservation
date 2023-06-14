using Microsoft.Extensions.Logging;

namespace RoomReservation.Domain {
    public static class Extensions {

        public static void LogError(this ILogger logger, Exception e)
        {
            logger.LogError(e, "{Message}\n{StackTrace}", e.Message, e.StackTrace);
        }
        
    }
}