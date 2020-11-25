using System;
using System.ComponentModel.DataAnnotations;

namespace MeterReading.Core.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
