using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class Sällskap
    {
        public int Id { get; set; }
        public  string Namn { get; set; }
        public int Antal_i_sällskapet { get; set; }
        public string Aktivitet { get; set; }
        public ICollection<Bokning> Bokningar { get; set; }
}
}
