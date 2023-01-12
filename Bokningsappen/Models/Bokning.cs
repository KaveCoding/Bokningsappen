using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class Bokning
    {
        public string Id { get; set; }
        public string? Namn { get; set; }
        public int? Antal_personer { get; set; }
        public int Rum { get; set; }
        public string Veckodag { get; set; }
        public int Veckonummer { get; set; }
    }
}
