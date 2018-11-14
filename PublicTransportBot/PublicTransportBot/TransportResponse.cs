using System;

namespace PublicTransportBot
{
    
        public class TransportResponse
        {
            public Connection[] connections { get; set; }
            public From from { get; set; }
            public To to { get; set; }
            public Stations stations { get; set; }
        }

        public class From
        {
            public string id { get; set; }
            public string name { get; set; }
            public object score { get; set; }
            public Coordinate coordinate { get; set; }
            public object distance { get; set; }
        }

        public class Coordinate
        {
            public string type { get; set; }
            public float x { get; set; }
            public float y { get; set; }
        }

        public class To
        {
            public string id { get; set; }
            public string name { get; set; }
            public object score { get; set; }
            public Coordinate coordinate { get; set; }
            public object distance { get; set; }
        }


        public class Stations
        {
            public From[] from { get; set; }
            public To[] to { get; set; }
        }
        

        public class Connection
        {
            public From from { get; set; }
            public To to { get; set; }
            public string duration { get; set; }
            public int transfers { get; set; }
            public object service { get; set; }
            public string[] products { get; set; }
            public object capacity1st { get; set; }
            public object capacity2nd { get; set; }
            public Section[] sections { get; set; }
        }

       

        public class Station
        {
            public string id { get; set; }
            public string name { get; set; }
            public object score { get; set; }
            public Coordinate coordinate { get; set; }
            public object distance { get; set; }
        }
        

        public class Prognosis
        {
            public object platform { get; set; }
            public object arrival { get; set; }
            public DateTime? departure { get; set; }
            public object capacity1st { get; set; }
            public object capacity2nd { get; set; }
        }

        public class Location
        {
            public string id { get; set; }
            public string name { get; set; }
            public object score { get; set; }
            public Coordinate coordinate { get; set; }
            public object distance { get; set; }
        }
        

        public class Section
        {
            public Journey journey { get; set; }
            public object walk { get; set; }
            public Departure departure { get; set; }
            public Arrival arrival { get; set; }
        }

        public class Journey
        {
            public string name { get; set; }
            public string category { get; set; }
            public object subcategory { get; set; }
            public object categoryCode { get; set; }
            public string number { get; set; }
            public string _operator { get; set; }
            public string to { get; set; }
            public Passlist[] passList { get; set; }
            public object capacity1st { get; set; }
            public object capacity2nd { get; set; }
        }

        public class Passlist
        {
            public Station station { get; set; }
            public DateTime? arrival { get; set; }
            public int? arrivalTimestamp { get; set; }
            public DateTime? departure { get; set; }
            public int? departureTimestamp { get; set; }
            public int? delay { get; set; }
            public string platform { get; set; }
            public Prognosis prognosis { get; set; }
            public object realtimeAvailability { get; set; }
            public Location location { get; set; }
        }
        

        

        public class Departure
        {
            public Station station { get; set; }
            public object arrival { get; set; }
            public object arrivalTimestamp { get; set; }
            public DateTime departure { get; set; }
            public int departureTimestamp { get; set; }
            public int? delay { get; set; }
            public string platform { get; set; }
            public Prognosis prognosis { get; set; }
            public object realtimeAvailability { get; set; }
            public Location location { get; set; }
        }
        
        
        

        public class Arrival
        {
            public Station station { get; set; }
            public DateTime arrival { get; set; }
            public int arrivalTimestamp { get; set; }
            public object departure { get; set; }
            public object departureTimestamp { get; set; }
            public int? delay { get; set; }
            public string platform { get; set; }
            public Prognosis prognosis { get; set; }
            public object realtimeAvailability { get; set; }
            public Location location { get; set; }
        }
}
