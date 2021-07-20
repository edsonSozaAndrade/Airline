using Airport.Models;

namespace Airport.Services
{
    public class BoaService
    {
        public BoaAirline GetAirlineInfo()
        {
            return new BoaAirline()
            {
                Nombre = "Boliviana de Aviación",
                Iata = "QAA 4587",
                Icao = "QW 7888",
                Modelo = "Boeing 747",
                NumeroSerie = "1248798465-AASF-QWQW"
            };
        }
    }
}
