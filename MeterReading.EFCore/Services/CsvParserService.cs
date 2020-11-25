using CsvHelper;
using MeterReading.Core.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MeterReading.Data.Services
{
    public class CsvParserService : ICsvParserService
    {
        List<Core.Entities.MeterReading> ICsvParserService.ReadCsvFile(IFormFile file)
        {
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = new List<Core.Entities.MeterReading>();
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        // TODO If more then 5 digits, what should I do.. Ask PO

                        // TODO : write unit test for testing special cases 
                        // case 1 : smaller than 5
                        // case 2 : greater than 5
                        // case 3 : not a number
                        // case 4 : exactly 5


                        string readValue = csv.GetField("MeterReadValue");
                        bool validValue = !string.IsNullOrEmpty(readValue) && readValue.All(char.IsDigit);

                        var record = new Core.Entities.MeterReading
                        {
                            AccountId = csv.GetField<int>("AccountId"),
                            MeterReadingDateTime = Convert.ToDateTime(csv.GetField("MeterReadingDateTime")),
                            MeterReadingValue = validValue ? readValue.PadLeft(5, '0') : "00000"
                        };
                        records.Add(record);
                    }
                    return records;
                }

            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }
    }
}
