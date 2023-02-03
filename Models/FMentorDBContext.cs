using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FMentorAPI.Models
{
    public partial class FMentorDBContext : DbContext
    {
        public FMentorDBContext()
        {
        }

        public FMentorDBContext(DbContextOptions<FMentorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Education> Educations { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Mentee> Mentees { get; set; } = null!;
        public virtual DbSet<Mentor> Mentors { get; set; } = null!;
        public virtual DbSet<MentorAvailability> MentorAvailabilities { get; set; } = null!;
        public virtual DbSet<MentorWorkingTime> MentorWorkingTimes { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Specialty> Specialties { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserPermission> UserPermissions { get; set; } = null!;
        public virtual DbSet<UserSpecialty> UserSpecialties { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:fmentor.database.windows.net,1433;Initial Catalog=FMentorDB;Persist Security Info=False;User ID=sqladmin;Password=adpass01@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.GoogleMeetLink)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("google_meet_link");

                entity.Property(e => e.MenteeId).HasColumnName("mentee_id");

                entity.Property(e => e.MentorId).HasColumnName("mentor_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Mentee)
                    .WithMany(p => p.AppointmentMentees)
                    .HasForeignKey(d => d.MenteeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__mente__7E37BEF6");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.AppointmentMentors)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__mento__7F2BE32F");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.MenteeId).HasColumnName("mentee_id");

                entity.Property(e => e.MentorId).HasColumnName("mentor_id");

                entity.Property(e => e.ReasonForRejection)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reason_for_rejection");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TotalCost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_cost");

                entity.HasOne(d => d.Mentee)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.MenteeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bookings__mentee__00200768");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bookings__mentor__01142BA1");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Instructor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("instructor");

                entity.Property(e => e.Link)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("link");

                entity.Property(e => e.MentorId).HasColumnName("mentor_id");

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Platform)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("platform");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Courses__mentor___02084FDA");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.Property(e => e.EducationId).HasColumnName("education_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Major)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("major");

                entity.Property(e => e.School)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("school");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Education__user___02FC7413");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.Company)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("company");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jobs__user_id__05D8E0BE");
            });

            modelBuilder.Entity<Mentee>(entity =>
            {
                entity.Property(e => e.MenteeId).HasColumnName("mentee_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Mentees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mentees__user_id__06CD04F7");

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Mentees)
                    .UsingEntity<Dictionary<string, object>>(
                        "FavoriteCourse",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__FavoriteC__cours__03F0984C"),
                        r => r.HasOne<Mentee>().WithMany().HasForeignKey("MenteeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__FavoriteC__mente__04E4BC85"),
                        j =>
                        {
                            j.HasKey("MenteeId", "CourseId").HasName("PK__Favorite__38881DED171370A5");

                            j.ToTable("FavoriteCourses");

                            j.IndexerProperty<int>("MenteeId").HasColumnName("mentee_id");

                            j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                        });
            });

            modelBuilder.Entity<Mentor>(entity =>
            {
                entity.Property(e => e.MentorId).HasColumnName("mentor_id");

                entity.Property(e => e.Availability).HasColumnName("availability");

                entity.Property(e => e.HourlyRate)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("hourly_rate");

                entity.Property(e => e.Specialty)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("specialty");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Mentors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mentors__user_id__08B54D69");
            });

            modelBuilder.Entity<MentorAvailability>(entity =>
            {
                entity.HasKey(e => new { e.MentorId, e.StartDate, e.EndDate })
                    .HasName("PK__MentorAv__B65B30A420F6C5B5");

                entity.ToTable("MentorAvailability");

                entity.Property(e => e.MentorId).HasColumnName("mentor_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.MentorAvailabilities)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MentorAva__mento__07C12930");
            });

            modelBuilder.Entity<MentorWorkingTime>(entity =>
            {
                entity.HasKey(e => new { e.MentorId, e.DayOfWeek })
                    .HasName("PK__MentorWo__7DBB903952DA4A25");

                entity.ToTable("MentorWorkingTime");

                entity.Property(e => e.MentorId).HasColumnName("mentor_id");

                entity.Property(e => e.DayOfWeek)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("day_of_week");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.MentorWorkingTimes)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MentorWor__mento__09A971A2");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.RevieweeId).HasColumnName("reviewee_id");

                entity.Property(e => e.ReviewerId).HasColumnName("reviewer_id");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__appoint__0A9D95DB");

                entity.HasOne(d => d.Reviewee)
                    .WithMany(p => p.ReviewReviewees)
                    .HasForeignKey(d => d.RevieweeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__reviewe__0C85DE4D");

                entity.HasOne(d => d.Reviewer)
                    .WithMany(p => p.ReviewReviewers)
                    .HasForeignKey(d => d.ReviewerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__reviewe__0B91BA14");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsMentor).HasColumnName("is_mentor");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.VideoIntroduction)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("video_introduction");

                entity.HasOne(d => d.IsMentorNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IsMentor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Users_UserPermissions_is_mentor_fk");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => e.IsMentor)
                    .HasName("UserPermissions_pk");

                entity.Property(e => e.IsMentor).HasColumnName("is_mentor");

                entity.Property(e => e.CanFollowMentors).HasColumnName("can_follow_mentors");

                entity.Property(e => e.CanLogout).HasColumnName("can_logout");

                entity.Property(e => e.CanMakeSchedule).HasColumnName("can_make_schedule");

                entity.Property(e => e.CanRequestToMentor).HasColumnName("can_request_to_mentor");

                entity.Property(e => e.CanSeeCourses).HasColumnName("can_see_courses");

                entity.Property(e => e.CanSeePolicy).HasColumnName("can_see_policy");

                entity.Property(e => e.CanSeeSettings).HasColumnName("can_see_settings");
            });

            modelBuilder.Entity<UserSpecialty>(entity =>
            {
                entity.Property(e => e.UserSpecialtyId).HasColumnName("user_specialty_id");

                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.UserSpecialties)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserSpeci__speci__0E6E26BF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSpecialties)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserSpeci__user___0F624AF8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
