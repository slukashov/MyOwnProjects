using System;
using System.Collections.Generic;

namespace CarOwners.Entities.Entities
{
    public class CarOwner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float DrivingExperience { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
