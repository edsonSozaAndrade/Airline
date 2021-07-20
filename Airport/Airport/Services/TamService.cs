using Airport.Models;

namespace Airport.Services
{
    public class TamService
    {
        public TamAirline GetAirlineInfo()
        {
            return new TamAirline()
            {
                AirlineName = "Transporte Aereo Militar",
                Captain = "Juan Valdéz",
                Chasis = "ASQQW-Q12",
                IataPlane = "VV 2233",
                PlaneModelTam = "Boeing 777"
            };
        }
    }
}
