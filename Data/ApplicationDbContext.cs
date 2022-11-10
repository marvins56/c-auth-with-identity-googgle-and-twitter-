
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STMIS.Models;

namespace STMIS.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ): base(options)
        {

        }
        public DbSet<AppUser> AppUser { get; set; }

        public virtual DbSet<ClassTable> ClassTables { get; set; }
        public virtual DbSet<EndOftermMarksTable> EndOftermMarksTables { get; set; }

        public virtual DbSet<MidtermMarksTable> MidtermMarksTables { get; set; }

        public virtual DbSet<StudentsTable> StudentsTables { get; set; }

        public virtual DbSet<SubTopicsTable> SubTopicsTables { get; set; }

        public virtual DbSet<SubjectTable> SubjectTables { get; set; }

        public virtual DbSet<TopicsTable> TopicsTables { get; set; }

        public virtual DbSet<WeeksTable> WeeksTables { get; set; }

      /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=MARVIN\\SQLEXPRESS;Initial Catalog=STMIS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClassTable>(entity =>
            {
                entity.HasKey(e => e.ClassId).HasName("PK__tmp_ms_x__B096396FFB6A633A");
            });

            modelBuilder.Entity<EndOftermMarksTable>(entity =>
            {
                entity.HasKey(e => e.MarksId).HasName("PK__EndOfter__AF29F1A92EBCAFED");

                entity.HasOne(d => d.Class).WithMany(p => p.EndOftermMarksTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_classstudentEOT");

                entity.HasOne(d => d.Student).WithMany(p => p.EndOftermMarksTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_studentmarksEOT");

                entity.HasOne(d => d.Subject).WithMany(p => p.EndOftermMarksTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_subjectstudentEOT");
            });

            modelBuilder.Entity<MidtermMarksTable>(entity =>
            {
                entity.HasKey(e => e.MarksId).HasName("PK__MidtermM__AF29F1A989497CA5");

                entity.HasOne(d => d.Class).WithMany(p => p.MidtermMarksTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_classstudent");

                entity.HasOne(d => d.Student).WithMany(p => p.MidtermMarksTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_studentmarks");

                entity.HasOne(d => d.Subject).WithMany(p => p.MidtermMarksTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_subjectstudent");
            });

            modelBuilder.Entity<StudentsTable>(entity =>
            {
                entity.HasKey(e => e.StudentId).HasName("PK__Students__A2F7EDF44F705171");

                entity.HasOne(d => d.Class).WithMany(p => p.StudentsTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_classtable");
            });

            modelBuilder.Entity<SubTopicsTable>(entity =>
            {
                entity.HasKey(e => e.SubTopicsId).HasName("PK__SubTopic__E32A7C73550346F0");

                entity.HasOne(d => d.Class).WithMany(p => p.SubTopicsTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_classstudent1");

                entity.HasOne(d => d.Topic).WithMany(p => p.SubTopicsTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Topic");
            });

            modelBuilder.Entity<SubjectTable>(entity =>
            {
                entity.HasKey(e => e.SubjectId).HasName("PK__tmp_ms_x__D98E58EEF5CE6768");
            });

            modelBuilder.Entity<TopicsTable>(entity =>
            {
                entity.HasKey(e => e.TopicId).HasName("PK__TopicsTa__8DEBA02D05B5EB3B");

                entity.HasOne(d => d.Class).WithMany(p => p.TopicsTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_classstudents");

                entity.HasOne(d => d.Subject).WithMany(p => p.TopicsTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_subjectstudents");
            });

            modelBuilder.Entity<WeeksTable>(entity =>
            {
                entity.HasKey(e => e.WeekId).HasName("PK__tmp_ms_x__CDA23E5C1E580B69");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
