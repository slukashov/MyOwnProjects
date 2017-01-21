using Microsoft.EntityFrameworkCore;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Context.Mappings;
using StudentProgress.Entities;

namespace StudentProgress.Context
{

    public class StudentProgressContext : DbContext
    {
        public StudentProgressContext(DbContextOptions<StudentProgressContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new AccountMapping().MapEntity(builder.Entity<Account>());
            new StudentMapping().MapEntity(builder.Entity<Student>());
            new ProfessorMapping().MapEntity(builder.Entity<Professor>());
            new DisciplineMapping().MapEntity(builder.Entity<Discipline>());
            new LessonMapping().MapEntity(builder.Entity<Lesson>());
            new FinalMarkMapping().MapEntity(builder.Entity<FinalMark>());
            new MarkMapping().MapEntity(builder.Entity<Mark>());
            new FacultyMapping().MapEntity(builder.Entity<Faculty>());
            new JournalSheetMapping().MapEntity(builder.Entity<JournalSheet>());
            new GroupMapping().MapEntity(builder.Entity<Group>());
            new LessonStudentMapping().MapEntity(builder.Entity<LessonStudent>());
            new IdentityRoleClaimMapping().MapEntity(builder.Entity<IdentityRoleClaim>());
            new IdentityRoleMapping().MapEntity(builder.Entity<IdentityRole>());
            new IdentityUserClaimMapping().MapEntity(builder.Entity<IdentityUserClaim>());
            new IdentityUserLoginMapping().MapEntity(builder.Entity<IdentityUserLogin>());
            new IdentityUserMapping().MapEntity(builder.Entity<IdentityUser>());
            new IdentityUserRoleMapping().MapEntity(builder.Entity<IdentityUserRole>());
            
            base.OnModelCreating(builder);
        }
    }
}
