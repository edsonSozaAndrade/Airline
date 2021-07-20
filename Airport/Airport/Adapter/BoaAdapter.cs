using Airport.Models;
using Airport.Services;

namespace Airport.Adapter
{
    public class BoaAdapter : IAirlineFormatter
    {
        public Airline FormatAirlineInfo()
        {
            Airline result = new Airline();
            BoaService service = new BoaService();
            var boaInfo = service.GetAirlineInfo();

            result.IcaoPlane = boaInfo.Icao;
            result.Name = boaInfo.Nombre;
            result.PlaneModel = boaInfo.Modelo;

            return result;
        }
    }
}
