using System;

namespace ShortURL.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortKey { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}