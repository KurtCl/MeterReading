using System.Collections.Generic;

namespace MeterReading.Core.Interface
{
    public interface IMeterReadingRepository : IGenericRepository<Entities.MeterReading>
    {
        IEnumerable<Entities.MeterReading> GetMeterReadings();
    }
}
