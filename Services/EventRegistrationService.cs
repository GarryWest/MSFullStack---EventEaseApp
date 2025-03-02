using Blazored.LocalStorage;
using EventEaseApp.Models;
using EventEaseApp.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventRegistrationService
{
    private readonly ILocalStorageService _localStorage;
    private const string EventRegistrationsKey = "EventRegistrations";
    private const string EventKey = "Events";
    private List<Event> _events;
    private List<EventRegistration> _eventRegistrations;

    private EventService _eventService;

    public EventRegistrationService(ILocalStorageService localStorage, EventService eventService)
    {
        _localStorage = localStorage;
        _eventService = eventService;
    }

    public async Task SaveEventRegistrationAsync(EventRegistration newRegistration)
    {
        if (newRegistration == null)
        {
            throw new System.ArgumentNullException(nameof(newRegistration));
        }

        _events = await _localStorage.GetItemAsync<List<Event>>(EventKey) ?? new List<Event>();
        _eventRegistrations = await _localStorage.GetItemAsync<List<EventRegistration>>(EventRegistrationsKey) ?? new List<EventRegistration>();

        var relatedEvent = _events.FirstOrDefault(e => e.Id == newRegistration.EventId);

        if (relatedEvent == null)
        {
            Console.WriteLine($"EventRegistrationService.SaveEventRegistrationAsync: Event with ID {newRegistration.EventId} does not exist.");
            throw new System.InvalidOperationException("The related event does not exist.");
        }

        newRegistration.Event = relatedEvent;
        newRegistration.Id = _eventRegistrations.Count + 1;


        var registrations = await GetEventRegistrationsAsync() ?? new List<EventRegistration>();
        registrations.Add(newRegistration);
        await _localStorage.SetItemAsync(EventRegistrationsKey, registrations);
    }

    public async Task<List<EventRegistration>> GetEventRegistrationsAsync()
    {
        return await _localStorage.GetItemAsync<List<EventRegistration>>(EventRegistrationsKey);
    }
}
