using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.Business.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mail.Contracts.Logics;

namespace Mail.Tests
{
    [TestFixture]
    public class LetterControllerTests
    {
        public Mock<IMemoryCache> mMemoryCache;
        public Mock<ILetterRepository> mLetterRepository;
        public Mock<ILogger<LetterService>> mLogger;
        public Mock<ILetterService> mLetterService;
        public Mock<ILetterLogics> mLetterLogics;
        public LetterService service;

        //[SetUp]
        //public void SetUp()
        //{
        //    this.mLogger = new Mock<ILogger<LetterService>>();
        //    this.mMemoryCache = new Mock<IMemoryCache>();
        //    this.mLetterRepository = new Mock<ILetterRepository>();
        //    this.mLetterService = new Mock<ILetterService>();
        //    this.mLetterLogics = new Mock<ILetterLogics>();
        //    this.service = new LetterService(mMemoryCache.Object, mLetterRepository.Object, mLogger.Object, mLetterLogics.Object);
        //}

        //[Test]
        //public void AddWithExpectException()
        //{
        //    Assert.Throws<ArgumentException>(() => service.SendLetter("", "", new long[] { }).GetAwaiter().GetResult());
        //    Assert.Throws<ArgumentNullException>(() => service.SendLetter("", "", null).GetAwaiter().GetResult());
        //}

        //[Test]
        //public void StatusMethodCallsTryGetValueByKey()
        //{
        //    //int value = 1;

        //    //mMemoryCache.Setup(ld => ld.TryGetValue(1, out int percentageСompletion))
        //    //    .Returns(true);

        //    //var result = service.Status();

        //    //Assert.Equals(result, value);
        //}

        //[Test]
        //public async Task GetDispatchesMethodCallsFindAllById()
        //{
        //    //var t1 = new LetterDto() { Id = 1, Status = true, UserId = 2, DepartureDate = DateTime.Now };
        //    //var time = new List<LetterDto> {t1};
        //    //mLetterRepository.Setup(repo => repo.FindAllById(1))
        //    //    .ReturnsAsync(time);

        //    //var result1 = await service.GetDispatches(1);
        //    //var result2 = await service.GetDispatches(2);
        //    //var dto1 = result1[0];

        //    //Assert.AreEqual(dto1.Id, t1.Id);
        //    //Assert.AreEqual(dto1.Status, t1.Status);
        //    //Assert.AreEqual(dto1.UserId, t1.UserId);
        //    //Assert.AreEqual(dto1.DepartureDate, t1.DepartureDate);
        //    //Assert.AreEqual(result2, null);
        //}

        //[Test]
        //public void GetDispatchesWithValueLessOneExpectException()
        //{
        //    Assert.Throws<ArgumentException>(() => service.GetDispatches(0).GetAwaiter().GetResult());
        //}
    }
}
//mLetterRepository.VerifyAdd()
//mLetterRepository.Verify();
