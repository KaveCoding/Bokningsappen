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
        public int Id { get; set; }
        public int Rum { get; set; }
        public string Veckodag { get; set; }
        public int Veckonummer { get; set; }
        public bool Tillgänglig { get; set; }
        public int? SällskapId { get; set; }


    }
}
