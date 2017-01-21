using Autofac;
using Microsoft.EntityFrameworkCore;
using StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Identity;
using StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Identity.Interfaces;
using StudentProgress.Context;
using StudentProgress.Repository.AccountRepositories;
using StudentProgress.Repository.AccountRepositories.Interfaces;
using StudentProgress.Repository.DisciplineRepositories;
using StudentProgress.Repository.DisciplineRepositories.Interfaces;
using StudentProgress.Repository.FacutiesRepositories;
using StudentProgress.Repository.FacutiesRepositories.Interfaces;
using StudentProgress.Repository.FinalMarkRepositories;
using StudentProgress.Repository.FinalMarkRepositories.Interfaces;
using StudentProgress.Repository.GroupRepositories;
using StudentProgress.Repository.GroupRepositories.Interfaces;
using StudentProgress.Repository.JournalSheetRepositories;
using StudentProgress.Repository.JournalSheetRepositories.Interfaces;
using StudentProgress.Repository.LessonRepositories;
using StudentProgress.Repository.LessonRepositories.Interfaces;
using StudentProgress.Repository.LessonStudentRepository;
using StudentProgress.Repository.LessonStudentRepository.Interfaces;
using StudentProgress.Repository.MarkRepositories;
using StudentProgress.Repository.MarkRepositories.Interfaces;
using StudentProgress.Repository.ProfessorRepositories;
using StudentProgress.Repository.ProfessorRepositories.Interfaces;
using StudentProgress.Repository.StudentRepositories;
using StudentProgress.Repository.StudentRepositories.Interfaces;

namespace StudentProgress.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
               .RegisterType<StudentProgressContext>()
               .As<DbContext>()
               .InstancePerLifetimeScope();

            builder.RegisterType<AccountWriteRepository>().As<IAccountWriteRepository>();
            builder.RegisterType<AccountReadRepository>().As<IAccountReadRepository>();

            builder.RegisterType<StudentReadRepository>().As<IStudentReadRepository>();
            builder.RegisterType<StudentWriteRepository>().As<IStudentWriteRepository>();

            builder.RegisterType<ProfessorReadRepository>().As<IProfessorReadRepository>();
            builder.RegisterType<ProfessorWriteRepository>().As<IProfessorWriteRepository>();

            builder.RegisterType<GroupWriteRepository>().As<IGroupWriteRepository>();
            builder.RegisterType<GroupReadRepository>().As<IGroupReadRepository>();

            builder.RegisterType<FacultyWriteRepository>().As<IFacultyWriteRepository>();
            builder.RegisterType<FacultyReadRepository>().As<IFacultyReadRepository>();

            builder.RegisterType<DisciplineWriteRepository>().As<IDisciplineWriteRepository>();
            builder.RegisterType<DisciplineReadRepository>().As<IDisciplineReadRepository>();

            builder.RegisterType<JournalSheetWriteRepository>().As<IJournalSheetWriteRepository>();
            builder.RegisterType<JournalSheetReadRepository>().As<IJournalSheetReadRepository>();

            builder.RegisterType<LessonWriteRepository>().As<ILessonWriteRepository>();
            builder.RegisterType<LessonReadRepository>().As<ILessonReadRepository>();

            builder.RegisterType<LessonStudentWriteRepository>().As<ILessonStudentWriteRepository>();
            builder.RegisterType<LessonStudentReadRepository>().As<ILessonStudentReadRepository>();

            builder.RegisterType<MarkWriteRepository>().As<IMarkWriteRepository>();
            builder.RegisterType<MarkReadRepository>().As<IMarkReadRepository>();

            builder.RegisterType<MarkReadRepository>().As<IMarkReadRepository>();

            builder.RegisterType<FinalMarkWriteRepository>().As<IFinalMarkWriteRepository>();
            builder.RegisterType<FinalMarkReadRepository>().As<IFinalMarkReadRepository>();

            builder.RegisterType<UserReadRepository>().As<IUserReadRepository>();


        }


    }
}
