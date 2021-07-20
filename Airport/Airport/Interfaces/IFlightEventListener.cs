using Airport.Models;

namespace Airport.Interfaces
{
    public interface IFlightEventListener
    {
        void New(FlightEvent newEvent);
        void Update(FlightEvent newEventData);
    }
}
