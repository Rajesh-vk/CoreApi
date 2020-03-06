using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserServices.Controllers;
using Xunit;

namespace XUnitTestProject.UserServiceTest
{
    public class UserServiceTest
    {
        UserController _controller;
        IUserBL _service;

        public UserServiceTest()
        {
            _service = new UserTestBl();
            _controller = new UserController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<UserDetails>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get("user5");

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var eventId = "user1";

            // Act
            var okResult = _controller.Get(eventId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var eventId = "user1";

            // Act
            var okResult = _controller.Get(eventId).Result as OkObjectResult;

            // Assert
            Assert.IsType<UserDetails>(okResult.Value);
            Assert.Equal(eventId, (okResult.Value as UserDetails).Id);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = "user12";

            // Act
            var badResponse = _controller.Delete(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingId = "user1";

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = "user1";

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.Equal(2, _service.GetAll().Count());
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new UserDetails()
            {
                Id = "user1",
                Password = "sa1",
                EmailId = "school@gmail.com",
                UserRoleId = 1
            };
            _controller.ModelState.AddModelError("eventName", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            UserDetails testItem = new UserDetails()
            {
                Id = "user4",
                Username = "sa1",
                Password = "sa1",
                EmailId = "school@gmail.com",
                UserRoleId = 1
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new UserDetails()
            {
                Id = "user4",
                Username = "sa1",
                Password = "sa1",
                EmailId = "school@gmail.com",
                UserRoleId = 1
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as UserDetails;

            // Assert
            Assert.IsType<UserDetails>(item);
            Assert.Equal("user4", item.Id);
        }
    }
}
