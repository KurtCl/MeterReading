using MeterReading.Core.Entities;
using System.Collections.Generic;

namespace MeterReading.Core.Interface
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        IEnumerable<Account> GetAccounts();
    }
}
