using CsvHelper.Configuration.Attributes;
using System;

namespace MeterReading.Core.Models
{
    public class MeterReadingModel
    {
        [Name("AccountId")]
        public int AccountId { get; set; }

        [Name("MeterReadingDateTime")]
        public DateTime MeterReadingDateTime { get; set; }

        [Name("MeterReadingValue")]
        public string MeterReadingValue { get; set; }
    }
}
