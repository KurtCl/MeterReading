using CsvHelper.Configuration;
using MeterReading.Core.Models;

namespace MeterReading.Core.Mappers
{
    public sealed class MeterReadingMap : ClassMap<MeterReadingModel>
    {
        public MeterReadingMap()
        {
            Map(m => m.AccountId).Name("AccountId");
            Map(m => m.MeterReadingDateTime).Name("MeterReadingDateTime");
            Map(m => m.MeterReadingValue).Name("MeterReadValue");
        }
    }
}
