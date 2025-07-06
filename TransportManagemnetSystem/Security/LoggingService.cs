using TransportManagemnetSystem.Data;

namespace TransportManagemnetSystem.Security
{

    public class LoggingService
    {
        private readonly TransportDbContext _context;

        public LoggingService(TransportDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string username, string action)
        {
            var log = new UserLog
            {
                Username = username,
                Action = action,
                Timestamp = DateTime.Now,
            };

            _context.UserLog.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}

