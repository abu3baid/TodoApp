using Microsoft.EntityFrameworkCore;
using TodoApp.DbModel.Models;

#nullable disable

namespace TodoApp.DbModel
{
    public partial class tododbContext : DbContext
    {
        public bool IgnoreFilter { get; set; }
        public tododbContext()
        {
        }

        public tododbContext(DbContextOptions<tododbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;port=3306;user=root;password=anmmn59;database=tododb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("todo");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CreatorId, "fk_user_todo_idx");

                entity.HasIndex(e => e.AssignedId, "fk_user_assigned_idx");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CreatedDateUtcTime)
                    .HasColumnName("CreatedDate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedDateUtcTime)
                    .HasColumnName("UpdatedDate")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsArchived).HasColumnType("tinyint");

                entity.Property(e => e.IsRead).HasColumnType("tinyint");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Todos)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("fk_user_todo");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Todos)
                    .HasForeignKey(d => d.AssignedId)
                    .HasConstraintName("fk_user_assigned");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "Email_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CreatedDateUtcTime)
                    .HasColumnName("CreatedDate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedDateUtcTime)
                    .HasColumnName("UpdatedDate")
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsAdmin).HasColumnType("tinyint");

                entity.Property(e => e.IsArchived).HasColumnType("tinyint");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<User>().HasQueryFilter(a => !a.IsArchived || IgnoreFilter);
            modelBuilder.Entity<Todo>().HasQueryFilter(a => !a.IsArchived || IgnoreFilter);

            modelBuilder.Entity<Todo>().HasQueryFilter(a => !a.IsRead || IgnoreFilter);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
