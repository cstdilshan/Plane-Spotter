using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.Core.Model
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public Guid InternalId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public String CreatedBy { get; set; }
        public String UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
