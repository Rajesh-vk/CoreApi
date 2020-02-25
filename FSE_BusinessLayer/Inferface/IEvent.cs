using FSE_API_MODEL;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_BusinessLayer.Inferface
{
    public interface IEvent
    {
        IEnumerable<EventDetails> GetAll();
        EventDetails GetById(string id);
    }
}
