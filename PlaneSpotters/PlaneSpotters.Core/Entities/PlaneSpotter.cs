using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.Core.Entities
{
    public class PlaneSpotter : BaseEntity
    {
        public String Make { get; set; }
        public String Model { get; set; }
        public String Registration { get; set; }
        public String Location { get; set; }
        public DateTime DateTime { get; set; }
        public String ImageUrl { get; set; }
    }
}
