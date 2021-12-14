using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Models
{
    public class UrlStatistics
    {
        public Guid Id { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public DateTime Created { get; set; }
        public Url Url { get; set; }
    }
}
