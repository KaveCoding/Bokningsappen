using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class Rum
    {
        public int Id { get; set; }
        public int TV { get; set; }
        public int Bord { get; set; }
        public int Stolar { get; set; }
        public ICollection<Bokning> Bokningar { get; set; }
    }
}
