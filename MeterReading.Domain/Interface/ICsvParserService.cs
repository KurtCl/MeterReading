using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MeterReading.Core.Interface
{
    public interface ICsvParserService
    {
        List<Entities.MeterReading> ReadCsvFile(IFormFile file);
    }
}
