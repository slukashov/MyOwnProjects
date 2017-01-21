using StudentProgress.Repository.DisciplineRepositories.Interfaces;
using StudentProgress.Requests;
using StudentProgress.Services.Interfaces;
using System.Threading.Tasks;

namespace StudentProgress.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineWriteRepository disciplineWriteRepository;
        private readonly IDisciplineReadRepository disciplineReadRepository;

        public DisciplineService(IDisciplineWriteRepository disciplineWriteRepository,
                                  IDisciplineReadRepository disciplineReadRepository)
        {
            this.disciplineWriteRepository = disciplineWriteRepository;
            this.disciplineReadRepository = disciplineReadRepository;
        }

        public async Task UpdateDiscipline(DisciplineRequest discipline)
        {
            var disciplineToUpdate = await disciplineReadRepository.GetDisciplineByIdAsync(discipline.Id);
            disciplineToUpdate.Name = discipline.NewNameOfDiscipline;
            await disciplineWriteRepository.UpdateAsync(disciplineToUpdate);
        }
    }
}
