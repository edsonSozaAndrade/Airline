using Airport.Adapter;
using Airport.Enums;
using Airport.Models;
using Airport.Observers;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Services
{
    public class DisplayManager
    {
        private IAirlineFormatter formatter;

        public Task<List<FlightEvent>> GetFlyesByType(FlightType type)
        {
            List<FlightEvent> results = new List<FlightEvent>();
            using (var reader = new StreamReader("Data.csv"))
            using (var csv = new CsvReader(reader, CSVReaderConfig()))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var flyEvent = ReadFlyEvent(csv);
                    if (csv.GetField("FlightType") == "Arrival")
                    {
                        if (type == FlightType.Arrival)
                            results.Add(flyEvent);
                    }
                    else
                    {                         
                        if (type == FlightType.Departure)
                            results.Add(flyEvent);
                    }
                }
                return Task.FromResult(results.OrderBy(x=>x.Hour).ToList());
            }
        }

        public Task CreateNewEvent()
        {
            List<FlightEvent> results = new List<FlightEvent>();            
            using (var reader = new StreamReader("NewData.csv"))
            using (var csv = new CsvReader(reader, CSVReaderConfig()))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var flyEvent = ReadFlyEvent(csv);
                    if (csv.GetField("FlightType") == "Arrival")
                    {
                        flyEvent.Attach(new ArrivalObserver());
                        flyEvent.AddNewFlightEvent(flyEvent);
                    }
                    else
                    {
                        flyEvent.Attach(new DepartureObserver());
                        flyEvent.AddNewFlightEvent(flyEvent);
                    }
                }
                return Task.FromResult(results);
            }
        }

        public Task<Airline> GetAirlineInfo(FlightEvent fly)
        {
            var result = new Airline();
            if (fly.Airline.Name == "Tam")
            {
                formatter = new TamAdapter();                
            }
            else
            {
                formatter = new BoaAdapter();
            }
            return Task.FromResult(formatter.FormatAirlineInfo());
        }

        private CsvConfiguration CSVReaderConfig()
        {
            return  new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                Delimiter = ";"
            };
        }

        private FlightEvent ReadFlyEvent(CsvReader csv)
        {
            FlightEvent result;
            if (csv.GetField("FlightType") == "Arrival")
                result = new Arrival();
            else
                result = new Departure();

            result.Airline = new Airline();
            result.To = csv.GetField("To");
            result.From = csv.GetField("From");
            result.Hour = DateTime.Parse(csv.GetField("Hour"));
            result.StimatedHour = DateTime.Parse(csv.GetField("StimatedHour"));
            result.IdFlight = csv.GetField("IdFlight");
            result.Airline.Name = csv.GetField("AirlineName");
            result.Airline.PlaneModel = csv.GetField("AirlinePlaneModel");
            result.Airline.IcaoPlane = csv.GetField("AirlineIcaoPlane");
            result.Observation = csv.GetField("Observation");
            result.Remark = csv.GetField("Remark");
            result.Type = csv.GetField<FlightType>("FlightType");

            return result;
        }
    }
}
