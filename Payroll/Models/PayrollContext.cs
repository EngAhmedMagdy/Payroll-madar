using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Payroll.Models
{
    public partial class PayrollContext : DbContext
    {
        public PayrollContext()
        {
        }

        public PayrollContext(DbContextOptions<PayrollContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Bonu> Bonus { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<LkupAbsent> LkupAbsents { get; set; }
        public virtual DbSet<PayrollReport> PayrollReports { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Payroll;Trusted_Connection=False;user id=magdy;password=5020786");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");

                entity.Property(e => e.AttendanceId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ATTENDANCE_ID");

                entity.Property(e => e.AbsentCaseId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ABSENT_CASE_ID");

                entity.Property(e => e.AbsentDate)
                    .HasColumnType("date")
                    .HasColumnName("ABSENT_DATE");

                entity.Property(e => e.AbsentReason)
                    .HasMaxLength(500)
                    .HasColumnName("ABSENT_REASON");

                entity.Property(e => e.AttendDate)
                    .HasColumnType("date")
                    .HasColumnName("ATTEND_DATE");

                entity.Property(e => e.EmpId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("EMP_ID");

                entity.Property(e => e.LeaveDate)
                    .HasColumnType("date")
                    .HasColumnName("LEAVE_DATE");

                entity.HasOne(d => d.AbsentCase)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.AbsentCaseId)
                    .HasConstraintName("FK_Attendance_Absent");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Attendanc__EMP_I__02084FDA");
            });

            modelBuilder.Entity<Bonu>(entity =>
            {
                entity.HasKey(e => e.BonusId)
                    .HasName("PK__Bonus__CE2CC37FC89E322C");

                entity.Property(e => e.BonusId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BONUS_ID");

                entity.Property(e => e.AbsentCaseId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ABSENT_CASE_ID");

                entity.Property(e => e.Annual)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ANNUAL");

                entity.Property(e => e.BonusDep)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("BONUS_DEP");

                entity.Property(e => e.Bouns)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("BOUNS");

                entity.Property(e => e.Discount)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("DISCOUNT");

                entity.HasOne(d => d.AbsentCase)
                    .WithMany(p => p.Bonus)
                    .HasForeignKey(d => d.AbsentCaseId)
                    .HasConstraintName("FK_Bonus_ABSENT");

                entity.HasOne(d => d.BonusDepNavigation)
                    .WithMany(p => p.Bonus)
                    .HasForeignKey(d => d.BonusDep)
                    .HasConstraintName("FK_Bonus_Dep");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepId)
                    .HasName("PK__Departme__E0AB2E5B54FF8855");

                entity.ToTable("Department");

                entity.Property(e => e.DepId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DEP_ID");

                entity.Property(e => e.DepDescription)
                    .HasMaxLength(200)
                    .HasColumnName("DEP_DESCRIPTION");

                entity.Property(e => e.DepName)
                    .HasMaxLength(200)
                    .HasColumnName("DEP_NAME");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__16EBFA26BFEB4068");

                entity.ToTable("Employee");

                entity.Property(e => e.EmpId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EMP_ID");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("BIRTH_DATE");

                entity.Property(e => e.DepId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("DEP_ID");

                entity.Property(e => e.EmpAddress)
                    .HasMaxLength(200)
                    .HasColumnName("EMP_ADDRESS");

                entity.Property(e => e.EmpEmail)
                    .HasMaxLength(200)
                    .HasColumnName("EMP_EMAIL");

                entity.Property(e => e.EmpGrade)
                    .HasMaxLength(200)
                    .HasColumnName("EMP_GRADE");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(200)
                    .HasColumnName("EMP_NAME");

                entity.Property(e => e.EmpPhone)
                    .HasMaxLength(200)
                    .HasColumnName("EMP_PHONE");

                entity.Property(e => e.HireDate)
                    .HasColumnType("date")
                    .HasColumnName("HIRE_DATE");

                entity.Property(e => e.JobId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("JOB_ID");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("FK__Employee__DEP_ID__7E37BEF6");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Employee__JOB_ID__7F2BE32F");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.JobId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("JOB_ID");

                entity.Property(e => e.JobDep)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("JOB_DEP");

                entity.Property(e => e.JobDescription)
                    .HasMaxLength(200)
                    .HasColumnName("JOB_DESCRIPTION");

                entity.Property(e => e.JobName)
                    .HasMaxLength(200)
                    .HasColumnName("JOB_NAME");

                entity.Property(e => e.SalaryRange)
                    .HasMaxLength(200)
                    .HasColumnName("SALARY_RANGE");

                entity.HasOne(d => d.JobDepNavigation)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.JobDep)
                    .HasConstraintName("FK__Job__JOB_DEP__286302EC");
            });

            modelBuilder.Entity<LkupAbsent>(entity =>
            {
                entity.HasKey(e => e.AbsentId)
                    .HasName("PK__Lkup_Abs__B96E137593EAFC91");

                entity.ToTable("Lkup_Absent");

                entity.Property(e => e.AbsentId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABSENT_ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("CODE");

                entity.Property(e => e.DaysNumber)
                    .HasMaxLength(200)
                    .HasColumnName("DAYS_NUMBER");
            });

            modelBuilder.Entity<PayrollReport>(entity =>
            {
                entity.HasKey(e => e.PayrollId)
                    .HasName("PK__Payroll___C67082323DB022B6");

                entity.ToTable("Payroll_Report");

                entity.Property(e => e.PayrollId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYROLL_ID");

                entity.Property(e => e.AttendanceId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ATTENDANCE_ID");

                entity.Property(e => e.EmpId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("EMP_ID");

                entity.Property(e => e.JobId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Job_ID");

                entity.Property(e => e.PayrollDate)
                    .HasColumnType("date")
                    .HasColumnName("PAYROLL_DATE");

                entity.Property(e => e.SalaryId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("SALARY_ID");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("TOTAL_AMOUNT");

                entity.HasOne(d => d.Attendance)
                    .WithMany(p => p.PayrollReports)
                    .HasForeignKey(d => d.AttendanceId)
                    .HasConstraintName("FK__Payroll_R__ATTEN__10566F31");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.PayrollReports)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Payroll_R__EMP_I__0D7A0286");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PayrollReports)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Payroll_R__Job_I__0E6E26BF");

                entity.HasOne(d => d.Salary)
                    .WithMany(p => p.PayrollReports)
                    .HasForeignKey(d => d.SalaryId)
                    .HasConstraintName("FK__Payroll_R__SALAR__0F624AF8");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salary");

                entity.Property(e => e.SalaryId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SALARY_ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.JobId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("JOB_ID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Salary__JOB_ID__0A9D95DB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
