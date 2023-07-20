using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace project.Models
{
    public partial class project_PRN231Context : DbContext
    {
        public project_PRN231Context()
        {
        }

        public project_PRN231Context(DbContextOptions<project_PRN231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<ExamResult> ExamResults { get; set; } = null!;
        public virtual DbSet<GradeDetail> GradeDetails { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<TestCase> TestCases { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;database=project_PRN231;Integrated security=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("adminId");

                entity.Property(e => e.Password)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => new { e.ExamId, e.PaperNo })
                    .HasName("PK__Exams__46FBF441E8CB5CFA");

                entity.Property(e => e.ExamId).HasColumnName("examId");

                entity.Property(e => e.PaperNo).HasColumnName("paperNo");

                entity.Property(e => e.ExamName)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("examName");
            });

            modelBuilder.Entity<ExamResult>(entity =>
            {
                entity.Property(e => e.ExamresultId).HasColumnName("examresultId");

                entity.Property(e => e.ExamId).HasColumnName("examId");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.PaperNo).HasColumnName("paperNo");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ExamResults)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__ExamResul__stude__300424B4");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamResults)
                    .HasForeignKey(d => new { d.ExamId, d.PaperNo })
                    .HasConstraintName("FK__ExamResults__30F848ED");
            });

            modelBuilder.Entity<GradeDetail>(entity =>
            {
                entity.HasKey(e => new { e.ExamresultId, e.QuestionId, e.TestcaseId })
                    .HasName("PK__GradeDet__5E1286531957C825");

                entity.ToTable("GradeDetail");

                entity.Property(e => e.ExamresultId).HasColumnName("examresultId");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.TestcaseId).HasColumnName("testcaseId");

                entity.Property(e => e.Output)
                    .IsUnicode(false)
                    .HasColumnName("output");

                entity.Property(e => e.Testresult).HasColumnName("testresult");

                entity.HasOne(d => d.Examresult)
                    .WithMany(p => p.GradeDetails)
                    .HasForeignKey(d => d.ExamresultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GradeDeta__examr__33D4B598");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.GradeDetails)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GradeDeta__quest__34C8D9D1");

                entity.HasOne(d => d.Testcase)
                    .WithMany(p => p.GradeDetails)
                    .HasForeignKey(d => d.TestcaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GradeDeta__testc__35BCFE0A");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.ExamId).HasColumnName("examId");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.PaperNo).HasColumnName("paperNo");

                entity.Property(e => e.QuestionName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("questionName");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => new { d.ExamId, d.PaperNo })
                    .HasConstraintName("FK__Questions__2A4B4B5E");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("studentName");
            });

            modelBuilder.Entity<TestCase>(entity =>
            {
                entity.Property(e => e.TestcaseId).HasColumnName("testcaseId");

                entity.Property(e => e.Input)
                    .IsUnicode(false)
                    .HasColumnName("input");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.Output)
                    .IsUnicode(false)
                    .HasColumnName("output");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TestCases)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__TestCases__quest__2D27B809");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
