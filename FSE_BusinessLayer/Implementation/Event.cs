using FSE_API_MODEL;
using FSE_BusinessLayer.Inferface;
using FSE_DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_BusinessLayer.Implementation
{
    public class Event : IEvent
    {
        private IEventRepo _eventRepo;
        public Event(IEventRepo eventRepo)
        {
            _eventRepo = eventRepo;

        }
        public IEnumerable<EventDetails> GetAll()
        {
            return _eventRepo.GetAll();

        }

        public EventDetails GetById(string id)
        {
            return _eventRepo.GetById(id);

        }

        public void InsertEvent(EventDetails eventDetails)
        {
             _eventRepo.Insert(eventDetails);

        }
    }
}
