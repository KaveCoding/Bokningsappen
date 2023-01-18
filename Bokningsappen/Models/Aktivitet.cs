using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class Aktivitet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sällskap> Sällskaper { get; set; }
    }
}
