using MeterReading.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace MeterReading.Data.Repositories
{
    public class AccountRepository : GenericRepository<Core.Entities.Account>, IAccountRepository
    {
        public AccountRepository(ApplicationContext context) : base(context)
        {
        }
        public IEnumerable<Core.Entities.Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }
    }
}
