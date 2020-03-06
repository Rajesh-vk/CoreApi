using EventServices.Controllers;
using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestProject.EventServiceTest
{
    public class EventtestService
    {
        EventController _controller;
        IEvent _service;

        public EventtestService()
        {
            _service = new EventTestBL();
            _controller = new EventController(_service);
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
            var items = Assert.IsType<List<EventDetails>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get("event5");

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var eventId = "event1";

            // Act
            var okResult = _controller.Get(eventId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var eventId = "event1";

            // Act
            var okResult = _controller.Get(eventId).Result as OkObjectResult;

            // Assert
            Assert.IsType<EventDetails>(okResult.Value);
            Assert.Equal(eventId, (okResult.Value as EventDetails).Id);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = "event10";

            // Act
            var badResponse = _controller.Delete(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingId = "event1";

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = "event1";

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.Equal(2, _service.GetAll().Count());
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new EventDetails()
            {
                eventDescription = "OutReachEvent",
                beneficiaryName = "school",
                baseLocation = "",
                venueAddress = "",
                totalNoVolunteers = 10,
                totalTravelHours = 10,
                totalVolunteHours = 100,
                livesImpacted = 1000
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
            EventDetails testItem = new EventDetails()
            {
                Id = "event4",
                eventName = "Event School",
                eventDescription = "OutReachEvent",
                beneficiaryName = "school",
                baseLocation = "",
                venueAddress = "",
                totalNoVolunteers = 10,
                totalTravelHours = 10,
                totalVolunteHours = 100,
                livesImpacted = 1000
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
            var testItem = new EventDetails()
            {
                Id = "event4",
                eventName = "Event School",
                eventDescription = "OutReachEvent",
                beneficiaryName = "school",
                baseLocation = "",
                venueAddress = "",
                totalNoVolunteers = 10,
                totalTravelHours = 10,
                totalVolunteHours = 100,
                livesImpacted = 1000
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as EventDetails;

            // Assert
            Assert.IsType<EventDetails>(item);
            Assert.Equal("event4", item.Id);
        }
    }
}
