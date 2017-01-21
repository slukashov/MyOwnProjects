using StudentProgress.Services.Interfaces;
using System.Threading.Tasks;
using StudentProgress.Requests;
using StudentProgress.Entities;
using StudentProgress.Repository.JournalSheetRepositories.Interfaces;
using StudentProgress.Repository.MarkRepositories.Interfaces;
using StudentProgress.Repository.LessonStudentRepository.Interfaces;
using StudentProgress.Repository.FinalMarkRepositories.Interfaces;
using System.Collections.Generic;
using StudentProgress.Repository.DisciplineRepositories.Interfaces;
using StudentProgress.Repository.GroupRepositories.Interfaces;
using StudentProgress.Repository.ProfessorRepositories.Interfaces;
using System.Linq;
using StudentProgress.Models;
using StudentProgress.Repository.LessonRepositories.Interfaces;
using StudentProgress.Repository.StudentRepositories.Interfaces;

namespace StudentProgress.Services
{
    internal class JournalSheetService : IJournalSheetService
    {
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IProfessorReadRepository professorReadRepository;
        private readonly IDisciplineReadRepository disciplineReadRepository;
        private readonly IJournalSheetWriteRepository journalSheetWriteRepository;
        private readonly IMarkWriteRepository markWriteRepository;
        private readonly ILessonStudentWriteRepository lessonStudentWriteRepository;
        private readonly IFinalMarkWriteRepository finalMarkWriteRepository;
        private readonly IFinalMarkReadRepository finalMarkReadRepository;
        private readonly IMarkReadRepository markReadRepository;
        private readonly IStudentReadRepository studentReadRepository;
        private readonly ILessonReadRepository lessonReadRepository;
        private readonly ILessonStudentReadRepository lessonStudentReadRepository;

        public JournalSheetService(IGroupReadRepository groupReadRepository,
            IProfessorReadRepository professorReadRepository,
            IDisciplineReadRepository disciplineReadRepository,
            IJournalSheetWriteRepository journalSheetWriteRepository,
            IMarkWriteRepository markWriteRepository,
            ILessonStudentWriteRepository lessonStudentWriteRepository,
            IFinalMarkWriteRepository finalMarkWriteRepository,
            IFinalMarkReadRepository finalMarkReadRepository,
            IMarkReadRepository markReadRepository,
            ILessonReadRepository lessonReadRepository,
            IStudentReadRepository studentReadRepository,
            ILessonStudentReadRepository lessonStudentReadRepository)
        {
            this.groupReadRepository = groupReadRepository;
            this.journalSheetWriteRepository = journalSheetWriteRepository;
            this.professorReadRepository = professorReadRepository;
            this.disciplineReadRepository = disciplineReadRepository;
            this.markWriteRepository = markWriteRepository;
            this.lessonStudentWriteRepository = lessonStudentWriteRepository;
            this.finalMarkWriteRepository = finalMarkWriteRepository;
            this.finalMarkReadRepository = finalMarkReadRepository;
            this.markReadRepository = markReadRepository;
            this.studentReadRepository = studentReadRepository;
            this.lessonReadRepository = lessonReadRepository;
            this.lessonStudentReadRepository = lessonStudentReadRepository;
        }

        public Task CreateJournalSheetAsync(CreateJournalSheetRequest createJournalSheetRequest)
        {
            return Task.Run(async () =>
            {
                var group = await groupReadRepository.GetGroupByIdAsync(createJournalSheetRequest.GroupId);
                var professor =
                    await professorReadRepository.GetProfessorByIdAsync(createJournalSheetRequest.ProfessorId);
                var discipline =
                    await disciplineReadRepository.GetDisciplineByIdAsync(createJournalSheetRequest.DisciplineId);
                var journalSheet = new JournalSheet(group, discipline, professor, createJournalSheetRequest.Semester);

                await journalSheetWriteRepository.AddAsync(journalSheet);
            });
        }

        public Task FillJournalSheetAsync(StudentModel[] model)
        {
            return Task.Run(async () =>
            {
                foreach (var student in model)
                {
                    foreach (var lesson in student.Lessons)
                    {
                        await markWriteRepository.AddOrModify(
                            new Mark
                            {
                                Rating = lesson.Mark,
                                JournalSheetId = lesson.JournalSheetId,
                                LessonId = lesson.LessonId,
                                StudentId = student.StudentId
                            });

                        await lessonStudentWriteRepository.AddOrModify(
                            new LessonStudent
                            {
                                StudentId = student.StudentId,
                                LessonId = lesson.LessonId,
                                Attending = lesson.Attending
                            });
                    }
                }
            });
        }

        public async Task FillAddFinalMarkAsync(FinalMarkRequest addFinalMarkRequest)
        {
            var students = addFinalMarkRequest.Students;
            var ratings = addFinalMarkRequest.Ratings;
            var marks =
                students.Select(
                    (student, index) =>
                        new FinalMark(ratings[index].Rating, addFinalMarkRequest.JournalSheetId, student.Id)).ToArray();
            await finalMarkWriteRepository.AddFinalMarksAsync(marks);
        }

        public Task<List<FinalMark>> GetAllFinalMarkAsync(FinalMarkRequest getFinalMarkRequest)
        {
            var students = getFinalMarkRequest.Students;

            var finalMarks = new List<FinalMark>();
            return Task.Run(async () =>
            {
                foreach (var item in students)
                {
                    var finalMark =
                        await
                            finalMarkReadRepository.GetStudentFinalMarksByCurrentDisciplineAsync(item.Id,
                                getFinalMarkRequest.JournalSheetId);
                    finalMarks.Add(finalMark);
                }
                return finalMarks;
            });
        }

        public async Task UpdateFinalMarksAsync(FinalMark[] updateFinalMarkRequest)
        {
            await finalMarkWriteRepository.UpdatedFinalMarksAsync(updateFinalMarkRequest);
        }

        public async Task<List<Mark[]>> GetAllMarksFromCurrentDisciplineAsync(GetMarkRequest getMarkRequest)
        {
            var lessons = getMarkRequest.Lessons;
            var listOfMarks = new List<Mark[]>();
            foreach (var lesson in lessons)
                listOfMarks.Add(
                    await
                        markReadRepository.GetStudentMarkByCurrentDisciplineAsync(lesson.Id,
                            getMarkRequest.JournalSheetId));
            return listOfMarks;
        }

        public async Task<StudentModel[]> ConfigureStudentModels(long groupId, long journaSheetId)
        {
            var students = (await studentReadRepository.GetStudentsFromCurrentGroupById(groupId)).ToList();
            var lessons = (await lessonReadRepository.GetLessonsFromCurrentJournalSheet(journaSheetId)).ToList();
            var array = students.Select(student => new StudentModel
            {
                StudentId = student.Id,
                Name = student.Account.Name,
                SurName = student.Account.SerName,
                GroupId = student.GroupId.Value.ToString(),
                Lessons = lessons.Select(
                     lesson =>
                          new LessonModel
                          {
                              JournalSheetId = journaSheetId,
                              LessonId = lesson.Id,
                              LessonDate = lesson.Date

                          }).ToList()
            }).ToArray();

            foreach (var student in array)
            {
                foreach (var lesson in student.Lessons)
                {
                    lesson.Mark =
                        (await
                            markReadRepository.GetMarkAsync(lesson.JournalSheetId, student.StudentId,
                                lesson.LessonId.Value)).Rating;
                    lesson.Attending =
                        (await
                            lessonStudentReadRepository.GetStudentAttengingAtLesson(lesson.LessonId.Value,
                                student.StudentId)).Attending;
                }
            }

            return array;
        }

    }
}
