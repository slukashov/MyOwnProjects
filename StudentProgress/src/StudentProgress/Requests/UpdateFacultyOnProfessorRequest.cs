using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProgress.Requests
{
    public class UpdateFacultyOnProfessorRequest
    {
        public long FacultyId { get; set; }
        public long ProfessorId { get; set; }
    }
}
