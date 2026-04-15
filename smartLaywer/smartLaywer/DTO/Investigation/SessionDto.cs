using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Investigation
{
    public class SessionDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Summary { get; set; }
        public List<string> Attendees { get; set; } = new();
        public string Decisions { get; set; }
    }
}
