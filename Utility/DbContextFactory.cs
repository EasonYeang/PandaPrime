using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Utility
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            DbContext dbContext = CallContext.GetData("DbContext") as DbContext;

            if (dbContext == null)
            {
                dbContext = new PrimeContext();
                CallContext.SetData("DbContext", dbContext);
            }

            return dbContext;
        }
    }
}
