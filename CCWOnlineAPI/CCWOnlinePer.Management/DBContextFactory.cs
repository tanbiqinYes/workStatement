using CCWOnlineAPI.Management.Models;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace CCWOnlineAPI.Management
{
    /// <summary>
    /// 工厂
    /// </summary>
    public class DBContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            DbContext db = (DbContext)CallContext.GetData("ManageMentConnection");

            if (db == null)
            {
                db = new ApplicationDbContext();
                CallContext.SetData("ManageMentConnection", db);
            }
            return db;
        }
    }
}
