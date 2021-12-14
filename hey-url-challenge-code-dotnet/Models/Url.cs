using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public int Count { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime Created { get; set; }
        
        [NotMapped]
        public string CreatedFormatted
        {
            get { return Created.ToString("MMM dd, yyyy"); }
        }
    }
}
