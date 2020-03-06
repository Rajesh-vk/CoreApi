using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XUnitTestProject.EventServiceTest
{
    public class EventTestBL : IEvent
    {

        private readonly List<EventDetails> _eventDetailst;

        public EventTestBL()
        {
            _eventDetailst = new List<EventDetails>()
            {
                new EventDetails() {
                    Id = "event1",
                    eventName = "Event School",
                    eventDescription ="OutReachEvent",
                    beneficiaryName = "school",
                    baseLocation ="",
                    venueAddress="",
                    totalNoVolunteers=10,
                    totalTravelHours=10,
                    totalVolunteHours=100,
                    livesImpacted=1000
                },
                 new EventDetails() {
                    Id = "event2",
                    eventName = "Event School",
                    eventDescription ="OutReachEvent",
                    beneficiaryName = "school",
                    baseLocation ="",
                    venueAddress="",
                    totalNoVolunteers=10,
                    totalTravelHours=10,
                    totalVolunteHours=100,
                    livesImpacted=1000
                },
                  new EventDetails() {
                    Id = "event3",
                    eventName = "Event School",
                    eventDescription ="OutReachEvent",
                    beneficiaryName = "school",
                    baseLocation ="",
                    venueAddress="",
                    totalNoVolunteers=10,
                    totalTravelHours=10,
                    totalVolunteHours=100,
                    livesImpacted=1000
                },
            };
        }

        public IEnumerable<EventDetails> GetAll()
        {
            return _eventDetailst;
        }

        public void InsertEvent(EventDetails eventDetails)
        {
            eventDetails.Id = "event4";
            _eventDetailst.Add(eventDetails);
        }

        public EventDetails GetById(string id)
        {
            return _eventDetailst.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void DeleteEvent(string id)
        {
            var existing = _eventDetailst.First(a => a.Id == id);
            _eventDetailst.Remove(existing);
        }

        public void UpdateEvent(string id, EventDetails eventDetails)
        {

        }

    }
}
