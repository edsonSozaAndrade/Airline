using Airport.Helpers;
using Airport.Interfaces;
using Airport.Models;

namespace Airport.Observers
{
    public class DepartureObserver : IFlightEventListener
    {
        public void New(FlightEvent newEvent)
        {
            FlyEventWriter.WriteEvent(newEvent);
        }

        public void Update(FlightEvent newEventData)
        {
            if (newEventData.Type == Enums.FlightType.Departure)
            {
                //Insert into DB
            }
        }
    }
}
