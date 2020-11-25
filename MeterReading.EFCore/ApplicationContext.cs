using MeterReading.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace MeterReading.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Core.Entities.MeterReading> Meterreadings { get; set; }

    }
}
