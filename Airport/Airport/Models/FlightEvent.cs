using Airport.Enums;
using Airport.Interfaces;
using System;
using System.Collections.Generic;

namespace Airport.Models
{
    public abstract class FlightEvent
    {
        public DateTime Hour { get; set; }
        public DateTime StimatedHour { get; set; }
        public string IdFlight { get; set; }
        public Airline Airline { get; set; }
        public Flight FlightDetail { get; set; }
        public FlightType Type { get; set; }
        public string Observation { get; set; }
        public string Remark { get; set; }
        public string To { get; set; }
        public string From { get; set; }

        private List<IFlightEventListener> _events = new List<IFlightEventListener>();

        public void Attach(IFlightEventListener flightEvent)
        {
            _events.Add(flightEvent);
        }

        public void Detach(IFlightEventListener flightEvent)
        {
            _events.Remove(flightEvent);
        }

        public void NotifyUpdate()
        {
            foreach (IFlightEventListener flight in _events)
            {
                flight.Update(this);
            }
        }

        public void NotifyNewEvent()
        {
            foreach (IFlightEventListener flight in _events)
            {
                flight.New(this);
            }
        }

        public void AddNewFlightEvent(FlightEvent newEvent)
        {
            this.Hour = newEvent.Hour;
            this.StimatedHour = newEvent.StimatedHour;
            this.IdFlight = newEvent.IdFlight;
            this.Airline = newEvent.Airline;
            this.FlightDetail = newEvent.FlightDetail;
            this.Type = newEvent.Type;
            if (newEvent.Type == FlightType.Arrival)
            {
                this.Remark = newEvent.Remark;
                this.From = newEvent.From;
            }
            else
            {
                this.Observation = newEvent.Observation;
                this.To = newEvent.To;
            }
            NotifyNewEvent();    
        }

        public void ModifyFlyEvent(FlightEvent updatedEvent)
        {
            this.StimatedHour = updatedEvent.StimatedHour;
            this.Observation = updatedEvent.Observation;
            this.Remark = updatedEvent.Remark;
            NotifyUpdate();
        }
    }
}
