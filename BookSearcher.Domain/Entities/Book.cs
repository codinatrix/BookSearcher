using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSearcher.Domain.Entities
{
    public class Book
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }

        // I would prefer to represent price as a decimal for its precision and small range,
        // but requirements were to represent as double
        public double Price { get; set;  }
        public string Genre { get; set; }

        [JsonProperty("publish_date")]
        public DateTime Published { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
