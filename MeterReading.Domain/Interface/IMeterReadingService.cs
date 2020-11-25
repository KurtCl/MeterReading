using MeterReading.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MeterReading.Core.Interface
{
    public interface IMeterReadingService
    {
        ResultModel ReadMeterValues(IFormFile uploadedFile);
    }
}
