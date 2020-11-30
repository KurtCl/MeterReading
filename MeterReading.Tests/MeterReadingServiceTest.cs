using FluentAssertions;
using MeterReading.Core.Entities;
using MeterReading.Core.Interface;
using MeterReading.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace MeterReading.Tests
{
    public class MeterReadingServiceTest
    {
        private readonly Mock<ILogger<MeterReadingService>> loggerMock;
        private readonly Mock<IAccountRepository> accountRepositoryMock;
        private readonly Mock<ICsvParserService> csvParserServiceMock;
        private readonly Mock<IMeterReadingRepository> meterReadingRepositoryMock;

        public MeterReadingServiceTest()
        {
            loggerMock = new Mock<ILogger<MeterReadingService>>();
            accountRepositoryMock = new Mock<IAccountRepository>();
            csvParserServiceMock = new Mock<ICsvParserService>();
            meterReadingRepositoryMock = new Mock<IMeterReadingRepository>();
        }

        [Fact]
        public void TestDuplicates()
        {
            var accountId = 123;
            var accountId2 = 999;
            var meterReadingValue = "78945";

            csvParserServiceMock.Setup(x => x.ReadCsvFile(It.IsAny<IFormFile>())).Returns(new List<Core.Entities.MeterReading>() { new Core.Entities.MeterReading() { AccountId = accountId, MeterReadingValue = meterReadingValue }, new Core.Entities.MeterReading() { AccountId = accountId2, MeterReadingValue = "88888" }});
            accountRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>() { new Account() { AccountId = accountId }, new Account() { AccountId = accountId2 } });
            meterReadingRepositoryMock.SetupSequence(x => x.Find(It.IsAny<Expression<Func<Core.Entities.MeterReading, bool>>>()))
                    .Returns(new List<Core.Entities.MeterReading>() { new Core.Entities.MeterReading() { AccountId = accountId, MeterReadingValue = meterReadingValue, MeterReadingDateTime = DateTime.Now } })
                    .Returns(new List<Core.Entities.MeterReading>());

            var meterReadingService = new MeterReadingService(loggerMock.Object, accountRepositoryMock.Object, meterReadingRepositoryMock.Object, csvParserServiceMock.Object);
            var result = meterReadingService.ReadMeterValues(null);

            result.Succesful.Should().Be(1);
            result.Fails.Should().Be(1);
        }

        [Fact]
        public void TestAccountNotFound()
        {
            var accountId = 123;
            var accountId2 = 999;
            var meterReadingValue = "78945";

            csvParserServiceMock.Setup(x => x.ReadCsvFile(It.IsAny<IFormFile>())).Returns(new List<Core.Entities.MeterReading>() { new Core.Entities.MeterReading() { AccountId = accountId, MeterReadingValue = meterReadingValue }, new Core.Entities.MeterReading() { AccountId = accountId2, MeterReadingValue = "88888" } });
            accountRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>());
            meterReadingRepositoryMock.SetupSequence(x => x.Find(It.IsAny<Expression<Func<Core.Entities.MeterReading, bool>>>()))
                    .Returns(new List<Core.Entities.MeterReading>() { new Core.Entities.MeterReading() { AccountId = accountId, MeterReadingValue = meterReadingValue, MeterReadingDateTime = DateTime.Now } })
                    .Returns(new List<Core.Entities.MeterReading>());

            var meterReadingService = new MeterReadingService(loggerMock.Object, accountRepositoryMock.Object, meterReadingRepositoryMock.Object, csvParserServiceMock.Object);
            var result = meterReadingService.ReadMeterValues(null);

            result.Fails.Should().Be(2);
        }

        [Fact]
        public void TestHappyPath()
        {
            var accountId = 123;
            var accountId2 = 999;
            var meterReadingValue = "78945";

            csvParserServiceMock.Setup(x => x.ReadCsvFile(It.IsAny<IFormFile>())).Returns(new List<Core.Entities.MeterReading>() { new Core.Entities.MeterReading() { AccountId = accountId, MeterReadingValue = meterReadingValue }, new Core.Entities.MeterReading() { AccountId = accountId2, MeterReadingValue = "88888" } });
            accountRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>() { new Account() { AccountId = accountId }, new Account() { AccountId = accountId2 } });
            meterReadingRepositoryMock.SetupSequence(x => x.Find(It.IsAny<Expression<Func<Core.Entities.MeterReading, bool>>>()))
                    .Returns(new List<Core.Entities.MeterReading>())
                    .Returns(new List<Core.Entities.MeterReading>());

            var meterReadingService = new MeterReadingService(loggerMock.Object, accountRepositoryMock.Object, meterReadingRepositoryMock.Object, csvParserServiceMock.Object);
            var result = meterReadingService.ReadMeterValues(null);

            result.Succesful.Should().Be(2);
        }

    }
}
