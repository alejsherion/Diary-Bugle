using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClarinDiary.DataAccess.Models
{
    public partial class ClarinDiaryContext : DbContext
    {
        public ClarinDiaryContext() { }

        public ClarinDiaryContext(DbContextOptions<ClarinDiaryContext> options)
            : base(options) { }

        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostComment> PostComment { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasComment("contains the people added to the application");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("User full name");

                entity.Property(e => e.IdRol).HasComment("Rol represents in the aplication");

                entity.Property(e => e.Identification)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasComment("User identification");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Person_Rol");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasComment("contains the publication for newspaper content");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdAuthor).HasComment("publication writer");

                entity.Property(e => e.IdPublisher).HasComment("publication writer");

                entity.Property(e => e.PostContent)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasComment("Post Content");

                entity.Property(e => e.PostDate)
                    .HasColumnType("datetime")
                    .HasComment("Publication Date");

                entity.Property(e => e.State).HasComment(@"Publication status:
0- Draft
1- pending approval
2- Approved
3- Rejected");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.PostIdAuthorNavigation)
                    .HasForeignKey(d => d.IdAuthor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Person");

                entity.HasOne(d => d.IdPublisherNavigation)
                    .WithMany(p => p.PostIdPublisherNavigation)
                    .HasForeignKey(d => d.IdPublisher)
                    .HasConstraintName("FK_Post_Person1");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasComment("Contains the comments of the publications");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasComment("Content of commentary about the related post");

                entity.Property(e => e.IdPerson).HasComment("person who comments on the publication if the field is null the author of the comment is anonymous");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.PostComment)
                    .HasForeignKey(d => d.IdPerson)
                    .HasConstraintName("FK_PostComment_Person");

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany(p => p.PostComment)
                    .HasForeignKey(d => d.IdPost)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComment_Post");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
