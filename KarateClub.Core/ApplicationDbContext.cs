using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KarateClub.Core;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BeltRank> BeltRanks { get; set; }

    public virtual DbSet<BeltTest> BeltTests { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberInstructor> MemberInstructors { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<SubscriptionPeriod> SubscriptionPeriods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=KarateClub2;User Id=sa;Password=123456;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AI");

        modelBuilder.Entity<BeltRank>(entity =>
        {
            entity.HasKey(e => e.RankId).HasName("PK__BeltRank__B37AFB9666B5014A");

            entity.ToTable("BeltRank");

            entity.Property(e => e.RankId)
                .ValueGeneratedNever()
                .HasColumnName("RankID");
            entity.Property(e => e.RankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestFees).HasColumnType("smallmoney");
        });

        modelBuilder.Entity<BeltTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__BeltTest__8CC331004560729F");

            entity.ToTable("BeltTest");

            entity.Property(e => e.TestId)
                .ValueGeneratedNever()
                .HasColumnName("TestID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.RankId).HasColumnName("RankID");
            entity.Property(e => e.TestedByInstructorId).HasColumnName("TestedByInstructorID");

            entity.HasOne(d => d.Member).WithMany(p => p.BeltTests)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BeltTest__Member__45F365D3");

            entity.HasOne(d => d.Payment).WithMany(p => p.BeltTests)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK_BeltTest_Payment");

            entity.HasOne(d => d.Rank).WithMany(p => p.BeltTests)
                .HasForeignKey(d => d.RankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BeltTest_BeltRank");

            entity.HasOne(d => d.TestedByInstructor).WithMany(p => p.BeltTests)
                .HasForeignKey(d => d.TestedByInstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BeltTest__Tested__47DBAE45");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__Instruct__9D010B7BAD035323");

            entity.ToTable("Instructor");

            entity.Property(e => e.InstructorId)
                .ValueGeneratedNever()
                .HasColumnName("InstructorID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Qualification)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Person).WithMany(p => p.Instructors)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Instructo__Perso__3F466844");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B38C00935CE");

            entity.ToTable("Member");

            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("MemberID");
            entity.Property(e => e.EmergencyContactInfo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastBeltRank).HasDefaultValue(1);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.LastBeltRankNavigation).WithMany(p => p.Members)
                .HasForeignKey(d => d.LastBeltRank)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Member_BeltRank");

            entity.HasOne(d => d.Person).WithMany(p => p.Members)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Member_Person");
        });

        modelBuilder.Entity<MemberInstructor>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.InstructorId }).HasName("PK__MemberIn__A5205B8FB97EC967");

            entity.ToTable("MemberInstructor");

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.InstructorId).HasColumnName("InstructorID");
            entity.Property(e => e.AssignDate).HasColumnType("datetime");

            entity.HasOne(d => d.Instructor).WithMany(p => p.MemberInstructors)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberIns__Instr__4316F928");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberInstructors)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberIns__Membe__4222D4EF");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A586A2A8A1D");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Member).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Member");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Person__AA2FFB85CC8CB708");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("PersonID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubscriptionPeriod>(entity =>
        {
            entity.HasKey(e => e.PeriodId);

            entity.Property(e => e.PeriodId)
                .ValueGeneratedNever()
                .HasColumnName("PeriodID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Fees).HasColumnType("smallmoney");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.SubscriptionPeriods)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubscriptionPeriods_Member");

            entity.HasOne(d => d.Payment).WithMany(p => p.SubscriptionPeriods)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK_SubscriptionPeriods_Payment");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
