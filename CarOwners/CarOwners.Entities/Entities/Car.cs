using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarOwners.Entities.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Mark { get; set; }
        public CarType CarType { set; get; }
        public decimal Price { get; set; }
        public DateTime IssueYear { get; set; }

        public virtual ICollection<CarOwner> Owners { get; set; }
    }
}
