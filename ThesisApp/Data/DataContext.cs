using Microsoft.EntityFrameworkCore;
using ThesisApp.Models;

namespace ThesisApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PreThesis> PreTheses { get; set; }
        public DbSet<MentorPair> MentorPairs { get; set; }
        public DbSet<MentoringSession> MentoringSessions { get; set; }
        public DbSet<Thesis> Theses { get; set; }
        public DbSet<ThesisDefence> ThesisDefences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PreThesis>()
                .HasOne(pt => pt.Student)
                .WithOne(u => u.PreThesis)
                .HasForeignKey<PreThesis>(pt => pt.StudentId);

            modelBuilder.Entity<MentorPair>()
                .HasOne(mp => mp.PreThesis)
                .WithOne(pt => pt.MentorPair)
                .HasForeignKey<MentorPair>(mp => mp.PreThesisId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MentorPair>()
                .HasOne(mp => mp.MentorLecturer)
                .WithOne(u => u.MentorPair)
                .HasForeignKey<MentorPair>(mp => mp.MentorLecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MentoringSession>()
                .HasOne(ms => ms.MentorPair)
                .WithMany(mp => mp.MentoringSessions)
                .HasForeignKey(ms => ms.MentorPairId);

            modelBuilder.Entity<Thesis>()
                .HasOne(t => t.Student)
                .WithOne(u => u.Thesis)
                .HasForeignKey<Thesis>(t => t.StudentId);

            modelBuilder.Entity<ThesisDefence>()
                .HasOne(td => td.Thesis)
                .WithOne(t => t.ThesisDefence)
                .HasForeignKey<ThesisDefence>(td => td.ThesisId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ThesisDefence>()
                .HasOne(td => td.MentorLecturer)
                .WithOne(u => u.ThesisDefenceForMentor)
                .HasForeignKey<ThesisDefence>(td => td.MentorLecturerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ThesisDefence>()
                .HasOne(td => td.ExaminerLecturer)
                .WithOne(u => u.ThesisDefenceForExaminer)
                .HasForeignKey<ThesisDefence>(td => td.ExaminerLecturerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
