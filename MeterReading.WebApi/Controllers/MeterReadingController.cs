using MeterReading.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MeterReading.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        [HttpPost]
        [Route("upload")]
        public ActionResult PostFile(IFormFile uploadedFile)
        {
            return Ok(_meterReadingService.ReadMeterValues(uploadedFile));
        }
    }
}
