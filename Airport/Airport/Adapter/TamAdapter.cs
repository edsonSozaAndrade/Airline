using Airport.Models;
using Airport.Services;

namespace Airport.Adapter
{
    public class TamAdapter : IAirlineFormatter
    {
        public Airline FormatAirlineInfo()
        {
            Airline result = new Airline();
            TamService service = new TamService();
            var tamInfo = service.GetAirlineInfo();

            result.IcaoPlane = tamInfo.IataPlane;
            result.Name = tamInfo.AirlineName;
            result.PlaneModel = tamInfo.PlaneModelTam;

            return result;
        }
    }
}
