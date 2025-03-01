using System;
using System.Collections.Generic;
using EventEaseApp.Models;

namespace EventEaseApp.Services
{
    public class EventService
    {
        private readonly List<Event> _events;
        private readonly List<EventRegistration> _eventRegistrations;

        public EventService()
        {
            _events = new List<Event>();
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await Task.FromResult(_events);
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await Task.FromResult(_events.Find(e => e.Id == eventId));
        }

        public async void CreateEventAsync(Event newEvent)
        {
            if (newEvent == null)
            {
                throw new ArgumentNullException(nameof(newEvent));
            }
            _events.Add(newEvent);
        }

        public async void UpdateEventAsync(Event updatedEvent)
        {
            if (updatedEvent == null)
            {
                throw new ArgumentNullException(nameof(updatedEvent));
            }
            var eventToUpdate = _events.Find(e => e.Id == updatedEvent.Id);
            if (eventToUpdate != null)
            {
                eventToUpdate.Name = updatedEvent.Name;
                eventToUpdate.Date = updatedEvent.Date;
                eventToUpdate.Location = updatedEvent.Location;
            }
        }

        public async void RegisterForEventAsync(EventRegistration newRegistration)
        {
            if (newRegistration == null)
            {
                throw new ArgumentNullException(nameof(newRegistration));
            }
            _eventRegistrations.Add(newRegistration);
        }


        public async Task<bool> RemoveEventAsync(int eventId)
        {
            var eventToRemove = _events.Find(e => e.Id == eventId);
            if (eventToRemove != null)
            {
                _events.Remove(eventToRemove);
                return true;
            }
            return false;
        }
    }
}