using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PerformanceTricks.List
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Director { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public double Rating { get; set; }
    }
}
