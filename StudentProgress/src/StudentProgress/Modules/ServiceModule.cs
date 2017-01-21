using Autofac;
using StudentProgress.Services.Interfaces;
using StudentProgress.Services;

namespace StudentProgress.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<FacultyService>().As<IFacultyService>();
            builder.RegisterType<StudentService>().As<IStudentService>();
            builder.RegisterType<ProfessorService>().As<IProfessorService>();
            builder.RegisterType<DisciplineService>().As<IDisciplineService>();
            builder.RegisterType<JournalSheetService>().As<IJournalSheetService>();
            builder.RegisterType<LessonService>().As<ILessonService>();
        }
    }
}
