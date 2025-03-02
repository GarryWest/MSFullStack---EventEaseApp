using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventEaseApp.Models;

namespace EventEaseApp.Services
{
    public class EventService
    {
        private List<Event> _events;

        private ILocalStorageService _localStorage;
        private const string EventRegistrationsKey = "EventRegistrations";
        private const string EventKey = "Events";


        public EventService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        private async Task LoadEventsAsync()
        {
            _events = await _localStorage.GetItemAsync<List<Event>>(EventKey) ?? new List<Event>();
        }

        private async Task InitializeAsync()
        {
            await LoadEventsAsync();
        }



        // public async Task<EventService> InitializeAsync(ILocalStorageService localStorage)
        // {
        //     _localStorage = localStorage;
        //     _events = await _localStorage.GetItemAsync<List<Event>>(EventKey) ?? new List<Event>();
        //     return this;
        // }

        public async Task<List<Event>> GetEventsAsync()
        {
            _events = await _localStorage.GetItemAsync<List<Event>>(EventKey) ?? new List<Event>();
            return _events;
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return _events.Find(e => e.Id == eventId) ?? new Event();
        }

        public async Task CreateEventAsync(Event newEvent)
        {
            Console.WriteLine($"EventService.CreateEventAsync: {newEvent}");
            if (newEvent == null)
            {
                throw new ArgumentNullException(nameof(newEvent));
            }
            await LoadEventsAsync();
            newEvent.Id = _events.Count + 1;
            _events.Add(newEvent);
            await _localStorage.SetItemAsync(EventKey, _events);
        }

        public async Task UpdateEventAsync(Event updatedEvent)
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
            await _localStorage.SetItemAsync(EventKey, _events);

            //return Task.CompletedTask;
        }

        public async Task<bool> RemoveEventAsync(int eventId)
        {
            var eventToRemove = _events.Find(e => e.Id == eventId);
            if (eventToRemove != null)
            {
                _events.Remove(eventToRemove);
                await _localStorage.SetItemAsync(EventKey, _events);
                return true;
            }
            return false;
        }
    }
}
