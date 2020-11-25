using MeterReading.Core.Interface;
using MeterReading.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MeterReading.Data.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly ILogger<MeterReadingService> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly ICsvParserService _csvParserService;
        private readonly IMeterReadingRepository _meterReadingRepository;

        public MeterReadingService(ILogger<MeterReadingService> logger, IAccountRepository accountRepository, IMeterReadingRepository meterReadingRepository, ICsvParserService csvParserService)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _csvParserService = csvParserService;
            _meterReadingRepository = meterReadingRepository;
        }

        public ResultModel ReadMeterValues(IFormFile file)
        {
            int successful = 0;
            int failed = 0;

            var meterReadings = _csvParserService.ReadCsvFile(file);

            foreach (var meterReading in meterReadings)
            {
                // TODO : write unit test for testing special cases 
                // case 1 : test duplicates
                // case 2 : test account not found
                // case 3 : happy path


                var foundAccount = _accountRepository.Find(x => x.AccountId == meterReading.AccountId).FirstOrDefault();
                var foundReading = _meterReadingRepository.Find(x => x.AccountId == meterReading.AccountId & x.MeterReadingValue == meterReading.MeterReadingValue).FirstOrDefault();

                if (foundAccount != null && foundReading == null)
                {
                    successful += 1;
                    _meterReadingRepository.Add(meterReading);
                }
                else
                {
                    failed += 1;
                }
            }
            _meterReadingRepository.Save();

            _logger.LogInformation($"Failed entries {failed}");
            _logger.LogInformation($"Successful entries {successful}");

            return new ResultModel { Fails = failed, Succesful = successful };
        }
    }
}
