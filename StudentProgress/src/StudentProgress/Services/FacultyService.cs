using StudentProgress.Repository.FacutiesRepositories.Interfaces;
using StudentProgress.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace StudentProgress.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyReadRepository facultyReadRepository;
        private readonly IFacultyWriteRepository facultyWriteRepository;

        public FacultyService(IFacultyReadRepository facultyReadRepository,
                                IFacultyWriteRepository facultyWriteRepository)
        {
            this.facultyReadRepository = facultyReadRepository;
            this.facultyWriteRepository = facultyWriteRepository;
        }

        public Task UpdateFacultyNameAsync(long facultyId, string name)
        {
                return Task.Run(async () =>
                {
                    var faculty = await facultyReadRepository.GetFacultyByIdAsync(facultyId);
                    faculty.Name = name;
                    await facultyWriteRepository.UpdateAsync(faculty);
                });
        }
    }
}
