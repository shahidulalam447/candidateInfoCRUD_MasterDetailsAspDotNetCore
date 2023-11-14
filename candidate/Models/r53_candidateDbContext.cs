using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace candidate.Models
{
    public partial class r53_candidateDbContext : DbContext
    {
        public r53_candidateDbContext()
        {
        }

        public r53_candidateDbContext(DbContextOptions<r53_candidateDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<CandidateSkill> CandidateSkills { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.; Database=r53_candidateDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.Property(e => e.CandidateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<CandidateSkill>(entity =>
            {
                entity.HasKey(e => e.CandidateSkillsId)
                    .HasName("PK__Candidat__717B8D26DA62707C");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateSkills)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("FK__Candidate__Candi__286302EC");

                entity.HasOne(d => d.Skills)
                    .WithMany(p => p.CandidateSkills)
                    .HasForeignKey(d => d.SkillsId)
                    .HasConstraintName("FK__Candidate__Skill__29572725");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.SkillsId)
                    .HasName("PK__Skills__95A17ED5E4371EE7");

                entity.Property(e => e.SkillName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
