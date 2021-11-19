using AutoMapper;
using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DB;
using Mail.DTO.Models;
using Mail.Repository;
using Mail.WebApi.Controllers;
using Mail.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Mail.Tests
{
    public class DemoClassTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {

            //Arrange
            var mockDataContext = new Mock<DataContext>();
            var mockMapper = new Mock<IMapper>();
            //mockMapper.Setup(repo => repo.GetAll()).Returns(GetTestUsers());
            var repository = new UserRepository(mockDataContext.Object, mockMapper.Object);

            //Act
            var result = repository.FindAllUsersOnGroup(1);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<int[]>(viewResult.Model);
            Assert.Equal(model, new int[] { 1, 2 });

        }
        //private Task<List<UserDto>> GetTestUsers()
        //{
        //    var users = new List<UserDto>
        //    {
        //        new UserDto { Id=1, Name="Tom", Surname="Tom", Email="Tom"}
        //    };
        //    return users;
        //}
    }
}
