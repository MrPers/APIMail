using Mail.Contracts.Repo;
using Mail.DTO.Models;
using Mail.Business.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Mail.Tests
{
    [TestFixture]
    public class GroupServiceTests
    {
        public Mock<IUserRepository> mUserRepository;
        public Mock<IGroupRepository> mGroupRepository;
        public Mock<GroupDto> mGroupDto;
        public GroupService groupService;
        
        [SetUp]
        public void SetUp()
        {
            this.mUserRepository = new Mock<IUserRepository>();
            this.mGroupRepository = new Mock<IGroupRepository>();
            this.mGroupDto = new Mock<GroupDto>();
            this.groupService = new GroupService(mUserRepository.Object, mGroupRepository.Object);
        }

        [Test]
        public void RegisterGroupWithNullGroupExpectException()
        {
            Assert.Throws<ArgumentNullException>(()=> groupService.RegisterAsync(null).GetAwaiter().GetResult());
        }

        [Test]
        public async Task GetAllCallsTryReturnsAsync()
        {
            var time = new List<GroupDto> {
                new GroupDto() { Id = 1, Name = "TestName" }, 
                new GroupDto() { Id = 2, Name = "Name2" }
            };
            
            mGroupRepository.Setup(ld => ld.GetAllAsync()).ReturnsAsync(time);

            CollectionAssert.AreEqual(time, await groupService.GetAllAsync());
        }

        [Test]
        public void DeleteWithIdLessThan0ExpectException([Range(-2,0)] int id)
        {
            Assert.Throws<ArgumentException>(() => groupService.DeleteAsync(id).GetAwaiter().GetResult());
        }

        [Test]
        public void UpdateWithNullGroupExpectException()
        {
            Assert.Throws<ArgumentNullException>(() => groupService.UpdateAsync(2, null).GetAwaiter().GetResult());
        }

        [Test]
        public void UpdateWithIdLessThan0ExpectException([Range(-2, 0)] int id)
        {
            Assert.Throws<ArgumentException>(() => groupService.UpdateAsync(id, new GroupDto()).GetAwaiter().GetResult());
        }
    }
}
