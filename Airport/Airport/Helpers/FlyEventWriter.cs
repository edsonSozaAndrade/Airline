using Airport.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Airport.Helpers
{
    public static class FlyEventWriter
    {
        public static void WriteEvent(FlightEvent newEvent)
        {
            var record = new
            {
                Hour = newEvent.Hour.ToString("dd/M/yyyy HH:mm"),
                StimatedHour = newEvent.StimatedHour.ToString("dd/M/yyyy HH:mm"),
                newEvent.IdFlight,
                AirlineName = newEvent.Airline.Name,
                AirlinePlaneModel = newEvent.Airline.PlaneModel,
                AirlineIcaoPlane = newEvent.Airline.IcaoPlane,
                FlightType = newEvent.Type,
                Observation = newEvent.Observation ?? string.Empty,
                Remark = newEvent.Remark ?? string.Empty,
                To = newEvent.To ?? string.Empty,
                From = newEvent.From ?? string.Empty
            };
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";"
            };
            using (var stream = File.Open("data.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.NextRecord();
                csv.WriteRecord(record);
            }
        }
    }
}
