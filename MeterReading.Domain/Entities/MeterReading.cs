using System;
using System.ComponentModel.DataAnnotations;

namespace MeterReading.Core.Entities
{
    public class MeterReading
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadingValue { get; set; }
    }
}
