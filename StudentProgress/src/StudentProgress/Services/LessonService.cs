using System;
using StudentProgress.Services.Interfaces;
using System.Threading.Tasks;
using StudentProgress.Requests;
using StudentProgress.Repository.LessonRepositories.Interfaces;
using StudentProgress.Entities;
using StudentProgress.Repository.JournalSheetRepositories.Interfaces;
using StudentProgress.Repository.LessonStudentRepository.Interfaces;
using StudentProgress.Repository.MarkRepositories.Interfaces;
namespace StudentProgress.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonWriteRepository lessonWriteRepository;
        private readonly IJournalSheetReadRepository journalSheetReadRepository;
        private readonly ILessonStudentWriteRepository lessonStudentWriteRepository;
        private readonly IMarkWriteRepository markWriteRepository;

        public LessonService(ILessonWriteRepository lessonWriteRepository,
                             IJournalSheetReadRepository journalSheetReadRepository,
                             ILessonStudentWriteRepository lessonStudentWriteRepository,
                             IMarkWriteRepository markWriteRepository)
        {
            this.lessonWriteRepository = lessonWriteRepository;
            this.journalSheetReadRepository = journalSheetReadRepository;
            this.lessonStudentWriteRepository = lessonStudentWriteRepository;
            this.markWriteRepository = markWriteRepository;
        }

        public Task CreateLesson(LessonRequest lessonRequest)
        {
            return Task.Run(async () =>
            {
                var journalSheet = await journalSheetReadRepository.GetJournalSheetByIdAsync(lessonRequest.JournalSheetId);
                var lesson = new Lesson(lessonRequest.Date, journalSheet);
                await lessonWriteRepository.AddAsync(lesson);

                foreach (var student in lessonRequest.StudentModels)
                {
                    await
                        markWriteRepository.AddMarkAsync(new Mark
                        {
                            JournalSheetId = lessonRequest.JournalSheetId,
                            StudentId = student.StudentId,
                            LessonId = lesson.Id,
                            Rating = 0
                        });
                    await
                        lessonStudentWriteRepository.AddAsync(new LessonStudent(student.StudentId, lesson.Id, false));

                }
            });
        }

    }
}
