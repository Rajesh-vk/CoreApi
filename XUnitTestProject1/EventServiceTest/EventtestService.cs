using EventServices.Controllers;
using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    }
}
