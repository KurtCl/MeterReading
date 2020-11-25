using MeterReading.Core.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeterReading.Data.Repositories
{
    public class MeterReadingRepository : GenericRepository<Core.Entities.MeterReading>, IMeterReadingRepository
    {
        public MeterReadingRepository(ApplicationContext context) : base(context)
        {
        }
        public IEnumerable<Core.Entities.MeterReading> GetMeterReadings()
        {
            return _context.Meterreadings.ToList();
        }
      }
}
